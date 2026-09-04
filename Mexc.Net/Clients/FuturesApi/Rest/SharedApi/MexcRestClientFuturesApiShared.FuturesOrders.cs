using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Mexc.Net.Clients.SpotApi;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Clients.FuturesApi
{
    internal partial class MexcRestClientFuturesSharedApi
    {

        #region Place Futures Order

        async Task<ICallResult<SharedId>> IPlaceFuturesOrder.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
            => await PlaceFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public SharedFeeDeductionType FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        public SharedFeeAssetType FuturesFeeAssetType => SharedFeeAssetType.InputAsset;
        public SharedOrderType[] FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        public SharedTimeInForce[] FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        public SharedQuantitySupport FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        public string GenerateClientOrderId() => ExchangeHelpers.RandomString(20);

        public PlaceFuturesOrderOptions PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, false)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.PositionSide), typeof(SharedPositionSide), "Position side", SharedPositionSide.Long),
            }
        };
        public async Task<HttpResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    GetOrderSide(request.Side, request.PositionSide!.Value),
                    GetOrderType(request.OrderType, request.TimeInForce),
                    quantity: request.Quantity?.QuantityInContracts ?? 0,
                    price: request.Price,
                    leverage: (int?)request.Leverage,
                    marginType: request.MarginMode == SharedMarginMode.Isolated ? MarginType.Isolated : MarginType.Cross,
                    reduceOnly: request.ReduceOnly,
                    clientOrderId: request.ClientOrderId,
                    stopLossPrice: request.StopLossPrice,
                    takeProfitPrice: request.TakeProfitPrice,
                    ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
        }

        #endregion

        private FuturesOrderType GetOrderType(SharedOrderType orderType, SharedTimeInForce? timeInForce)
        {
            if (orderType == SharedOrderType.LimitMaker)
                return FuturesOrderType.LimitMaker;
            if (orderType == SharedOrderType.Market)
                return FuturesOrderType.Market;
            if (timeInForce == SharedTimeInForce.FillOrKill)
                return FuturesOrderType.FillOrKill;
            if (timeInForce == SharedTimeInForce.ImmediateOrCancel)
                return FuturesOrderType.ImmediateOrCancel;

            return FuturesOrderType.Limit;
        }

        private FuturesOrderSide GetOrderSide(SharedOrderSide side, SharedPositionSide posSide)
        {
            if (posSide == SharedPositionSide.Long)
            {
                if (side == SharedOrderSide.Buy) return FuturesOrderSide.OpenLong;
                return FuturesOrderSide.CloseLong;
            }

            if (side == SharedOrderSide.Buy) return FuturesOrderSide.CloseShort;
            return FuturesOrderSide.OpenShort;
        }

        #region Get Futures Order

        async Task<ICallResult<SharedFuturesOrder>> IGetFuturesOrder.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderOptions GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.GetOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                order.Data.OrderType == FuturesOrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                (order.Data.OrderSide == FuturesOrderSide.CloseShort || order.Data.OrderSide == FuturesOrderSide.OpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.Quantity)),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.QuantityFilled)),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = order.Data.OrderSide == FuturesOrderSide.OpenLong || order.Data.OrderSide == FuturesOrderSide.CloseLong ? SharedPositionSide.Long : SharedPositionSide.Short,
                Leverage = order.Data.Leverage,
                TakeProfitPrice = order.Data.TakeProfitPrice,
                StopLossPrice = order.Data.StopLossPrice,
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = order.Data.MakerFee + order.Data.TakerFee,
                FeeAsset = order.Data.FeeAsset
