using Mexc.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using CryptoExchange.Net;

namespace Mexc.Net.Clients.SpotApi
{
    internal class MexcSocketClientSpotSharedApi
        : SharedApiBase, 
        IMexcSocketClientSpotApiShared,
        IMexcSocketClientSpotSharedApi
    {
        private readonly MexcSocketClientSpotApi _api;

        private const string _topicId = "MexcSpot";
        private const string _exchangeName = "Mexc";

        public override  SharedClientInfo Discover() => SharedUtils.GetClientInfo(MexcExchange.Metadata, this);

        public MexcSocketClientSpotSharedApi(MexcSocketClientSpotApi api)
            : base(
                  api.Exchange,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeBalanceOptions,
                SubscribeSpotOrderOptions,
                SubscribeUserTradeOptions,
                SubscribeAllTickersOptions
                );
        }

        #region Trade client

        public SubscribeTradeOptions SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 18
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToTradeUpdatesAsync(symbols, 10, update => handler(update.ToType<SharedTrade[]>(update.Data.Select(x =>
            new SharedTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol), 
                update.Symbol!, 
                new SharedOrderQuantity(x.Quantity),
                x.Price, 
                x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray())), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Book Ticker client

        public SubscribeBookTickerOptions SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false)
        {
            MaxSymbolCount = 18,
            SupportsMultipleSymbols = true
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToBookTickerUpdatesAsync(symbols, update => handler(update.ToType(
                new SharedBookTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol), 
                    update.Symbol!,
                    update.Data.BestAskPrice,
                    new SharedOrderQuantity(update.Data.BestAskQuantity), 
                    update.Data.BestBidPrice,
                    new SharedOrderQuantity(update.Data.BestBidQuantity)))), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Kline client
        public SubscribeKlineOptions SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.ThreeMinutes,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 18
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;

            var validationError = SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToKlineUpdatesAsync(symbols, interval, update => handler(update.ToType(
                new SharedKline(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol), 
                    update.Symbol!, 
                    update.Data.StartTime,
                    update.Data.ClosePrice, 
                    update.Data.HighPrice, 
                    update.Data.LowPrice, 
                    update.Data.OpenPrice, 
                    new SharedOrderQuantity(update.Data.Volume, update.Data.QuoteVolume)))), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Order Book client
        public SubscribeOrderBookOptions SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 5, 10, 20 })
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 18
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToPartialOrderBookUpdatesAsync(symbols, request.Limit ?? 20, update => handler(
                update.ToType(
                    new SharedOrderBook(SharedQuantityType.BaseAsset, update.Data.SequenceEnd, update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Balance client
        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToAccountUpdatesAsync(
                update => handler(update.ToType<SharedBalance[]>(new[] { 
                    new SharedBalance(
                        SupportedTradingModes,
                        update.Data.Asset,
                        update.Data.Free, 
                        update.Data.Free + update.Data.Frozen) })),
                ct: ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Spot Order client

        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
            => await SubscribeToSpotOrderUpdatesAsync(request, x => handler(x.ToType<SharedSpotOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeSpotOrderOptions SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToOrderUpdatesAsync(
                update => handler(update.ToType<SharedSpotOrderUpdate[]>(new[] {
                        new SharedSpotOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol),
                            update.Symbol!,
                            update.Data.OrderId!,
                            update.Data.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : update.Data.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(update.Data.Status),
                            update.Data.Timestamp)
                        {
                            ClientOrderId = update.Data.ClientOrderId,
                            OrderPrice = update.Data.Price,
                            OrderQuantity = new SharedOrderQuantity(update.Data.Quantity == 0 ? null : update.Data.Quantity, update.Data.QuoteQuantity),
                            QuantityFilled = new SharedOrderQuantity(update.Data.CumulativeQuantity, update.Data.CumulativeQuoteQuantity),
                            AveragePrice = update.Data.AveragePrice == 0 ? null : update.Data.AveragePrice,
                            UpdateTime = update.Data.Timestamp,
                            TimeInForce = update.Data.OrderType == Enums.OrderType.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : update.Data.OrderType == Enums.OrderType.FillOrKill ? SharedTimeInForce.FillOrKill : null
                        }
                })),
                ct: ct).ConfigureAwait(false);
            return result;
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == Enums.OrderStatus.Canceled || status == Enums.OrderStatus.PartiallyCanceled)
                return SharedOrderStatus.Canceled;
            if (status == Enums.OrderStatus.New || status == Enums.OrderStatus.PartiallyFilled)
                return SharedOrderStatus.Open;
            if (status == OrderStatus.Filled)
                return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }
        #endregion

        #region User Trade client

        public SubscribeUserTradeOptions SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserTradeUpdatesAsync(
                update => handler(update.ToType<SharedUserTrade[]>(new[] {
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol),
                            update.Symbol!,
                            update.Data.OrderId,
                            update.Data.TradeId.ToString(),
                            update.Data.TradeSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            new SharedOrderQuantity(update.Data.Quantity, update.Data.QuoteQuantity),
                            update.Data.Price,
                            update.Data.TradeTime)
                        {
                            ClientOrderId = update.Data.ClientOrderId,
                            Role = update.Data.IsMaker ? SharedRole.Maker : SharedRole.Taker,
                            Fee = update.Data.Fee,
                            FeeAsset = update.Data.FeeAsset
                        }
                })),
                ct: ct).ConfigureAwait(false);
            return result;
        }


        #endregion

        #region Tickers client
        async Task<WebSocketResult<UpdateSubscription>> ISubscribeAllTickersOperation.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedTicker[]>> handler, CancellationToken ct)
            => await SubscribeToAllTickersUpdatesAsync(request, x => handler(x.ToType<SharedTicker[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickersOptions SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions(_exchangeName);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToAllMiniTickerUpdatesAsync(update => handler(update.ToType(update.Data.Select(x => 
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol), 
                    x.Symbol, 
                    x.LastPrice, 
                    x.HighPrice,
                    x.LowPrice, 
                    new SharedOrderQuantity(x.Volume, x.QuoteVolume), 
                    x.PriceChangePercentage)
                {
                }).ToArray())), ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Ticker client
        async Task<WebSocketResult<UpdateSubscription>> ISubscribeTickerOperation.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
            => await SubscribeToTickerUpdatesAsync(request, x => handler(x.ToType<SharedTicker>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickerOptions SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName) {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 18
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToMiniTickerUpdatesAsync(symbols, update => handler(update.ToType(
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                    update.Data.Symbol,
                    update.Data.LastPrice,
                    update.Data.HighPrice,
                    update.Data.LowPrice,
                    new SharedOrderQuantity(update.Data.Volume, update.Data.QuoteVolume),
                    update.Data.PriceChangePercentage)
                {
                })), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

    }
}

