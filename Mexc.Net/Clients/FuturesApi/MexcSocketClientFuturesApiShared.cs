using Mexc.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using CryptoExchange.Net;

namespace Mexc.Net.Clients.FuturesApi
{
    internal partial class MexcSocketClientFuturesApi : IMexcSocketClientFuturesApiShared
    {
        private const string _topicId = "MexcFutures";
        private const string _exchangeName = "Mexc";

        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.PerpetualLinear };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(this);

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false, SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.EightHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth);
        async Task<WebSocketResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.FuturesKlineInterval)request.Interval;

            var validationError = SharedClient.SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.ToType(
                new SharedKline(request.Symbol, symbol, update.Data.OpenTime, update.Data.ClosePrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.OpenPrice, update.Data.Volume))), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 5, 10, 20 });
        async Task<WebSocketResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToPartialOrderBookUpdatesAsync(symbol, request.Limit ?? 5, update => handler(update.ToType(new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Ticker client
        SubscribeTickerOptions ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName);
        async Task<WebSocketResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.ToType(new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.Volume24h, update.Data.ChangePercentage)
            {
                QuoteVolume = update.Data.QuoteVolume24h
            })), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Tickers client
        SubscribeTickersOptions ITickersSocketClient.SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions(_exchangeName);
        async Task<WebSocketResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToTickersUpdatesAsync( update => handler(update.ToType(update.Data.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume24h, x.ChangePercentage)
            {
                QuoteVolume = x.QuoteVolume24h
            }).ToArray())), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Trade client

        SubscribeTradeOptions ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.ToType(update.Data.Select(x =>            
                new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.Timestamp)
                {
                    Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
                }
            ).ToArray())), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Balance client
        SubscribeBalanceOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToUserDataUpdatesAsync(
                balanceUpdateHandler: update => handler(update.ToType<SharedBalance[]>([new SharedBalance(update.Data.Asset, update.Data.AvailableBalance, update.Data.AvailableBalance + update.Data.FrozenBalance)])),
                ct: ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Position client
        SubscribePositionOptions IPositionSocketClient.SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToUserDataUpdatesAsync(
                positionUpdateHandler: update => handler(update.ToType<SharedPosition[]>([new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol), update.Data.Symbol, update.Data.PositionSize, update.Data.UpdateTime)
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

        SubscribeFuturesOrderOptions IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToUserDataUpdatesAsync(
                orderUpdateHandler: update => handler(update.ToType(new[] {
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol), update.Data.Symbol,
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
                        Fee = update.Data.MakerFee + update.Data.TakerFee,
                        FeeAsset = update.Data.FeeAsset,
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
        SubscribeUserTradeOptions IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToUserDataUpdatesAsync(
                userTradeUpdate: update => handler(update.ToType<SharedUserTrade[]>(new[] {
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol) ?? ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        update.Data.Id.ToString(),
                        (update.Data.Side == FuturesOrderSide.OpenLong || update.Data.Side == FuturesOrderSide.CloseShort) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        update.Data.Quantity,
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
