using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects.Errors;

namespace Mexc.Net.Clients.SpotApi
{
    internal partial class MexcRestClientSpotApi : IMexcRestClientSpotApiShared
    {
        private const string _topicId = "MexcSpot";

        public string Exchange => MexcExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Kline client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(true, false, true, 500, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth);
        async Task<ExchangeWebResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 500;
            var direction = DataDirection.Ascending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, true);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetKlinesAsync(
                symbol,
                interval,
                pageParams.StartTime,
                pageParams.EndTime,
                pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedKline[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Max(x => x.OpenTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return result.AsExchangeResult(
                    Exchange,
                    request.Symbol.TradingMode,
                    ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Spot Symbol client
        EndpointOptions<GetSymbolsRequest> ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);

        async Task<ExchangeWebResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetExchangeInfoAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, null, default);

            var response = result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, TradingMode.Spot, result.Data.Symbols.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Name, s.IsSpotTradingAllowed && s.Status == SymbolStatus.Enabled)
            {
                PriceDecimals = s.QuoteAssetPrecision,
                QuantityDecimals = s.BaseAssetPrecision,
                QuantityStep = s.BaseQuantityPrecision,
                MinTradeQuantity = s.BaseQuantityPrecision,
                MinNotionalValue = s.QuoteQuantityPrecision
            }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, response.Data);
            return response;
        }

        async Task<ExchangeResult<SharedSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<SharedSymbol[]>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<SharedSymbol[]>(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, baseAsset));
        }

        async Task<ExchangeResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbol));
        }

        async Task<ExchangeResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbolName));
        }
        #endregion

        #region Ticker client

        GetTickerOptions ISpotTickerRestClient.GetSpotTickerOptions { get; } = new GetTickerOptions();
        async Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTickerAsync(symbol, ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, result.Data.LastPrice, result.Data.HighPrice, result.Data.LowPrice, result.Data.Volume, result.Data.PriceChangePercentage * 100)
            {
                QuoteVolume = result.Data.QuoteVolume
            });
        }

        GetTickersOptions ISpotTickerRestClient.GetSpotTickersOptions { get; } = new GetTickersOptions();
        async Task<ExchangeWebResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickersOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, TradingMode.Spot, result.Data.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume, x.PriceChangePercentage * 100)
            {
                QuoteVolume = x.QuoteVolume
            }).ToArray());
        }

        #endregion

        #region Book Ticker client

        EndpointOptions<GetBookTickerRequest> IBookTickerRestClient.GetBookTickerOptions { get; } = new EndpointOptions<GetBookTickerRequest>(false);
        async Task<ExchangeWebResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IBookTickerRestClient)this).GetBookTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBookTicker>(Exchange, validationError);

            var resultTicker = await ExchangeData.GetBookPricesAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<SharedBookTicker>(Exchange, null, default);

            var ticker = resultTicker.Data;
            return resultTicker.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, ticker.Symbol),
                ticker.Symbol,
                ticker.BestAskPrice ?? 0,
                ticker.BestAskQuantity ?? 0,
                ticker.BestBidPrice ?? 0,
                ticker.BestBidQuantity ?? 0));
        }

        #endregion

        #region Recent Trade client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(1000, false);

        async Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetRecentTradesAsync(
                symbol,
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedTrade[]>(Exchange, request.Symbol.TradingMode, result.Data.Select(x => 
            new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.IsBuyerMaker ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Balance client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(AccountTypeFilter.Spot);

        async Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBalance[]>(Exchange, validationError);

            var result = await Account.GetAccountInfoAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedBalance[]>(Exchange, TradingMode.Spot, result.Data.Balances.Select(x => new SharedBalance(x.Asset, x.Available, x.Total)).ToArray());
        }

        #endregion

        #region Spot Order client

        SharedFeeDeductionType ISpotOrderRestClient.SpotFeeDeductionType => SharedFeeDeductionType.AddToCost;
        SharedFeeAssetType ISpotOrderRestClient.SpotFeeAssetType => SharedFeeAssetType.QuoteAsset;
        SharedOrderType[] ISpotOrderRestClient.SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        SharedTimeInForce[] ISpotOrderRestClient.SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        string ISpotOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        SharedQuantitySupport ISpotOrderRestClient.SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAndQuoteAsset);

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions();
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).PlaceSpotOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.TradingMode,
                SupportedTradingModes,
                ((ISpotOrderRestClient)this).SpotSupportedOrderTypes,
                ((ISpotOrderRestClient)this).SpotSupportedTimeInForce,
                ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                request.Quantity?.QuantityInBaseAsset,
                request.Quantity?.QuantityInQuoteAsset,
                request.Price,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()));
        }

        EndpointOptions<GetOrderRequest> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId,
                ParseOrderType(order.Data.OrderType),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.Timestamp)
            {
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.OrderType),
                UpdateTime = order.Data.UpdateTime,
                TriggerPrice = order.Data.StopPrice
            });
        }

        EndpointOptions<GetOpenOrdersRequest> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var order = await Trading.GetOpenOrdersAsync(symbol: symbol, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

            return order.AsExchangeResult<SharedSpotOrder[]>(Exchange, TradingMode.Spot, order.Data.Select(x => new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                x.Symbol,
                x.OrderId,
                ParseOrderType(x.OrderType),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.Timestamp)
            {
                ClientOrderId = x.ClientOrderId,
                OrderPrice = x.Price,
                OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(x.OrderType),
                UpdateTime = x.UpdateTime,
                TriggerPrice = x.StopPrice
            }).ToArray());
        }
        
        GetClosedOrdersOptions ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new GetClosedOrdersOptions(false, true, true, 1000);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);
