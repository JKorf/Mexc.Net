using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.Protobuf;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Converters;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Protobuf;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Objects.Options;
using Mexc.Net.Objects.Sockets.Queries;
using Mexc.Net.Objects.Sockets.Subscriptions;
using ProtoBuf.Serializers;
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

        internal MexcSocketClientSpotApi(ILogger logger, MexcSocketOptions options) :
            base(logger, options.Environment.SpotSocketAddress, options, options.SpotOptions)
        {
            AddSystemSubscription(new MexcErrorSubscription(_logger));
            RateLimiter = MexcExchange.RateLimiter.SpotSocket;

            MessageSendSizeLimit = 1024;

            RegisterPeriodicQuery(
                "Ping",
                TimeSpan.FromSeconds(30),
                x => new MexcPingQuery(),
                (connection, result) =>
                {
                    if (result.Error?.Message.Equals("Query timeout") == true)
                    {
                        // Ping timeout, reconnect
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
                });
        }

        #endregion

        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) =>
            type == WebSocketMessageType.Binary ? new ProtobufByteMessageAccessor<SocketEvent>(ProtobufInclude.Model) :
            new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(MexcExchange.SerializerContext));

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

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, int interval, Action<DataEvent<MexcStreamTrade[]>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcUpdateTrades, ProtoTradeUpdate>(_logger, symbols.Select(s => "spot@public.aggre.deals.v3.api.pb@"+interval+"ms@" + s), x => handler(x.As(x.Data.Data.Select(x => x.ToModel()).ToArray())), false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcUpdateKlines, ProtoStreamKline>(_logger, symbols.Select(s => "spot@public.kline.v3.api.pb@" + s + "@" + GetIntervalString(interval)), x => handler(x.As(x.Data.ToModel())), false);
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
            var subscription = new MexcSubscription<MexcUpdateOrderBook, ProtoOrderBookUpdate>(_logger, symbols.Select(s => "spot@public.aggre.depth.v3.api.pb@" + updateInterval + "ms@" + s), x => handler(x.As(x.Data.ToModel())), false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcStreamOrderBook[]>> handler, CancellationToken ct = default)
            => await SubscribeToAggregatedOrderBookUpdatesAsync(new[] { symbol }, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamOrderBook[]>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcUpdateAggOrderBook, ProtoAggOrderBookUpdate>(_logger, symbols.Select(s => "spot@public.increase.depth.batch.v3.api.pb@" + s), x => handler(x.As(x.Data.Data.Select(x => x.ToModel()).ToArray())), false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
            => await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, depth, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
        {
            depth.ValidateIntValues(nameof(depth), 5, 10, 20);

            var subscription = new MexcSubscription<MexcUpdatePartialOrderBook, ProtoOrderBookUpdate>(_logger, symbols.Select(s => "spot@public.limit.depth.v3.api.pb@" + s + "@" + depth), x => handler(x.As(x.Data.ToModel())), false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default)
            => await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcUpdateBookTicker, ProtoStreamBookTickerUpdate>(_logger, symbols.Select(s => "spot@public.bookTicker.batch.v3.api.pb@" + s), x => handler(x.As(x.Data.Data.First().ToModel())), false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<MexcStreamMiniTick>> handler, string? timezone = null, CancellationToken ct = default)
            => await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, handler, timezone, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamMiniTick>> handler, string? timezone = null, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcUpdateMiniTicker, MexcStreamMiniTick>(_logger, symbols.Select(s => "spot@public.miniTicker.v3.api@" + s + "@" + (timezone ?? "UTC+0")), handler, false);
            return await SubscribeAsync("wss://wbs.mexc.com/ws", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(Action<DataEvent<MexcStreamMiniTick[]>> handler, string? timezone = null, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcUpdateMiniTickers, MexcStreamMiniTick[]>(_logger, new[] { "spot@public.miniTickers.v3.api@" + (timezone ?? "UTC+0") }, handler, false);
            return await SubscribeAsync("wss://wbs.mexc.com/ws", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(string listenKey, Action<DataEvent<MexcAccountUpdate>> handler, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var subscription = new MexcSubscription<MexcUpdateAccount, ProtoAccountUpdate>(_logger, new[] { "spot@private.account.v3.api.pb" }, x => handler(x.As(x.Data.ToModel())), true);
            return await SubscribeAsync(BaseAddress + "?listenKey=" + listenKey, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string listenKey, Action<DataEvent<MexcUserOrderUpdate>> handler, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var subscription = new MexcSubscription<MexcUpdateOrder, ProtoOrderUpdate>(_logger, new[] { "spot@private.orders.v3.api.pb" }, x => handler(x.As(x.Data.ToModel())), true);
            return await SubscribeAsync(BaseAddress + "?listenKey=" + listenKey, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string listenKey, Action<DataEvent<MexcUserTradeUpdate>> handler, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var subscription = new MexcSubscription<MexcUpdateUserTrade, ProtoUserTradeUpdate>(_logger, new[] { "spot@private.deals.v3.api.pb" }, x => handler(x.As(x.Data.ToModel())), true);
            return await SubscribeAsync(BaseAddress + "?listenKey=" + listenKey, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override async Task<Uri?> GetReconnectUriAsync(SocketConnection connection)
        {
            if (connection.Subscriptions.Any(s => s.Authenticated))
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

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

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
