using Mexc.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using CryptoExchange.Net;

namespace Mexc.Net.Clients.SpotApi
{
    internal partial class MexcSocketClientSpotApi : IMexcSocketClientSpotApiShared
    {
        private const string _topicId = "MexcSpot";
        private const string _exchangeName = "Mexc";

        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(this);

        #region Trade client

        SubscribeTradeOptions ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 18
        };
        async Task<WebSocketResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToTradeUpdatesAsync(symbols, 10, update => handler(update.ToType<SharedTrade[]>(update.Data.Select(x =>
            new SharedTrade(ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol), update.Symbol!, x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray())), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Book Ticker client

        SubscribeBookTickerOptions IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false)
        {
            MaxSymbolCount = 18,
            SupportsMultipleSymbols = true
        };
        async Task<WebSocketResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToBookTickerUpdatesAsync(symbols, update => handler(update.ToType(new SharedBookTicker(ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol), update.Symbol!, update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false,
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
        async Task<WebSocketResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;

            var validationError = SharedClient.SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToKlineUpdatesAsync(symbols, interval, update => handler(update.ToType(
                new SharedKline(ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol), update.Symbol!, update.Data.StartTime, update.Data.ClosePrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.OpenPrice, update.Data.Volume))), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 5, 10, 20 })
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 18
        };
        async Task<WebSocketResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToPartialOrderBookUpdatesAsync(symbols, request.Limit ?? 20, update => handler(update.ToType(new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Balance client
        SubscribeBalanceOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
                {
                    new ParameterDescription(nameof(SubscribeBalancesRequest.ListenKey), typeof(string), "The listenkey for user data", "123123123")
                }
        };
        async Task<WebSocketResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToAccountUpdatesAsync(
                request.ListenKey!,
                update => handler(update.ToType<SharedBalance[]>(new[] { new SharedBalance(update.Data.Asset, update.Data.Free, update.Data.Free + update.Data.Frozen) })),
                ct: ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Spot Order client

        SubscribeSpotOrderOptions ISpotOrderSocketClient.SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, false)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
                {
                    new ParameterDescription(nameof(SubscribeSpotOrderRequest.ListenKey), typeof(string), "The listenkey for user data", "123123123")
                }
        };
        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToOrderUpdatesAsync(
                request.ListenKey!,
                update => handler(update.ToType<SharedSpotOrder[]>(new[] {
                        new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol),
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

        SubscribeUserTradeOptions IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, false)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
                {
                    new ParameterDescription(nameof(SubscribeUserTradeRequest.ListenKey), typeof(string), "The listenkey for user data", "123123123")
                }
        };
        async Task<WebSocketResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToUserTradeUpdatesAsync(
                request.ListenKey!,
                update => handler(update.ToType<SharedUserTrade[]>(new[] {
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol),
                            update.Symbol!,
                            update.Data.OrderId,
                            update.Data.TradeId.ToString(),
                            update.Data.TradeSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            update.Data.Quantity,
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
        SubscribeTickersOptions ITickersSocketClient.SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions(_exchangeName);
        async Task<WebSocketResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToAllMiniTickerUpdatesAsync(update => handler(update.ToType(update.Data.Select(x => 
                new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume, x.PriceChangePercentage)
                {
                    QuoteVolume = x.QuoteVolume
                }).ToArray())), ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Ticker client
        SubscribeTickerOptions ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName) {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 18
        };
        async Task<WebSocketResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToMiniTickerUpdatesAsync(symbols, update => handler(update.ToType(
                new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol), update.Data.Symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.Volume, update.Data.PriceChangePercentage)
                {
                    QuoteVolume = update.Data.QuoteVolume
                })), ct).ConfigureAwait(false);
            return result;
        }
        #endregion

    }
}

