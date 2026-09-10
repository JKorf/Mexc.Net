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
        #region Place Futures Trigger Order

        async Task<ICallResult<SharedId>> IPlaceFuturesTriggerOrder.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
            => await PlaceFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false)
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<PlaceFuturesTriggerOrderRequest>.Required(x => x.MarginMode),
                RequestParameterRuleOverride<PlaceFuturesTriggerOrderRequest>.NotSupported(x => x.ReduceOnly),
            ]
        };
        public async Task<HttpResult<SharedId>> PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetTriggerOrderParameters(request.PositionSide, request.OrderDirection);
            var validationError = PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlacePlanOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                side: side,
                orderType: request.OrderPrice == null ? FuturesOrderType.Market : FuturesOrderType.Limit,
                executeCycle: ExecuteCycle.OneWeek,
                triggerType: request.PriceDirection == SharedTriggerPriceDirection.PriceBelow ? TriggerType.LessThanOrEqual : TriggerType.MoreThanOrEqual,
                marginType: request.MarginMode == SharedMarginMode.Isolated ? MarginType.Isolated : MarginType.Cross,
                leverage: (int?)request.Leverage,
                quantity: request.Quantity.QuantityInContracts ?? 0,
                triggerPrice: request.TriggerPrice,
                price: request.OrderPrice,
                triggerPriceType: GetWorkingType(request),
                reduceOnly: request.ReduceOnly,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.ToString()));
        }

        #endregion

        private TriggerPriceType GetWorkingType(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.TriggerPriceType == null)
                return TriggerPriceType.LastPrice;

            if (request.TriggerPriceType == SharedTriggerPriceType.LastPrice)
                return TriggerPriceType.LastPrice;

            if (request.TriggerPriceType == SharedTriggerPriceType.MarkPrice)
                return TriggerPriceType.MarkPrice;

            return TriggerPriceType.IndexPrice;
        }

        #region Get Futures Trigger Order

        async Task<ICallResult<SharedFuturesTriggerOrder>> IGetFuturesTriggerOrder.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesTriggerOrderOptions GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesTriggerOrder>> GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var id))
                throw new ArgumentException($"Invalid order id");

            var orders = await _api.Trading.GetTriggerOrdersAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(orders);

            var order = orders.Data.SingleOrDefault(x => x.Id == id);
            if (order == null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(orders, new ServerError(new ErrorInfo(ErrorType.UnknownOrder, $"Order with id {id} not found")));

            MexcFuturesOrder? placedOrder = null;
            if (order.OrderId > 0)
            {
                var placedOrderResult = await _api.Trading.GetOrderAsync(order.OrderId, ct: ct).ConfigureAwait(false);
                if (!placedOrderResult.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(placedOrderResult, new ServerError(new ErrorInfo(ErrorType.UnknownOrder, $"Order with id {id} not found")));

                placedOrder = placedOrderResult.Data;
            }

            // Return
            return HttpResult.Ok(orders, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Symbol),
                order.Symbol,
                order.Id.ToString(),
                order.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                order.Side == FuturesOrderSide.OpenShort || order.Side == FuturesOrderSide.OpenLong ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerStatus(order),
                order.TriggerPrice,
                order.Side == FuturesOrderSide.OpenLong || order.Side == FuturesOrderSide.CloseLong ? SharedPositionSide.Long : SharedPositionSide.Short,
                order.CreateTime
                )
            {
                AveragePrice = placedOrder?.AveragePrice == 0 ? null : placedOrder?.AveragePrice,
                OrderPrice = order.Price == 0 ? null : order.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Quantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: placedOrder?.QuantityFilled),
                UpdateTime = order.UpdateTime,
                PlacedOrderId = order.OrderId.ToString(),
            });
        }

        #endregion

        private SharedTriggerOrderStatus ParseTriggerStatus(MexcFuturesTriggerOrder data)
        {
            if (data.Status == TpSlStatus.Executed)
                return SharedTriggerOrderStatus.Triggered;

            if (data.Status == TpSlStatus.Canceled || data.Status == TpSlStatus.Failed)
                return SharedTriggerOrderStatus.CanceledOrRejected;

            if (data.Status == TpSlStatus.Untriggered)
                return SharedTriggerOrderStatus.Active;

            return SharedTriggerOrderStatus.Unknown;
        }

        #region Cancel Futures Trigger Order

        async Task<ICallResult<SharedId>> ICancelFuturesTriggerOrder.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesTriggerOrderOptions CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelPlanOrdersAsync([new MexcCancelRequest { Symbol = request.Symbol!.GetSymbol(FormatSymbol), OrderId = orderId }], ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(orderId.ToString()));
        }

        #endregion

        private FuturesOrderSide GetTriggerOrderParameters(SharedPositionSide positionSide, SharedTriggerOrderDirection orderDirection)
        {
            if (orderDirection == SharedTriggerOrderDirection.Enter)
                return positionSide == SharedPositionSide.Long ? FuturesOrderSide.OpenLong : FuturesOrderSide.OpenShort;
            else
                // PriceAbove + Exit = TakeProfit Sell order
                return positionSide == SharedPositionSide.Long ? FuturesOrderSide.CloseLong : FuturesOrderSide.CloseShort;
        }

    }
}