#pragma warning restore CS0618 // Type or member is obsolete
            });
        }

        #endregion

        #region Get Open Futures Orders

        async Task<ICallResult<SharedFuturesOrder[]>> IGetOpenFuturesOrders.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
            => await GetOpenFuturesOrdersAsync(request, ct).ConfigureAwait(false);

        public GetOpenFuturesOrdersOptions GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await _api.Trading.GetOpenOrdersAsync(ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(orders);

            var data = orders.Data.Where(x => x.Symbol == symbol).ToArray();
            return HttpResult.Ok(orders, data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId.ToString(),
                x.OrderType == FuturesOrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                (x.OrderSide == FuturesOrderSide.CloseShort || x.OrderSide == FuturesOrderSide.OpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                OrderPrice = x.Price == 0 ? null : x.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity)),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(x.QuantityFilled)),
                UpdateTime = x.UpdateTime,
                PositionSide = x.OrderSide == FuturesOrderSide.OpenLong || x.OrderSide == FuturesOrderSide.CloseLong ? SharedPositionSide.Long : SharedPositionSide.Short,
                Leverage = x.Leverage,
                TakeProfitPrice = x.TakeProfitPrice,
                StopLossPrice = x.StopLossPrice,
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = x.MakerFee + x.TakerFee,
                FeeAsset = x.FeeAsset
#pragma warning restore CS0618 // Type or member is obsolete
            }).ToArray());
        }

        #endregion

        #region Get Closed Futures Orders

        async Task<ICallResult<SharedFuturesOrder[]>> IGetClosedFuturesOrders.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetClosedFuturesOrdersAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetFuturesClosedOrdersOptions GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(90));

            // Get data
            var result = await _api.Trading.GetOrderHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                status: [FuturesOrderStatus.Canceled, FuturesOrderStatus.Filled],
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                page: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromPage(pageParams),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(90));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFuturesOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.OrderType == FuturesOrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                            (x.OrderSide == FuturesOrderSide.CloseShort || x.OrderSide == FuturesOrderSide.OpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(x.Status),
                            x.CreateTime)
                        {
                            ClientOrderId = x.ClientOrderId,
                            AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                            OrderPrice = x.Price == 0 ? null : x.Price,
                            OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity)),
                            QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(x.QuantityFilled)),
                            UpdateTime = x.UpdateTime,
                            PositionSide = x.OrderSide == FuturesOrderSide.OpenLong || x.OrderSide == FuturesOrderSide.CloseLong ? SharedPositionSide.Long : SharedPositionSide.Short,
                            Leverage = x.Leverage,
                            TakeProfitPrice = x.TakeProfitPrice,
                            StopLossPrice = x.StopLossPrice,
#pragma warning disable CS0618 // Type or member is obsolete
                            Fee = x.MakerFee + x.TakerFee,
                            FeeAsset = x.FeeAsset
