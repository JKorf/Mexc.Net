using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.HighPerf.Interfaces;
using CryptoExchange.Net.Sockets.Interfaces;
using CryptoExchange.Net.TokenManagement;
using Mexc.Net.Clients.MessageHandlers;
using Mexc.Net.Converters;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Protobuf;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Objects.Options;
using Mexc.Net.Objects.Sockets.Queries;
using Mexc.Net.Objects.Sockets.Subscriptions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reflection;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal partial class MexcSocketClientSpotApi : SocketApiClient<MexcEnvironment, MexcAuthenticationProvider, MexcCredentials>, IMexcSocketClientSpotApi
    {
        private readonly ILoggerFactory? _loggerFactory;
        private MexcRestClient? _tokenClient;
        internal TokenManager TokenManager { get; }
        private MexcRestClient TokenClient
        {
            get
            {
                if (_tokenClient == null)
                {
                    _tokenClient = new MexcRestClient(null, _loggerFactory, Options.Create(new MexcRestOptions
                    {
                        ApiCredentials = ApiCredentials,
                        Environment = ClientOptions.Environment,
                        Proxy = ClientOptions.Proxy,
                        OutputOriginalData = ClientOptions.OutputOriginalData
                    }));
                }

                return _tokenClient;
            }
        }

        public event Action<ListenKeyRenewedEvent>? ListenkeyRenewed;

        #region constructor/destructor

        // Note, this client doesn't have HighPerf variant subscriptions for 2 reasons:
        // 1. The socket sends mixed Text and Binary message
        // 2. The protobuf messages send in Binary format are not (length) delimited, making it impossible to continuously deserialize as is.
        // Implementing a work around for this would not be as performant and defeats the idea of the HighPerf subscription

        internal MexcSocketClientSpotApi(ILoggerFactory? loggerFactory, MexcSocketOptions options) :
            base(loggerFactory, MexcExchange.Metadata.Id, options.Environment.SpotSocketAddress, options, options.SpotOptions)
        {
            _loggerFactory = loggerFactory;

            AddSystemSubscription(new MexcErrorSubscription(_logger));
            RateLimiter = MexcExchange.RateLimiter.SpotSocket;

            MessageSendSizeLimit = 1024;
            MaxIndividualSubscriptionsPerConnection = 30;

            RegisterPeriodicQuery(
                "Ping",
                TimeSpan.FromSeconds(30),
                x => new MexcPingQuery(),
                (connection, result) =>
                {
                    if (result.Error?.ErrorType == ErrorType.Timeout)
                    {
                        // Ping timeout, reconnect
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
                });

            TokenManager = new TokenManager(
                MexcExchange.Metadata.Id,
                loggerFactory,
                TimeSpan.FromMinutes(30),
                TimeSpan.FromMinutes(60),
                startToken: StartListenKeyAsync,
                keepAliveToken: KeepAliveListenKeyAsync,
                stopToken: StopListenKeyAsync);
        }

        #endregion

        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType)
        {
            if (messageType == WebSocketMessageType.Text)
                return new MexcSocketSpotMessageHandler();

            return new MexcProtobufMessageHandler();
        }

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange._serializerContext));

        /// <inheritdoc />
        protected override MexcAuthenticationProvider CreateAuthenticationProvider(MexcCredentials credentials)
            => new MexcAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => MexcExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        public IMexcSocketClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<MexcStreamTrade[]>> handler, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync(new[] { symbol }, 10, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, int interval, Action<DataEvent<MexcStreamTrade[]>> handler, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync(new[] { symbol }, interval, handler, ct).ConfigureAwait(false);

        private DateTime? GetDataTimestamp(long sendTime, long createTime)
        {
            var time = sendTime != 0 ? sendTime : createTime;
            if (time == 0)
                return null;

            return DateTimeConverter.ParseFromDecimal(time);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, int interval, Action<DataEvent<MexcStreamTrade[]>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateTrades>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcStreamTrade[]>(MexcExchange.ExchangeName, data.Data.Select(x => x.ToModel()).ToArray(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcSubscription<MexcUpdateTrades>(_logger, symbols.Select(s => "spot@public.aggre.deals.v3.api.pb@"+interval+"ms@" + s).ToArray(),
                internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateKlines>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcStreamKline>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcSubscription<MexcUpdateKlines>(_logger, symbols.Select(s => "spot@public.kline.v3.api.pb@" + s + "@" + GetIntervalString(interval)).ToArray(), internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, 10, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int updateInterval, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, updateInterval, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int updateInterval, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateOrderBook>((receiveTime, originalData, data) =>
            {
                var model = data.ToModel();
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcStreamOrderBook>(MexcExchange.ExchangeName, model, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                        .WithSequenceNumber(model.SequenceEnd ?? model.Sequence)
                    );
            });

            var subscription = new MexcSubscription<MexcUpdateOrderBook>(_logger, symbols.Select(s => "spot@public.aggre.depth.v3.api.pb@" + updateInterval + "ms@" + s).ToArray(), internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
            => await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, depth, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
        {
            depth.ValidateIntValues(nameof(depth), 5, 10, 20);

            var internalHandler = new Action<DateTime, string?, MexcUpdateOrderBookLimit>((receiveTime, originalData, data) =>
            {
                var model = data.ToModel();
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcStreamOrderBook>(MexcExchange.ExchangeName, model, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                        .WithSequenceNumber(model.SequenceEnd ?? model.Sequence)
                    );
            });

            var subscription = new MexcSubscription<MexcUpdateOrderBookLimit>(_logger, symbols.Select(s => "spot@public.limit.depth.v3.api.pb@" + s + "@" + depth).ToArray(), internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default)
            => await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateBookTickers>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcStreamBookTick>(MexcExchange.ExchangeName, data.Data.First().ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcSubscription<MexcUpdateBookTickers>(_logger, symbols.Select(s => "spot@public.bookTicker.batch.v3.api.pb@" + s).ToArray(), internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<MexcMiniTickUpdate>> handler, CancellationToken ct = default)
            => await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, null, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcMiniTickUpdate>> handler, CancellationToken ct = default)
            => await SubscribeToMiniTickerUpdatesAsync(symbols, null, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, string? timeZone, Action<DataEvent<MexcMiniTickUpdate>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateMiniTicker>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcMiniTickUpdate>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcSubscription<MexcUpdateMiniTicker>(_logger, symbols.Select(s => "spot@public.miniTicker.v3.api.pb@" + s + "@" + (timeZone ?? "24H")).ToArray(), internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(Action<DataEvent<MexcMiniTickUpdate[]>> handler, CancellationToken ct = default)
            => SubscribeToAllMiniTickerUpdatesAsync(null, handler);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(string? timeZone, Action<DataEvent<MexcMiniTickUpdate[]>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateMiniTickers>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcMiniTickUpdate[]>(MexcExchange.ExchangeName, data.Tickers.Select(x => x.ToModel()).ToArray(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcSubscription<MexcUpdateMiniTickers>(_logger, ["spot@public.miniTickers.v3.api.pb@" + (timeZone ?? "24H")], internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<MexcAccountUpdate>> handler, CancellationToken ct = default)
            => SubscribeToAccountUpdatesAsync(null, handler, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(string? listenKey, Action<DataEvent<MexcAccountUpdate>> handler, CancellationToken ct = default)
        {
            if (listenKey == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    MexcExchange.Metadata.Id,
                    EnvironmentName,
                    "Spot",
                    ApiCredentials!.Credential!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var lk = listenKey ?? lease!.Token.Token;
            var internalHandler = new Action<DateTime, string?, MexcUpdateAccount>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcAccountUpdate>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcSubscription<MexcUpdateAccount>(_logger, new[] { "spot@private.account.v3.api.pb" }, internalHandler, true)
            {
                TokenLease = lease
            };
            var result = await SubscribeAsync(BaseAddress + "?listenKey=" + lk, subscription, ct).ConfigureAwait(false);
            if (!result.Success && lease != null)
                await lease.ReleaseAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<MexcUserOrderUpdate>> handler, CancellationToken ct = default)
            => SubscribeToOrderUpdatesAsync(null, handler, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? listenKey, Action<DataEvent<MexcUserOrderUpdate>> handler, CancellationToken ct = default)
        {
            if (listenKey == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    MexcExchange.Metadata.Id,
                    EnvironmentName,
                    "Spot",
                    ApiCredentials!.Credential!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var lk = listenKey ?? lease!.Token.Token;
            var internalHandler = new Action<DateTime, string?, MexcUpdateOrder>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcUserOrderUpdate>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcSubscription<MexcUpdateOrder>(_logger, new[] { "spot@private.orders.v3.api.pb" }, internalHandler, true)
            {
                TokenLease = lease
            };
            var result = await SubscribeAsync(BaseAddress + "?listenKey=" + lk, subscription, ct).ConfigureAwait(false);
            if (!result.Success && lease != null)
                await lease.ReleaseAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<MexcUserTradeUpdate>> handler, CancellationToken ct = default)
            => SubscribeToUserTradeUpdatesAsync(null, handler, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string? listenKey, Action<DataEvent<MexcUserTradeUpdate>> handler, CancellationToken ct = default)
        {
            if (listenKey == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    MexcExchange.Metadata.Id,
                    EnvironmentName,
                    "Spot",
                    ApiCredentials!.Credential!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var lk = listenKey ?? lease!.Token.Token;
            var internalHandler = new Action<DateTime, string?, MexcUpdateUserTrade>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcUserTradeUpdate>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });
            var subscription = new MexcSubscription<MexcUpdateUserTrade>(_logger, new[] { "spot@private.deals.v3.api.pb" }, internalHandler, true)
            {
                TokenLease = lease
            };
            var result = await SubscribeAsync(BaseAddress + "?listenKey=" + lk, subscription, ct).ConfigureAwait(false);
            if (!result.Success && lease != null)
                await lease.ReleaseAsync().ConfigureAwait(false);

            return result;
        }

        protected override async Task<Uri?> GetReconnectUriAsync(ISocketConnection connection)
        {
            if (!connection.HasAuthenticatedSubscription)
                return await base.GetReconnectUriAsync(connection).ConfigureAwait(false);

            var subscriptions = ((SocketConnection)connection).Subscriptions.Where(x => x.TokenLease != null).ToList();
            if (subscriptions.Count == 0)
            {
                // We have authenticated subscriptions, but not via the token manager
                var listenKeyResult = await TokenClient.SpotApi.Account.StartUserStreamAsync().ConfigureAwait(false);
                if (listenKeyResult.Success)
                {
                    var oldKey = connection.ConnectionUri.Query.Split('=')[1];
                    if (oldKey != listenKeyResult.Data)
                        ListenkeyRenewed?.Invoke(new ListenKeyRenewedEvent(oldKey, listenKeyResult.Data));
                    return new Uri(BaseAddress + "?listenKey=" + listenKeyResult.Data);
                }

                return null;
            }

            // We have authenticated subscription via the token manager
            var scope = new TokenScope(
                    MexcExchange.Metadata.Id,
                    EnvironmentName,
                    "Spot",
                    ApiCredentials!.Credential.Key);

            var token = await TokenManager.AcquireAndReplaceAsync(subscriptions[0], scope).ConfigureAwait(false);
            if (!token.Success)
                return null;

            return new Uri(BaseAddress + "?listenKey=" + token.Data.Token.Token);
        }

        private async Task<CallResult<string>> StartListenKeyAsync(TokenScope tokenScope, CancellationToken ct)
        {
            var result = await TokenClient.SpotApi.Account.StartUserStreamAsync(ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok(result.Data);
        }

        private async Task<CallResult> KeepAliveListenKeyAsync(TokenInfo token, CancellationToken ct)
        {
            var result = await TokenClient.SpotApi.Account.KeepAliveUserStreamAsync(token.Token, ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok();
        }

        private async Task<CallResult> StopListenKeyAsync(TokenInfo token, CancellationToken ct)
        {
            var result = await TokenClient.SpotApi.Account.StopUserStreamAsync(token.Token, ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok();
        }

        private static string GetIntervalString(KlineInterval interval)
            => interval switch
            {
                KlineInterval.OneMinute => "Min1",
                KlineInterval.FiveMinutes => "Min5",
                KlineInterval.FifteenMinutes => "Min15",
                KlineInterval.ThirtyMinutes => "Min30",
                KlineInterval.OneHour => "Min60",
                KlineInterval.FourHours => "Hour4",
                KlineInterval.OneDay => "Day1",
                KlineInterval.OneWeek => "Week1",
                KlineInterval.OneMonth => "Month1",
                _ => throw new InvalidOperationException()
            };
    }
}
