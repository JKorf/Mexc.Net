using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Objects.Options;
using Mexc.Net.Objects.Sockets.Queries;
using Mexc.Net.Objects.Sockets.Subscriptions;
using System.Net.WebSockets;

namespace Mexc.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal partial class MexcSocketClientFuturesApi : SocketApiClient, IMexcSocketClientFuturesApi
    {
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("channel");
        private static readonly MessagePath _symbolPath = MessagePath.Get().Property("symbol");

        #region constructor/destructor

        internal MexcSocketClientFuturesApi(ILogger logger, MexcSocketOptions options) :
            base(logger, options.Environment.FuturesSocketAddress, options, options.FuturesOptions)
        {
            AddSystemSubscription(new MexcErrorSubscription(_logger));

            RegisterPeriodicQuery(
                "Ping",
                TimeSpan.FromSeconds(10),
                x => new MexcFuturesPingQuery(),
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

        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType msgType) => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(MexcExchange.SerializerContext));

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange.SerializerContext));

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor messageAccessor)
        {
            return messageAccessor.GetValue<string>(_channelPath) + messageAccessor.GetValue<string>(_symbolPath);
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new MexcFuturesAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => MexcExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection)
        {
            var authProvider = (MexcFuturesAuthenticationProvider)AuthenticationProvider!;
            return Task.FromResult<Query?>(new MexcFuturesQuery("login", authProvider.GetSocketAuthParameters(), false));
        }

        public IMexcSocketClientFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickersUpdatesAsync(Action<DataEvent<MexcFuturesTickerUpdate[]>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcFuturesSubscription<MexcFuturesTickerUpdate[]>(_logger, "tickers", null, null, null, handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesTicker>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcFuturesSubscription<MexcFuturesTicker>(_logger, "ticker", symbol, null, null, handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesTrade>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcFuturesSubscription<MexcFuturesTrade>(_logger, "deal", symbol, null, null, handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, FuturesKlineInterval interval, Action<DataEvent<MexcFuturesStreamKline>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcFuturesSubscription<MexcFuturesStreamKline>(_logger, "kline", symbol, interval, null, handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesOrderBook>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcFuturesSubscription<MexcFuturesOrderBook>(_logger, "depth", symbol, null, null, handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int? limit, Action<DataEvent<MexcFuturesOrderBook>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcFuturesSubscription<MexcFuturesOrderBook>(_logger, "depth.full", symbol, null, limit, handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<MexcFundingRateUpdate>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcFuturesSubscription<MexcFundingRateUpdate>(_logger, "funding.rate", symbol, null, null, handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<MexcPriceUpdate>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcFuturesSubscription<MexcPriceUpdate>(_logger, "index.price", symbol, null, null, handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<MexcPriceUpdate>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcFuturesSubscription<MexcPriceUpdate>(_logger, "fair.price", symbol, null, null, handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            Action<DataEvent<MexcFuturesBalanceUpdate>>? balanceUpdateHandler = null,
            Action<DataEvent<MexcFuturesOrder>>? orderUpdateHandler = null,
            Action<DataEvent<MexcPosition>>? positionUpdateHandler = null,
            Action<DataEvent<MexcRiskLimit>>? riskLimitUpdateHandler = null,
            Action<DataEvent<MexcAdlUpdate>>? adlUpdateHandler = null,
            Action<DataEvent<MexcPositionModeUpdate>>? positionModeUpdateHandler = null,
            CancellationToken ct = default)
        {
            var subscription = new MexcFuturesUserSubscription(_logger, balanceUpdateHandler, orderUpdateHandler, positionUpdateHandler, riskLimitUpdateHandler, adlUpdateHandler, positionModeUpdateHandler);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }
    }
}
