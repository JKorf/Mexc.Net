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
using System.Collections.Generic;
using System.Net.WebSockets;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal partial class MexcSocketClientSpotApi : SocketApiClient, IMexcSocketClientSpotApi
    {
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _msgPath = MessagePath.Get().Property("msg");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("c");

        public event Action<ListenKeyRenewedEvent>? ListenkeyRenewed;

        #region constructor/destructor

        // Note, this client doens't have HighPerf variant subscriptions for 2 reasons:
        // 1. The socket sends mixed Text and Binary message
        // 2. The protobuf messages send in Binary format are not (lenght) delimited, making it impossible to continuously deserialize as is.
        // Implementing a work around for this would not be as performant and defeats the idea of the HighPerf subscription

        internal MexcSocketClientSpotApi(ILogger logger, MexcSocketOptions options) :
            base(logger, options.Environment.SpotSocketAddress, options, options.SpotOptions)
        {
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
        }

        #endregion
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType)
        {
            if (messageType == WebSocketMessageType.Text)
                return new MexcSocketSpotMessageHandler();

            return new MexcProtobufMessageHandler();
        }

        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => throw new NotImplementedException();

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange.SerializerContext));

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor messageAccessor)
        {
            var msg = messageAccessor.GetValue<string?>(_msgPath);
            if (msg?.Equals("PONG", StringComparison.Ordinal) == true)
                return "PONG";

            var id = messageAccessor.GetValue<int?>(_idPath);
            if (id != null)
                return id.Value.ToString();

            return messageAccessor.GetValue<string>(_channelPath);
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new MexcAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => MexcExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        public IMexcSocketClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<MexcStreamTrade[]>> handler, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync(new[] { symbol }, 10, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, int interval, Action<DataEvent<MexcStreamTrade[]>> handler, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync(new[] { symbol }, interval, handler, ct).ConfigureAwait(false);

        private DateTime? GetDataTimestamp(long sendTime, long createTime)
        {
            var time = sendTime != 0 ? sendTime : createTime;
            if (time == 0)
                return null;

            return DateTimeConverter.ParseFromDecimal(time);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, int interval, Action<DataEvent<MexcStreamTrade[]>> handler, CancellationToken ct = default)
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default)
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, 10, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int updateInterval, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, updateInterval, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int updateInterval, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateOrderBook>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcStreamOrderBook>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcSubscription<MexcUpdateOrderBook>(_logger, symbols.Select(s => "spot@public.aggre.depth.v3.api.pb@" + updateInterval + "ms@" + s).ToArray(), internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
            => await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, depth, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
        {
            depth.ValidateIntValues(nameof(depth), 5, 10, 20);

            var internalHandler = new Action<DateTime, string?, MexcUpdateOrderBookLimit>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(DateTimeConverter.ConvertFromMilliseconds(data.SendTime));

                handler(
                    new DataEvent<MexcStreamOrderBook>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(GetDataTimestamp(data.SendTime, data.CreateTime), GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcSubscription<MexcUpdateOrderBookLimit>(_logger, symbols.Select(s => "spot@public.limit.depth.v3.api.pb@" + s + "@" + depth).ToArray(), internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default)
            => await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default)
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<MexcMiniTickUpdate>> handler, CancellationToken ct = default)
            => await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, null, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcMiniTickUpdate>> handler, CancellationToken ct = default)
            => await SubscribeToMiniTickerUpdatesAsync(symbols, null, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, string? timeZone, Action<DataEvent<MexcMiniTickUpdate>> handler, CancellationToken ct = default)
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
        public Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(Action<DataEvent<MexcMiniTickUpdate[]>> handler, CancellationToken ct = default)
            => SubscribeToAllMiniTickerUpdatesAsync(null, handler);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(string? timeZone, Action<DataEvent<MexcMiniTickUpdate[]>> handler, CancellationToken ct = default)
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(string listenKey, Action<DataEvent<MexcAccountUpdate>> handler, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

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

            var subscription = new MexcSubscription<MexcUpdateAccount>(_logger, new[] { "spot@private.account.v3.api.pb" }, internalHandler, true);
            return await SubscribeAsync(BaseAddress + "?listenKey=" + listenKey, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string listenKey, Action<DataEvent<MexcUserOrderUpdate>> handler, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

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

            var subscription = new MexcSubscription<MexcUpdateOrder>(_logger, new[] { "spot@private.orders.v3.api.pb" }, internalHandler, true);
            return await SubscribeAsync(BaseAddress + "?listenKey=" + listenKey, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string listenKey, Action<DataEvent<MexcUserTradeUpdate>> handler, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

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
            var subscription = new MexcSubscription<MexcUpdateUserTrade>(_logger, new[] { "spot@private.deals.v3.api.pb" }, internalHandler, true);
            return await SubscribeAsync(BaseAddress + "?listenKey=" + listenKey, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override async Task<Uri?> GetReconnectUriAsync(ISocketConnection connection)
        {
            if (connection.HasAuthenticatedSubscription)
            {
                // If any of the subs on the connection is authenticated we request a new listenkey
                // to prevent endlessly looping if the listenkey happens to be expired
                var client = new MexcRestClient(opts =>
                {
                    opts.ApiCredentials = ApiCredentials;
                });

                var listenKeyResult = await client.SpotApi.Account.StartUserStreamAsync().ConfigureAwait(false);
                if (listenKeyResult)
                {
                    var oldKey = connection.ConnectionUri.Query.Split('=')[1];
                    if (oldKey != listenKeyResult.Data)
                        ListenkeyRenewed?.Invoke(new ListenKeyRenewedEvent(oldKey, listenKeyResult.Data));
                    return new Uri(BaseAddress + "?listenKey=" + listenKeyResult.Data);
                }
            }

            return await base.GetReconnectUriAsync(connection).ConfigureAwait(false);
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
