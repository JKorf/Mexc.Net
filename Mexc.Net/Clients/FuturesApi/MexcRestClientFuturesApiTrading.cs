using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.RateLimiting.Guards;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Futures;
using System;

namespace Mexc.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class MexcRestClientFuturesApiTrading : IMexcRestClientFuturesApiTrading
    {
        private readonly MexcRestClientFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal MexcRestClientFuturesApiTrading(MexcRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesOrder[]>> GetOpenOrdersAsync(int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("page_num", page);
            parameters.AddOptional("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/private/order/list/open_orders/", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesOrder[]>> GetOrderHistoryAsync(string? symbol = null, IEnumerable<FuturesOrderStatus>? status = null, OrderCategory? category = null, FuturesOrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("states", status == null ? null : string.Join(",", status));
            parameters.AddOptionalEnum("category", category);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalMilliseconds("start_time", startTime);
            parameters.AddOptionalMilliseconds("end_time", endTime);
            parameters.Add("page_num", page ?? 1);
            parameters.Add("page_size", pageSize ?? 20);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/private/order/list/history_orders", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order By Client Order Id

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesOrder>> GetOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/private/order/external/{symbol}/{clientOrderId}", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/private/order/get/{orderId}", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Orders By Id

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesOrder[]>> GetOrdersByIdAsync(IEnumerable<long> orderIds, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("order_ids", string.Join(",", orderIds));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/private/order/batch_query", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Trades

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesUserTrade[]>> GetOrderTradesAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/private/order/deal_details/{orderId}", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesUserTrade[]>> GetUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalMillisecondsString("start_time", startTime);
            parameters.AddOptionalMillisecondsString("end_time", endTime);
            parameters.AddOptional("page_num", page);
            parameters.AddOptional("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/private/order/list/order_deals/v3", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesTriggerOrder[]>> GetTriggerOrdersAsync(string? symbol = null, IEnumerable<FuturesOrderStatus>? status = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("states", status == null ? null : string.Join(",", status));
            parameters.AddOptionalMilliseconds("start_time", startTime);
            parameters.AddOptionalMilliseconds("end_time", endTime);
            parameters.Add("page_num", page ?? 1);
            parameters.Add("page_size", pageSize ?? 20);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/private/planorder/list/orders", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesTriggerOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Tp Sl Orders

        /// <inheritdoc />
        public async Task<WebCallResult<MexcStopOrder[]>> GetTpSlOrdersAsync(string? symbol = null, bool? finished = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("is_finished", finished);
            parameters.AddOptionalMilliseconds("start_time", startTime);
            parameters.AddOptionalMilliseconds("end_time", endTime);
            parameters.AddOptional("page_num", page);
            parameters.AddOptional("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/private/stoporder/list/orders", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcStopOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Risk Limits

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, MexcRiskLimit[]>>> GetRiskLimitsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/private/account/risk_limit", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<Dictionary<string, MexcRiskLimit[]>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position History

        /// <inheritdoc />
        public async Task<WebCallResult<MexcPosition[]>> GetPositionHistoryAsync(string? symbol = null, PositionSide? positionSide = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("positionSide", positionSide);
            parameters.Add("page_num", page ?? 1);
            parameters.Add("page_size", pageSize ?? 20);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/private/position/list/history_positions", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<WebCallResult<MexcPosition[]>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/private/position/open_positions", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesOrderResult>> PlaceOrderAsync(
            string symbol,
            FuturesOrderSide side,
            FuturesOrderType type, 
            decimal quantity,
            decimal? price = null,
            int? leverage = null,
            MarginType? marginType = null,
            string? clientOrderId = null, 
            long? positionId = null,
            decimal? stopLossPrice = null,
            decimal? takeProfitPrice = null,
            TriggerPriceType? stopLossPriceType = null, 
            TriggerPriceType? takeProfitPriceType = null,
            bool? priceProtect = null,
            PositionMode? positionMode = null, 
            bool? reduceOnly = null,
            bool? marketCeiling = null, 
            bool? flashClose = null, 
            LimitOrderType? limitOrderType = null,
            StpMode? stpMode = null, 
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnumAsInt("side", side);
            parameters.AddEnumAsInt("type", type);
            parameters.Add("vol", quantity);
            parameters.AddOptional("price", price);
            parameters.AddOptional("leverage", leverage);
            parameters.AddOptionalEnumAsInt("openType", marginType);
            parameters.AddOptional("externalOid", clientOrderId);
            parameters.AddOptional("positionId", positionId);
            parameters.AddOptional("stopLossPrice", stopLossPrice);
            parameters.AddOptional("takeProfitPrice", takeProfitPrice);
            parameters.AddOptionalEnumAsInt("lossTrend", stopLossPriceType);
            parameters.AddOptionalEnumAsInt("profitTrend", takeProfitPriceType);
            parameters.AddOptional("priceProtect", priceProtect == null ? null : priceProtect.Value ? 1 : 0);
            parameters.AddOptionalEnumAsInt("positionMode", positionMode);
            parameters.AddOptional("reduceOnly", reduceOnly);
            parameters.AddOptional("marketCeiling", marketCeiling);
            parameters.AddOptional("flashClose", flashClose);
            parameters.AddOptionalEnumAsInt("bboTypeNum", limitOrderType);
            parameters.AddOptionalEnumAsInt("stpMode", stpMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/order/create", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<CallResult<MexcFuturesBatchOrderResult>[]>> PlaceMultipleOrdersAsync(
            IEnumerable<MexcFuturesOrderRequest> orders,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/order/submit_batch", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var response = await _baseClient.SendAsync<MexcFuturesBatchOrderResult[]>(request, parameters, ct).ConfigureAwait(false);
            if (!response.Success)
                return response.As<CallResult<MexcFuturesBatchOrderResult>[]>(default);

            var result = new List<CallResult<MexcFuturesBatchOrderResult>>();
            foreach (var item in response.Data)
            {
                result.Add(item.ErrorCode != 0
                    ? new CallResult<MexcFuturesBatchOrderResult>(new ServerError(item.ErrorCode.ToString(), _baseClient.GetErrorInfo(item.ErrorCode, "")))
                    : new CallResult<MexcFuturesBatchOrderResult>(item));
            }

            if (result.All(x => !x.Success))
                return response.AsErrorWithData(new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, false, "All orders failed")), result.ToArray());

            return response.As(result.ToArray());
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<WebCallResult<MexcCancelResult[]>> CancelOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(orderIds.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/order/cancel", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcCancelResult[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Chase Order

        /// <inheritdoc />
        public async Task<WebCallResult> ChaseOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("orderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/order/chase_limit_order", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<WebCallResult> EditOrderAsync(long orderId,
            decimal quantity,
            decimal price,
        CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("orderId", orderId);
            parameters.Add("vol", quantity);
            parameters.Add("price", price);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/order/change_limit_order", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Orders By Client Order Ids

        /// <inheritdoc />
        public async Task<WebCallResult<MexcCancelResult[]>> CancelOrdersByClientOrderIdsAsync(IEnumerable<MexcCancelRequest> orders, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/order/batch_cancel_with_external", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcCancelResult[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/order/cancel_all", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Reverse Position

        /// <inheritdoc />
        public async Task<WebCallResult> ReversePositionAsync(
            string symbol,
            long positionId,
            decimal quantity,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("positionId", positionId);
            parameters.Add("vol", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/position/reverse", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close All Positions

        /// <inheritdoc />
        public async Task<WebCallResult> CloseAllPositionsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/position/close_all", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Order Counts

        /// <inheritdoc />
        public async Task<WebCallResult<MexcOrderCount>> GetOpenOrderCountsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/private/order/open_order_total_count", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcOrderCount>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Plan Order

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PlacePlanOrderAsync(
            string symbol,
            FuturesOrderSide side,
            FuturesOrderType orderType,
            decimal quantity,
            MarginType marginType,
            decimal triggerPrice,
            TriggerType triggerType,
            ExecuteCycle executeCycle,
            TriggerPriceType triggerPriceType,
            int? leverage = null,
            decimal? price = null,
            bool? priceProtect = null,
            PositionMode? positionMode = null,
            TriggerPriceType? stopLossPriceType = null,
            TriggerPriceType? takeProfitPriceType = null,
            decimal? stopLossPrice = null,
            decimal? takeProfitPrice = null,
            bool? reduceOnly = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnumAsInt("side", side);
            parameters.AddEnumAsInt("orderType", orderType);
            parameters.Add("vol", quantity);
            parameters.AddOptional("leverage", leverage);
            parameters.AddEnumAsInt("openType", marginType);
            parameters.Add("triggerPrice", triggerPrice);
            parameters.AddEnumAsInt("triggerType", triggerType);
            parameters.AddEnumAsInt("executeCycle", executeCycle);
            parameters.AddEnumAsInt("trend", triggerPriceType);
            parameters.AddOptional("price", price);
            parameters.AddOptional("priceProtect", priceProtect);
            parameters.AddOptionalEnumAsInt("positionMode", positionMode);
            parameters.AddOptionalEnumAsInt("lossTrend", stopLossPriceType);
            parameters.AddOptionalEnumAsInt("profitTrend", takeProfitPriceType);
            parameters.AddOptional("stopLossPrice", stopLossPrice);
            parameters.AddOptional("takeProfitPrice", takeProfitPrice);
            parameters.AddOptional("reduceOnly", reduceOnly);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/planorder/place/v2", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Plan Order

        /// <inheritdoc />
        public async Task<WebCallResult> EditPlanOrderAsync(
            string symbol,
            long orderId,
            decimal triggerPrice,
            decimal price,
            FuturesOrderType orderType,
            TriggerType triggerType,
            TriggerPriceType triggerPriceType,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("triggerPrice", triggerPrice);
            parameters.Add("price", price);
            parameters.AddEnum("orderType", orderType);
            parameters.AddEnum("triggerType", triggerType);
            parameters.AddEnum("trend", triggerPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/planorder/change_price", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Plan Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelPlanOrdersAsync(IEnumerable<MexcCancelRequest> orders, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/planorder/cancel", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Planned Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllPlannedOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/planorder/cancel_all", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Tp Sl Order

        /// <inheritdoc />
        public async Task<WebCallResult> PlaceTpSlOrderAsync(
            long positionId,
            decimal quantity,
            TriggerPriceType stopLossPriceType,
            TriggerPriceType takeProfitPriceType,
            decimal? stopLossPrice = null,
            decimal? takeProfitPrice = null,
            ProfitLossVolumeType? profitLossVolumeType = null,
            decimal? takeProfitVolume = null,
            decimal? stopLossVolume = null,
            VolumeType? volumeType = null,
            bool? takeProfitReverse = null,
            bool? stopLossReverse = null,
            TpSlType? takeProfitType = null,
            decimal? takeProfitOrderPrice = null,
            TpSlType? stopLossType = null,
            decimal? stopLossOrderPrice = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("positionId", positionId);
            parameters.Add("vol", quantity);
            parameters.AddEnumAsInt("lossTrend", stopLossPriceType);
            parameters.AddEnumAsInt("profitTrend", takeProfitPriceType);
            parameters.AddOptional("stopLossPrice", stopLossPrice);
            parameters.AddOptional("takeProfitPrice", takeProfitPrice);
            parameters.AddOptionalEnumAsInt("profitLossVolType", profitLossVolumeType);
            parameters.AddOptional("takeProfitVol", takeProfitVolume);
            parameters.AddOptional("stopLossVol", stopLossVolume);
            parameters.AddOptionalEnumAsInt("volType", volumeType);
            parameters.AddOptional("takeProfitReverse", takeProfitReverse == null ? null : takeProfitReverse.Value ? 1 : 2);
            parameters.AddOptional("stopLossReverse", stopLossReverse == null ? null : stopLossReverse.Value ? 1 : 2);
            parameters.AddOptionalEnumAsInt("takeProfitType", takeProfitType);
            parameters.AddOptional("takeProfitOrderPrice", takeProfitOrderPrice);
            parameters.AddOptionalEnumAsInt("stopLossType", stopLossType);
            parameters.AddOptional("stopLossOrderPrice", stopLossOrderPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/stoporder/place", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Tp Sl Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelTpSlOrdersAsync(IEnumerable<MexcCancelRequest> orders, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/stoporder/cancel", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Tp Sl Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllTpSlOrdersAsync(
            long? positionId = null,
            string? symbol = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("positionId", positionId);
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/stoporder/cancel_all", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Limit Order Tp Sl

        /// <inheritdoc />
        public async Task<WebCallResult> EditLimitOrderTpSlAsync(
            long orderId,
            TriggerPriceType? stopLossPriceType = null,
            TriggerPriceType? takeProfitPriceType = null,
            decimal? stopLossPrice = null,
            decimal? takeProfitPrice = null,
            bool? takeProfitReverse = null,
            bool? stopLossReverse = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("orderId", orderId);
            parameters.AddOptionalEnumAsInt("lossTrend", stopLossPriceType);
            parameters.AddOptionalEnumAsInt("profitTrend", takeProfitPriceType);
            parameters.AddOptional("stopLossPrice", stopLossPrice);
            parameters.AddOptional("takeProfitPrice", takeProfitPrice);
            parameters.AddOptional("takeProfitReverse", takeProfitReverse == null ? null : takeProfitReverse.Value ? 1 : 2);
            parameters.AddOptional("stopLossReverse", stopLossReverse == null ? null : stopLossReverse.Value ? 1 : 2);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/stoporder/change_price", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Tp Sl Order

        /// <inheritdoc />
        public async Task<WebCallResult> EditTpSlOrderAsync(
            long stopPlanOrderId,
            TriggerPriceType? stopLossPriceType = null,
            TriggerPriceType? takeProfitPriceType = null,
            decimal? stopLossPrice = null,
            decimal? takeProfitPrice = null,
            bool? takeProfitReverse = null,
            bool? stopLossReverse = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("stopPlanOrderId", stopPlanOrderId);
            parameters.AddOptionalEnumAsInt("lossTrend", stopLossPriceType);
            parameters.AddOptionalEnumAsInt("profitTrend", takeProfitPriceType);
            parameters.AddOptional("stopLossPrice", stopLossPrice);
            parameters.AddOptional("takeProfitPrice", takeProfitPrice);
            parameters.AddOptional("takeProfitReverse", takeProfitReverse == null ? null : takeProfitReverse.Value ? 1 : 2);
            parameters.AddOptional("stopLossReverse", stopLossReverse == null ? null : stopLossReverse.Value ? 1 : 2);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/stoporder/change_plan_price", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Tp Sl Orders

        /// <inheritdoc />
        public async Task<WebCallResult<MexcTpSlOrder[]>> GetOpenTpSlOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/private/stoporder/open_orders", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcTpSlOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Trailing Order

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PlaceTrailingOrderAsync(
            string symbol,
            FuturesOrderSide side,
            decimal quantity,
            int leverage,
            MarginType marginType,
            TriggerPriceType triggerType,
            CallbackType callbackType,
            decimal callbackValue,
            PositionMode positionMode,
            bool reduceOnly,
            decimal? activationPrice = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnumAsInt("side", side);
            parameters.Add("vol", quantity);
            parameters.Add("leverage", leverage);
            parameters.AddEnumAsInt("openType", marginType);
            parameters.AddEnumAsInt("trend", triggerType);
            parameters.AddEnumAsInt("backType", callbackType);
            parameters.Add("backValue", callbackValue);
            parameters.AddEnumAsInt("positionMode", positionMode);
            parameters.Add("reduceOnly", reduceOnly);
            parameters.AddOptional("activePrice", activationPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/trackorder/place", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Trailing Order

        /// <inheritdoc />
        public async Task<WebCallResult> CancelTrailingOrderAsync(
            string symbol,
            long orderId,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("trackOrderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/trackorder/cancel", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Trailing Order

        /// <inheritdoc />
        public async Task<WebCallResult> EditTrailingOrderAsync(
            string symbol,
            long orderId,
            TriggerPriceType triggerType,
            CallbackType callbackType,
            decimal callbackValue,
            decimal quantity,
            decimal? activationPrice = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("trackOrderId", orderId);
            parameters.AddEnum("trend", triggerType);
            parameters.AddEnum("backType", callbackType);
            parameters.Add("backValue", callbackValue);
            parameters.Add("vol", quantity);
            parameters.AddOptional("activePrice", activationPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/private/trackorder/change_order", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trailing Orders

        /// <inheritdoc />
        public async Task<WebCallResult<MexcTrailingOrder[]>> GetTrailingOrdersAsync(
            string? symbol = null,
            IEnumerable<TpSlStatus>? status = null,
            FuturesOrderSide? side = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalCommaSeparated("states", (status ?? [TpSlStatus.Untriggered, TpSlStatus.Untriggered, TpSlStatus.Executed]).Select(x => EnumConverter.GetString(x)).ToArray());
            parameters.AddOptionalEnumAsInt("side", side);
            parameters.AddOptionalMilliseconds("start_time", startTime);
            parameters.AddOptionalMilliseconds("end_time", endTime);
            parameters.AddOptional("pageIndex", page);
            parameters.AddOptional("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/private/trackorder/list/orders", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcTrailingOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
