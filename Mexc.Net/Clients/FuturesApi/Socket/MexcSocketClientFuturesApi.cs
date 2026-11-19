using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Mexc.Net.Clients.MessageHandlers;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Objects.Models.Protobuf.Futures;
using Mexc.Net.Objects.Options;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Queries;
using Mexc.Net.Objects.Sockets.Subscriptions;
using System.Net.WebSockets;

namespace Mexc.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal partial class MexcSocketClientFuturesApi : SocketApiClient<MexcEnvironment, MexcFuturesAuthenticationProvider, MexcCredentials>, IMexcSocketClientFuturesApi
    {
        private readonly MexcSocketClientFuturesSharedApi _sharedApi;

        private readonly string _address;

        #region constructor/destructor

        internal MexcSocketClientFuturesApi(ILoggerFactory? loggerFactory, MexcSocketOptions options) :
            base(loggerFactory, MexcExchange.Metadata.Id, options.Environment.FuturesSocketAddress, options, options.FuturesOptions)
        {
            AddSystemSubscription(new MexcErrorSubscription(_logger));

            _sharedApi = new MexcSocketClientFuturesSharedApi(this);

            MaxIndividualSubscriptionsPerConnection = 30;

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

            _address = options.Environment.FuturesSocketAddress.AppendPath("/edge?Trans-Protocol=Protobuf");
        }

        #endregion

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange._serializerContext));

        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType)
        {
            if (messageType == WebSocketMessageType.Text)
                return new MexcSocketFuturesMessageHandler();

            return new MexcFuturesProtobufMessageHandler();
        }

        /// <inheritdoc />
        protected override MexcFuturesAuthenticationProvider CreateAuthenticationProvider(MexcCredentials credentials)
            => new MexcFuturesAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => MexcExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        public IMexcSocketClientFuturesApiShared SharedClient => _sharedApi;
        public IMexcSocketClientFuturesSharedApi SharedApi => _sharedApi;

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickersUpdatesAsync(Action<DataEvent<MexcFuturesTickerUpdate[]>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateFuturesTickers>((receiveTime, originalData, data) =>
            {
                var timestamp = DateTimeConverter.ConvertFromMilliseconds(data.CreateTime);
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<MexcFuturesTickerUpdate[]>(MexcExchange.ExchangeName, data.Tickers.Select(x => x.ToModel()).ToArray(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcFuturesSubscription<MexcUpdateFuturesTickers>(_logger, "tickers", null, null, null, internalHandler, false);
            return await SubscribeAsync(_address, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesDetailTickerUpdate>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateFuturesDetailTicker>((receiveTime, originalData, data) =>
            {
                var timestamp = DateTimeConverter.ConvertFromMilliseconds(data.CreateTime);
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<MexcFuturesDetailTickerUpdate>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcFuturesSubscription<MexcUpdateFuturesDetailTicker>(_logger, "ticker", symbol, null, null, internalHandler, false);
            return await SubscribeAsync(_address, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesTradeUpdate[]>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateFuturesTrade>((receiveTime, originalData, data) =>
            {
                var timestamp = DateTimeConverter.ConvertFromMilliseconds(data.CreateTime);
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<MexcFuturesTradeUpdate[]>(MexcExchange.ExchangeName, [data.ToModel()], receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcFuturesSubscription<MexcUpdateFuturesTrade>(_logger, "deal", symbol, null, null, internalHandler, false);
            return await SubscribeAsync(_address, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, FuturesKlineInterval interval, Action<DataEvent<MexcFuturesStreamKline>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateFuturesKline>((receiveTime, originalData, data) =>
            {
                var timestamp = DateTimeConverter.ConvertFromMilliseconds(data.CreateTime);
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<MexcFuturesStreamKline>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcFuturesSubscription<MexcUpdateFuturesKline>(_logger, "kline", symbol, interval, null, internalHandler, false);
            return await SubscribeAsync(_address, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesOrderBook>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateFuturesOrderBook>((receiveTime, originalData, data) =>
            {
                var model = data.ToModel();
                UpdateTimeOffset(model.Timestamp!.Value);

                handler(
                    new DataEvent<MexcFuturesOrderBook>(MexcExchange.ExchangeName, model, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(model.Timestamp, GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                        .WithSequenceNumber(model.SequenceEnd)
                    );
            });

            var subscription = new MexcFuturesSubscription<MexcUpdateFuturesOrderBook>(_logger, "depth", symbol, null, null, internalHandler, false);
            return await SubscribeAsync(_address, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int? limit, Action<DataEvent<MexcFuturesOrderBook>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateFuturesOrderBook>((receiveTime, originalData, data) =>
            {
                var model = data.ToModel();
                UpdateTimeOffset(model.Timestamp!.Value);

                handler(
                    new DataEvent<MexcFuturesOrderBook>(MexcExchange.ExchangeName, model, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(model.Timestamp, GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                        .WithSequenceNumber(model.SequenceEnd)
                    );
            });

            var subscription = new MexcFuturesSubscription<MexcUpdateFuturesOrderBook>(_logger, "depth.full", symbol, null, limit, internalHandler, false);
            return await SubscribeAsync(_address, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<MexcFundingRateUpdate>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateFuturesFundingRate>((receiveTime, originalData, data) =>
            {
                var timestamp = DateTimeConverter.ConvertFromMilliseconds(data.CreateTime);
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<MexcFundingRateUpdate>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcFuturesSubscription<MexcUpdateFuturesFundingRate>(_logger, "funding.rate", symbol, null, null, internalHandler, false);
            return await SubscribeAsync(_address, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<MexcPriceUpdate>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateFuturesIndexPrice>((receiveTime, originalData, data) =>
            {
                var timestamp = DateTimeConverter.ConvertFromMilliseconds(data.CreateTime);
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<MexcPriceUpdate>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcFuturesSubscription<MexcUpdateFuturesIndexPrice>(_logger, "index.price", symbol, null, null, internalHandler, false);
            return await SubscribeAsync(_address, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<MexcPriceUpdate>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateFuturesMarkPrice>((receiveTime, originalData, data) =>
            {
                var timestamp = DateTimeConverter.ConvertFromMilliseconds(data.CreateTime);
                UpdateTimeOffset(timestamp);

                handler(
                    new DataEvent<MexcPriceUpdate>(MexcExchange.ExchangeName, data.ToModel(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(data.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcFuturesSubscription<MexcUpdateFuturesMarkPrice>(_logger, "fair.price", symbol, null, null, internalHandler, false);
            return await SubscribeAsync(_address, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(Action<DataEvent<MexcContractUpdate>> handler, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, MexcUpdateFuturesContract>((receiveTime, originalData, data) =>
            {
                var timestamp = DateTimeConverter.ConvertFromMilliseconds(data.CreateTime);
                UpdateTimeOffset(timestamp);
                var model = data.ToModel();

                handler(
                    new DataEvent<MexcContractUpdate>(MexcExchange.ExchangeName, model, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(timestamp, GetTimeOffset())
                        .WithSymbol(model.Symbol)
                        .WithStreamId(data.Channel)
                    );
            });

            var subscription = new MexcFuturesSubscription<MexcUpdateFuturesContract>(_logger, "contract", null, null, null, internalHandler, false);
            return await SubscribeAsync(_address, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            Action<DataEvent<MexcFuturesBalanceUpdate>>? balanceUpdateHandler = null,
            Action<DataEvent<MexcFuturesOrderUpdate>>? orderUpdateHandler = null,
            Action<DataEvent<MexcPositionUpdate>>? positionUpdateHandler = null,
            Action<DataEvent<MexcRiskLimit>>? riskLimitUpdateHandler = null,
            Action<DataEvent<MexcPositionModeUpdate>>? positionModeUpdateHandler = null,
            Action<DataEvent<MexcPlanOrderUpdate>>? planOrderHandler = null,
            Action<DataEvent<MexcTpSlOrderUpdate>>? tpSlOrderHandler = null,
            Action<DataEvent<MexcTrailingOrder>>? trailingOrderHandler = null,
            Action<DataEvent<MexcTpSlPriceUpdate>>? tpSlPriceUpdate = null,
            Action<DataEvent<MexcFuturesUserTradeUpdate>>? userTradeUpdate = null,
            Action<DataEvent<MexcChaseOrderFailure>>? chaseOrderFailUpdate = null,
            Action<DataEvent<MexcLiquidationRiskUpdate>>? liquidationRiskUpdate = null,
            Action<DataEvent<MexcLeverageModeUpdate>>? leverageModeUpdate = null,
            Action<DataEvent<object>>? closeAllFailUpdate = null,
            Action<DataEvent<MexcReversePositionUpdate>>? reversePositionUpdate = null,
            Action<DataEvent<MexcLiquidationUpdate>>? liquidationUpdate = null,
            CancellationToken ct = default)
        {
            var subscription = new MexcFuturesUserSubscription(
                _logger, 
                this,
                balanceUpdateHandler,
                orderUpdateHandler,
                positionUpdateHandler,
                riskLimitUpdateHandler,
                positionModeUpdateHandler,
                planOrderHandler,
                tpSlOrderHandler,
                trailingOrderHandler,
                tpSlPriceUpdate,
                userTradeUpdate,
                chaseOrderFailUpdate,
                liquidationRiskUpdate,
                leverageModeUpdate,
                closeAllFailUpdate,
                reversePositionUpdate,
                liquidationUpdate);
            return await SubscribeAsync(_address, subscription, ct).ConfigureAwait(false);
        }
    }
}