#pragma warning restore CS0618 // Type or member is obsolete
                        })
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Get Futures Order Trades

        async Task<ICallResult<SharedUserTrade[]>> IGetFuturesOrderTrades.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
            => await GetFuturesOrderTradesAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderTradesOptions GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id"));

            var orders = await _api.Trading.GetOrderTradesAsync(orderId: orderId, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity)),
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.Taker ? SharedRole.Taker : SharedRole.Maker
            }).ToArray());
        }

        #endregion

        #region Get Futures User Trade History

        async Task<ICallResult<SharedUserTrade[]>> IGetFuturesUserTradeHistory.GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetFuturesUserTradeHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetFuturesUserTradeHistoryAsync(request, pageRequest, ct);
        GetFuturesUserTradeHistoryOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions => GetFuturesUserTradeHistoryOptions;

        public GetFuturesUserTradeHistoryOptions GetFuturesUserTradeHistoryOptions { get; } = new GetFuturesUserTradeHistoryOptions(_exchangeName, false, true, true, 1000);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFuturesUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(90));

            // Get data
            var result = await _api.Trading.GetUserTradesAsync(request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                page: pageParams.Page ?? 1,
                pageSize: pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromPage(pageParams),
                     result.Data.Length,
                     result.Data.Select(x => x.Timestamp),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(90));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.Id.ToString(),
                            x.Side == FuturesOrderSide.OpenLong || x.Side == FuturesOrderSide.CloseShort ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity)),
                            x.Price,
                            x.Timestamp)
                        {
                            Fee = x.Fee,
                            FeeAsset = x.FeeAsset,
                            Role = x.Taker ? SharedRole.Taker : SharedRole.Maker
                        })
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Cancel Futures Order

        async Task<ICallResult<SharedId>> ICancelFuturesOrder.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelOrdersAsync([orderId]).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.SingleOrDefault()?.OrderId.ToString()!));
        }

        #endregion

        #region Get Positions

        async Task<ICallResult<SharedPosition[]>> IGetPositions.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
            => await GetPositionsAsync(request, ct).ConfigureAwait(false);

        public GetPositionsOptions GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true);
        public async Task<HttpResult<SharedPosition[]>> GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);

            var result = await _api.Trading.GetPositionsAsync(request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPosition[]>(result);
            
            return HttpResult.Ok(result, result.Data.Select(x => 
            new SharedPosition(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol, 
                new SharedOrderQuantity(contractQuantity: Math.Abs(x.PositionSize)),
                x.UpdateTime)
            {
                Id = x.PositionId.ToString(),
                LiquidationPrice = x.LiquidationPrice,
                AverageOpenPrice = x.OpenAveragePrice,
                Leverage = x.Leverage,
                PositionSide = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
            }).ToArray());
        }

        #endregion

        #region Close Position

        async Task<ICallResult<SharedId>> IClosePosition.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
            => await ClosePositionAsync(request, ct).ConfigureAwait(false);

        public ClosePositionOptions ClosePositionOptions { get; } = new ClosePositionOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ClosePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.PositionSide == SharedPositionSide.Long ? FuturesOrderSide.CloseLong : FuturesOrderSide.CloseShort,
                FuturesOrderType.Market,
                0,
                flashClose: true,
                reduceOnly: true,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
        }

        #endregion

        private SharedOrderStatus ParseOrderStatus(FuturesOrderStatus status)
        {
            if (status == FuturesOrderStatus.Open)
                return SharedOrderStatus.Open;

            if (status == FuturesOrderStatus.Filled)
                return SharedOrderStatus.Filled;

            if (status == FuturesOrderStatus.Canceled || status == FuturesOrderStatus.Invalid)
                return SharedOrderStatus.Canceled;

            return SharedOrderStatus.Unknown;
        }

        #region Get Futures Order By Client Order Id

        async Task<ICallResult<SharedFuturesOrder>> IGetFuturesOrderByClientOrderId.GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderByClientOrderIdOptions GetFuturesOrderByClientOrderIdOptions { get; } = new GetFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderByClientOrderIdAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                order.Data.OrderType == FuturesOrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                (order.Data.OrderSide == FuturesOrderSide.CloseShort || order.Data.OrderSide == FuturesOrderSide.OpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.Quantity)),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.QuantityFilled)),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = order.Data.OrderSide == FuturesOrderSide.OpenLong || order.Data.OrderSide == FuturesOrderSide.CloseLong ? SharedPositionSide.Long : SharedPositionSide.Short,
                Leverage = order.Data.Leverage,
                TakeProfitPrice = order.Data.TakeProfitPrice,
                StopLossPrice = order.Data.StopLossPrice,
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = order.Data.MakerFee + order.Data.TakerFee,
                FeeAsset = order.Data.FeeAsset
#pragma warning restore CS0618 // Type or member is obsolete
            });
        }

        #endregion

        #region Cancel Futures Order By Client Order Id

        async Task<ICallResult<SharedId>> ICancelFuturesOrderByClientOrderId.CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderByClientOrderIdOptions CancelFuturesOrderByClientOrderIdOptions { get; } = new CancelFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrdersByClientOrderIdsAsync(new[] { new MexcCancelRequest { ClientOrderId = request.OrderId, Symbol = request.Symbol!.GetSymbol(FormatSymbol) } }, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion
    }
}