#warning max age 7 days

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await Trading.GetOrdersAsync(
                symbol: symbol,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

            var data = result.Data.Where(x => x.Status == OrderStatus.Filled || x.Status == OrderStatus.Canceled);
            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Timestamp)),
                     result.Data.Length,
                     result.Data.Select(x => x.Timestamp),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return result.AsExchangeResult(
                Exchange,
                request.Symbol.TradingMode,
                ExchangeHelpers.ApplyFilter(data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                .Select(x => 
                    new SharedSpotOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                        x.Symbol,
                        x.OrderId,
                        ParseOrderType(x.OrderType),
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x.Status),
                        x.Timestamp)
                    {
                        ClientOrderId = x.ClientOrderId,
                        OrderPrice = x.Price,
                        OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                        QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                        TimeInForce = ParseTimeInForce(x.OrderType),
                        UpdateTime = x.UpdateTime,
                        TriggerPrice = x.StopPrice
                    })
                .ToArray(), nextPageRequest);
        }

        EndpointOptions<GetOrderTradesRequest> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var order = await Trading.GetUserTradesAsync(symbol: symbol, request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            return order.AsExchangeResult<SharedUserTrade[]>(Exchange, TradingMode.Spot, order.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.IsBuyer ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                ClientOrderId = x.ClientOrderId,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.IsMaker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        GetUserTradesOptions ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new GetUserTradesOptions(false, true, true, 100)
        {
            MaxAge = TimeSpan.FromDays(30)
        };
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotUserTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await Trading.GetUserTradesAsync(
                symbol: symbol,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit : pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Timestamp)),
                     result.Data.Length,
                     result.Data.Select(x => x.Timestamp),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     maxAge: TimeSpan.FromDays(30));

            return result.AsExchangeResult(
                Exchange,
                request.Symbol.TradingMode,
                ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                .Select(x => 
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                        x.Symbol,
                        x.OrderId.ToString(),
                        x.Id.ToString(),
                        x.IsBuyer ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        x.Quantity,
                        x.Price,
                        x.Timestamp)
                    {
                        ClientOrderId = x.ClientOrderId,
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset,
                        Role = x.IsMaker ? SharedRole.Maker : SharedRole.Taker
                    })
                .ToArray(), nextPageRequest);
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.New || status == OrderStatus.PartiallyFilled) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.PartiallyCanceled) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;
            if (type == OrderType.LimitMaker) return SharedOrderType.LimitMaker;
            if (type == OrderType.Limit) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(OrderType type)
        {
            if (type == OrderType.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (type == OrderType.FillOrKill) return SharedTimeInForce.FillOrKill;

            return null;
        }

        private OrderType GetPlaceOrderType(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (type == SharedOrderType.Market) return OrderType.Market;
            if (type == SharedOrderType.LimitMaker) return OrderType.LimitMaker;
            if (tif == SharedTimeInForce.ImmediateOrCancel) return OrderType.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return OrderType.FillOrKill;

            return OrderType.Limit;
        }

        #endregion

        #region Asset client

        EndpointOptions<GetAssetRequest> IAssetsRestClient.GetAssetOptions { get; } = new EndpointOptions<GetAssetRequest>(false);
        async Task<ExchangeWebResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset>(Exchange, validationError);

            var assets = await Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset>(Exchange, null, default);

            var asset = assets.Data.SingleOrDefault(x => x.Asset.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (asset == null)
                return assets.AsExchangeError<SharedAsset>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, "Asset not found")));

            return assets.AsExchangeResult<SharedAsset>(Exchange, TradingMode.Spot, new SharedAsset(asset.Asset)
            {
                FullName = asset.AssetName,
                Networks = asset.Networks?.Select(x => new SharedAssetNetwork(x.Name)
                {
                    FullName = x.Network,
                    MinConfirmations = x.MinConfirmations,
                    DepositEnabled = x.DepositEnabled,
                    MinWithdrawQuantity = x.WithdrawMin,
                    WithdrawEnabled = x.WithdrawEnabled,
                    WithdrawFee = x.WithdrawFee,
                    ContractAddress = x.Contract
                }).ToArray()
            });
        }

        EndpointOptions<GetAssetsRequest> IAssetsRestClient.GetAssetsOptions { get; } = new EndpointOptions<GetAssetsRequest>(true);
        async Task<ExchangeWebResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset[]>(Exchange, validationError);

            var assets = await Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset[]>(Exchange, null, default);

            return assets.AsExchangeResult<SharedAsset[]>(Exchange, TradingMode.Spot, assets.Data.Select(x => new SharedAsset(x.Asset)
            {
                FullName = x.AssetName,
                Networks = x.Networks?.Select(x => new SharedAssetNetwork(x.Name)
                {
                    FullName = x.Network,
                    MinConfirmations = x.MinConfirmations,
                    DepositEnabled = x.DepositEnabled,
                    MinWithdrawQuantity = x.WithdrawMin,
                    WithdrawEnabled = x.WithdrawEnabled,
                    WithdrawFee = x.WithdrawFee,
                    ContractAddress = x.Contract
                }).ToArray()
            }).ToArray());
        }

        #endregion

        #region Deposit client

        EndpointOptions<GetDepositAddressesRequest> IDepositRestClient.GetDepositAddressesOptions { get; } = new EndpointOptions<GetDepositAddressesRequest>(true);
        async Task<ExchangeWebResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositAddressesOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await Account.GetDepositAddressesAsync(request.Asset, request.Network).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, null, default);

            return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, TradingMode.Spot, depositAddresses.Data.Select(x => new SharedDepositAddress(request.Asset, x.Address)
            {
                TagOrMemo = x.Memo,
                Network = x.Network
            }
            ).ToArray());
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(false, true, true, 1000)
        {
            MaxAge = TimeSpan.FromDays(83)
        };
        async Task<ExchangeWebResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDeposit[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

            // Get data
            var result = await Account.GetDepositHistoryAsync(
                request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedDeposit[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.InsertTime)),
                    result.Data.Length,
                    result.Data.Select(x => x.InsertTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams,
                    TimeSpan.FromDays(7),
                    TimeSpan.FromDays(83));

            return result.AsExchangeResult(
                Exchange,
                TradingMode.Spot,
                ExchangeHelpers.ApplyFilter(result.Data, x => x.InsertTime, request.StartTime, request.EndTime, direction)
                .Select(x => 
                    new SharedDeposit(
                        x.Asset,
                        x.Quantity, 
                        x.Status == DepositStatus.Success, 
                        x.InsertTime,
                        x.Status == DepositStatus.Success ? SharedTransferStatus.Completed
                        : x.Status == DepositStatus.Rejected ? SharedTransferStatus.Failed
                        : SharedTransferStatus.InProgress)
                    {
                        Network = x.Network,
                        TransactionId = x.TransactionId,
                        Tag = x.Memo
                    }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(1, 5000, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        // Doesn't return data if timestamp is specified?
        //        #region Trade History client

        //        async Task<ExchangeWebResult<SharedTrade[]>> ITradeHistoryRestClient.GetTradeHistoryAsync(GetTradeHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        //        {
        //            DateTime? fromTime = null;
        //            if (pageToken is DateTimeToken token)
        //                fromTime = token.LastTime;

        //#warning max timestamp dif is 1 hour
        //            // Get data
        //            var result = await ExchangeData.GetAggregatedTradeHistoryAsync(
        //                request.Symbol.GetSymbol(FormatSymbol),
        //                //startTime: fromTime ?? request.StartTime,
        //                //endTime: request.EndTime,
        //                limit: 1000,
        //                ct: ct).ConfigureAwait(false);
        //            if (!result)
        //                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

        //            DateTimeToken? nextToken = null;
        //            if (result.Data.Count() == 1000)
        //                nextToken = new DateTimeToken(result.Data.Max(x => x.Timestamp));

        //            // Return
        //            return result.AsExchangeResult(Exchange, result.Data.Where(x => x.Timestamp < request.EndTime).Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)), nextToken);
        //        }
        //        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(false, true, true, 1000);
        async Task<ExchangeWebResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IWithdrawalRestClient)this).GetWithdrawalsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedWithdrawal[]>(Exchange, validationError);

#warning max age 90 days

            // Determine page token
            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

            // Get data
            var result = await Account.GetWithdrawHistoryAsync(
                request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedWithdrawal[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.ApplyTime)),
                    result.Data.Length,
                    result.Data.Select(x => x.ApplyTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams,
                    TimeSpan.FromDays(7));

            return result.AsExchangeResult(
                Exchange,
                TradingMode.Spot,
                ExchangeHelpers.ApplyFilter(result.Data, x => x.ApplyTime, request.StartTime, request.EndTime, direction)
                .Select(x =>
                    new SharedWithdrawal(x.Asset, x.Address ?? string.Empty, x.Quantity, x.Status == WithdrawStatus.Success, x.ApplyTime)
                    {
                        Id = x.Id,
                        Confirmations = x.Confirmations,
                        Network = x.Network,
                        Tag = x.Memo,
                        TransactionId = x.TransactionId,
                        Fee = x.TransactionFee
                    })
                .ToArray(), nextPageRequest);
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions();

        public EndpointOptions GetAssetOptions => throw new NotImplementedException();

        async Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var withdrawal = await Account.WithdrawAsync(
                request.Asset,
                request.Address,
                request.Quantity,
                network: request.Network,
                memo: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal)
                return withdrawal.AsExchangeResult<SharedId>(Exchange, null, default);

            return withdrawal.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(withdrawal.Data.Id));
        }

        #endregion

        #region Listen Key client

        EndpointOptions<StartListenKeyRequest> IListenKeyRestClient.StartOptions { get; } = new EndpointOptions<StartListenKeyRequest>(true);
        async Task<ExchangeWebResult<string>> IListenKeyRestClient.StartListenKeyAsync(StartListenKeyRequest request, CancellationToken ct)
        {
            var validationError = ((IListenKeyRestClient)this).StartOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<string>(Exchange, validationError);

            // Get data
            var result = await Account.StartUserStreamAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<string>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, result.Data);
        }
        EndpointOptions<KeepAliveListenKeyRequest> IListenKeyRestClient.KeepAliveOptions { get; } = new EndpointOptions<KeepAliveListenKeyRequest>(true);
        async Task<ExchangeWebResult<string>> IListenKeyRestClient.KeepAliveListenKeyAsync(KeepAliveListenKeyRequest request, CancellationToken ct)
        {
            var validationError = ((IListenKeyRestClient)this).KeepAliveOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<string>(Exchange, validationError);

            // Get data
            var result = await Account.KeepAliveUserStreamAsync(request.ListenKey, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<string>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, request.ListenKey);
        }

        EndpointOptions<StopListenKeyRequest> IListenKeyRestClient.StopOptions { get; } = new EndpointOptions<StopListenKeyRequest>(true);
        async Task<ExchangeWebResult<string>> IListenKeyRestClient.StopListenKeyAsync(StopListenKeyRequest request, CancellationToken ct)
        {
            var validationError = ((IListenKeyRestClient)this).StopOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<string>(Exchange, validationError);

            // Get data
            var result = await Account.StopUserStreamAsync(request.ListenKey, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<string>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, request.ListenKey);
        }
        #endregion

        #region Fee Client
        EndpointOptions<GetFeeRequest> IFeeRestClient.GetFeeOptions { get; } = new EndpointOptions<GetFeeRequest>(true);

        async Task<ExchangeWebResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = ((IFeeRestClient)this).GetFeeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetTradeFeeAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFee>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedFee(result.Data.MakerFee * 100, result.Data.TakerFee * 100));
        }
        #endregion

        #region Transfer client

        TransferOptions ITransferRestClient.TransferOptions { get; } = new TransferOptions([
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.DeliveryInverseFutures,
            SharedAccountType.Spot
            ]);
        async Task<ExchangeWebResult<SharedId>> ITransferRestClient.TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = ((ITransferRestClient)this).TransferOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var fromType = GetTransferType(request.FromAccountType);
            var toType = GetTransferType(request.ToAccountType);
            if (fromType == null || toType == null)
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            // Get data
            var transfer = await Account.TransferAsync(
                request.Asset,
                fromType.Value,
                toType.Value,
                request.Quantity,
                ct: ct).ConfigureAwait(false);
            if (!transfer)
                return transfer.AsExchangeResult<SharedId>(Exchange, null, default);

            return transfer.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(transfer.Data.TransferId));
        }

        private AccountType? GetTransferType(SharedAccountType type)
        {
            if (type == SharedAccountType.Spot) return AccountType.Spot;
            if (type.IsFuturesAccount()) return AccountType.Futures;
            return null;
        }

        #endregion
    }
}
