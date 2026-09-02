using Mexc.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using CryptoExchange.Net;

namespace Mexc.Net.Clients.FuturesApi
{
    internal class MexcSocketClientFuturesSharedApi :
        SharedApiBase,
        IMexcSocketClientFuturesApiShared,
        IMexcSocketClientFuturesSharedApi
    {
        private readonly MexcSocketClientFuturesApi _api;

        private const string _topicId = "MexcFutures";
        private const string _exchangeName = "Mexc";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(MexcExchange.Metadata, this);

        public MexcSocketClientFuturesSharedApi(MexcSocketClientFuturesApi api)
            : base(
                  api.Exchange,
                  [TradingMode.PerpetualLinear, TradingMode.PerpetualInverse],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeTickerOptions,
                SubscribeAllTickersOptions,
                SubscribeTradeOptions,
                SubscribeBalanceOptions,
                SubscribePositionOptions,
                SubscribeFuturesOrderOptions,
                SubscribeUserTradeOptions
                );
        }

        #region Kline client
        public SubscribeKlineOptions SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false, SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.EightHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.FuturesKlineInterval)request.Interval;

            var validationError = SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.ToType(
                new SharedKline(
                    request.Symbol,
                    symbol, 
                    update.Data.OpenTime,
                    update.Data.ClosePrice,
                    update.Data.HighPrice,
                    update.Data.LowPrice,
                    update.Data.OpenPrice, 
                    new SharedOrderQuantity(null, update.Data.Volume, update.Data.QuoteVolume)))), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Order Book client
        public SubscribeOrderBookOptions SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 5, 10, 20 });
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToPartialOrderBookUpdatesAsync(symbol, request.Limit ?? 5, update => handler(
                update.ToType(
                    new SharedOrderBook(SharedQuantityType.Contracts, update.Data.SequenceEnd, update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Ticker client
        async Task<WebSocketResult<UpdateSubscription>> ISubscribeTickerSocket.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
            => await SubscribeToTickerUpdatesAsync(request, x => handler(x.ToType<SharedTicker>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickerOptions SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToTickerUpdatesAsync(symbol, update => handler(update.ToType(
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol),
                    symbol,
                    update.Data.LastPrice,
                    update.Data.HighPrice,
                    update.Data.LowPrice,
                    new SharedOrderQuantity(null, update.Data.QuoteVolume24h, update.Data.Volume24h),
                    update.Data.ChangePercentage)

            {
            })), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Tickers client
        async Task<WebSocketResult<UpdateSubscription>> ISubscribeAllTickersSocket.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedTicker[]>> handler, CancellationToken ct)
            => await SubscribeToAllTickersUpdatesAsync(request, x => handler(x.ToType<SharedTicker[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickersOptions SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions(_exchangeName);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToTickersUpdatesAsync( update => handler(update.ToType(update.Data.Select(x => 
            new SharedSpotTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol, 
                x.LastPrice,
                x.HighPrice, 
                x.LowPrice,
                new SharedOrderQuantity(null, x.QuoteVolume24h, x.Volume24h),
                x.ChangePercentage)
            {
            }).ToArray())), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Trade client

        public SubscribeTradeOptions SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToTradeUpdatesAsync(symbol, update => handler(update.ToType(update.Data.Select(x =>            
                new SharedTrade(request.Symbol, symbol, new SharedOrderQuantity(contractQuantity: x.Quantity), x.Price, x.Timestamp)
                {
                    Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
                }
            ).ToArray())), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Balance client
        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                balanceUpdateHandler: update => handler(update.ToType<SharedBalance[]>([
                    new SharedBalance(
                        SupportedTradingModes,
                        update.Data.Asset,
                        update.Data.AvailableBalance,
                        update.Data.AvailableBalance + update.Data.FrozenBalance)])),
                ct: ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Position client
        public SubscribePositionOptions SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                positionUpdateHandler: update => handler(update.ToType<SharedPosition[]>([
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol, 
                        new SharedOrderQuantity(contractQuantity: update.Data.PositionSize), 
                        update.Data.UpdateTime)
                    {
                        Id = update.Data.PositionId.ToString(),
                        AverageOpenPrice = update.Data.HoldAveragePrice,
                        PositionMode = SharedPositionMode.HedgeMode,
                        PositionSide = update.Data.PositionSide == Enums.PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                        LiquidationPrice = update.Data.LiquidationPrice,
                        Leverage = update.Data.Leverage,
                        UnrealizedPnl = update.Data.Pnl
                    }])),
                ct: ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Futures Order client

        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
            => await SubscribeToFuturesOrderUpdatesAsync(request, x => handler(x.ToType<SharedFuturesOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeFuturesOrderOptions SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                orderUpdateHandler: update => handler(update.ToType(new[] {
                    new SharedFuturesOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol), update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        ParseOrderType(update.Data.OrderType),
                        (update.Data.OrderSide == FuturesOrderSide.OpenLong || update.Data.OrderSide == FuturesOrderSide.CloseShort) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.Status),
                        update.Data.UpdateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId,
                        OrderPrice = update.Data.Price == 0 ? null : update.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: update.Data.Quantity),
                        QuantityFilled = new SharedOrderQuantity(contractQuantity : update.Data.QuantityFilled),
                        UpdateTime = update.Data.UpdateTime,
                        TimeInForce = ParseTimeInForce(update.Data.OrderType),
#pragma warning disable CS0618 // Type or member is obsolete
                        Fee = update.Data.MakerFee + update.Data.TakerFee,
                        FeeAsset = update.Data.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                        Leverage = update.Data.Leverage,
                        StopLossPrice = update.Data.StopLossPrice,
                        TakeProfitPrice = update.Data.TakeProfitPrice,
                        AveragePrice = update.Data.AveragePrice == 0 ? null : update.Data.AveragePrice,
                        PositionSide = (update.Data.OrderSide == FuturesOrderSide.OpenLong || update.Data.OrderSide == FuturesOrderSide.CloseLong) ? SharedPositionSide.Long : SharedPositionSide.Short                        
                    }
                })),
                ct: ct).ConfigureAwait(false);
            return result;
        }

        private SharedOrderStatus ParseOrderStatus(FuturesOrderStatus status)
        {
            if (status == FuturesOrderStatus.Open)
                return SharedOrderStatus.Open;

            if (status == FuturesOrderStatus.Canceled || status == FuturesOrderStatus.Invalid)
                return SharedOrderStatus.Canceled;

            return SharedOrderStatus.Filled;
        }

        private SharedTimeInForce? ParseTimeInForce(FuturesOrderType orderType)
        {
            if (orderType == FuturesOrderType.ImmediateOrCancel || orderType == FuturesOrderType.Market)
                return SharedTimeInForce.ImmediateOrCancel;

            if (orderType == FuturesOrderType.FillOrKill)
                return SharedTimeInForce.FillOrKill;

            if (orderType == FuturesOrderType.Limit)
                return SharedTimeInForce.GoodTillCanceled;

            return null;
        }

        private SharedOrderType ParseOrderType(FuturesOrderType type)
        {
            if (type == FuturesOrderType.Market)
                return SharedOrderType.Market;

            if (type == FuturesOrderType.Limit)
                return SharedOrderType.Limit;

            if (type == FuturesOrderType.LimitMaker)
                return SharedOrderType.LimitMaker;

            return SharedOrderType.Other;
        }
        #endregion

        #region User Trade client
        public SubscribeUserTradeOptions SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                userTradeUpdate: update => handler(update.ToType<SharedUserTrade[]>(new[] {
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol) ?? ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        update.Data.Id.ToString(),
                        (update.Data.Side == FuturesOrderSide.OpenLong || update.Data.Side == FuturesOrderSide.CloseShort) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        new SharedOrderQuantity(contractQuantity: update.Data.Quantity),
                        update.Data.Price,
                        update.Data.Timestamp)
                    {
                        Role = update.Data.IsTaker ? SharedRole.Taker : SharedRole.Maker,
                        Fee = update.Data.Fee,
                        FeeAsset = update.Data.FeeAsset,
                    }
                })),
                ct: ct).ConfigureAwait(false);
            return result;
        }
        #endregion
    }
}
