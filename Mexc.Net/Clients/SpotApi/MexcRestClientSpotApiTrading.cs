using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Interfaces.Clients.SpotApi;
using System.Text.Json;
using CryptoExchange.Net.Objects.Errors;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class MexcRestClientSpotApiTrading : IMexcRestClientSpotApiTrading
    {
        private readonly MexcRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal MexcRestClientSpotApiTrading(MexcRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Place Test Order

        /// <inheritdoc />
        public async Task<HttpResult> PlaceTestOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("side", side);
            parameters.Add("type", type);
            parameters.Add("quantity", quantity?.Normalize());
            parameters.Add("quoteOrderQty", quoteQuantity?.Normalize());
            parameters.Add("price", price?.Normalize());
            parameters.Add("newClientOrderId", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/order/test", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<object>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            return HttpResult.Ok(result);
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<MexcOrder>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("side", side);
            parameters.Add("type", type);
            parameters.Add("quantity", quantity?.Normalize());
            parameters.Add("quoteOrderQty", quoteQuantity?.Normalize());
            parameters.Add("price", price?.Normalize());
            parameters.Add("newClientOrderId", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/order", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<CallResult<MexcOrderResult>[]>> PlaceMultipleOrdersAsync(IEnumerable<MexcPlaceOrderRequest> requests, CancellationToken ct = default)
        {
#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "batchOrders", JsonSerializer.Serialize(requests.ToArray(), options: SerializerOptions.WithConverters(MexcExchange._serializerContext)) }
            };
#pragma warning restore IL2026
#pragma warning restore IL3050

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/batchOrders", MexcExchange.RateLimiter.SpotRest, 1, true);
            var resultData = await _baseClient.SendAsync<MexcOrderResult[]>(request, parameters, ct).ConfigureAwait(false);
            if (!resultData.Success)
                return HttpResult.Fail<CallResult<MexcOrderResult>[]>(resultData);

            var result = new List<CallResult<MexcOrderResult>>();
            foreach (var item in resultData.Data)
            {
                if (item.ErrorCode != null)
                    result.Add(CallResult.Fail<MexcOrderResult>(new ServerError(item.ErrorCode.Value, _baseClient.GetErrorInfo(item.ErrorCode.Value, item.ErrorMessage!))));
                else
                    result.Add(CallResult.Ok(item));
            }

            if (result.All(x => !x.Success))
                return HttpResult.Fail(resultData, new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), result.ToArray());

            return HttpResult.Ok(resultData, result.ToArray());
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<MexcOrder>> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, string? newClientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("orderId", orderId);
            parameters.Add("origClientOrderId", clientOrderId);
            parameters.Add("newClientOrderId", newClientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v3/order", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public Task<HttpResult<MexcOrder[]>> CancelAllOrdersAsync(string symbol, CancellationToken ct = default)
            => CancelAllOrdersAsync(new[] { symbol });
        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<HttpResult<MexcOrder[]>> CancelAllOrdersAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "symbol", string.Join(",", symbols) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v3/openOrders", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<MexcOrder>> GetOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("orderId", orderId);
            parameters.Add("origClientOrderId", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/order", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<MexcOrder[]>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/openOrders", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<HttpResult<MexcOrder[]>> GetOrdersAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/allOrders", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<MexcUserTrade[]>> GetUserTradesAsync(string symbol, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            parameters.Add("orderId", orderId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/myTrades", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
