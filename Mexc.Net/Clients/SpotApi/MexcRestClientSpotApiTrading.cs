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
        public async Task<WebCallResult> PlaceTestOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalString("quantity", quantity);
            parameters.AddOptionalString("quoteOrderQty", quoteQuantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptional("newClientOrderId", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/order/test", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<object>(request, parameters, ct).ConfigureAwait(false);
            return result.AsDataless();
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<MexcOrder>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalString("quantity", quantity);
            parameters.AddOptionalString("quoteOrderQty", quoteQuantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptional("newClientOrderId", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/order", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<CallResult<MexcOrderResult>[]>> PlaceMultipleOrdersAsync(IEnumerable<MexcPlaceOrderRequest> requests, CancellationToken ct = default)
        {
#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
            var parameters = new ParameterCollection()
            {
                { "batchOrders", JsonSerializer.Serialize(requests.ToArray(), options: SerializerOptions.WithConverters(MexcExchange.SerializerContext)) }
            };
#pragma warning restore IL2026
#pragma warning restore IL3050

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/batchOrders", MexcExchange.RateLimiter.SpotRest, 1, true);
            var resultData = await _baseClient.SendAsync<MexcOrderResult[]>(request, parameters, ct).ConfigureAwait(false);
            if (!resultData)
                return resultData.As<CallResult<MexcOrderResult>[]>(default);

            var result = new List<CallResult<MexcOrderResult>>();
            foreach (var item in resultData.Data)
            {
                if (item.ErrorCode != null)
                    result.Add(new CallResult<MexcOrderResult>(item, null, new ServerError(item.ErrorCode.Value, _baseClient.GetErrorInfo(item.ErrorCode.Value, item.ErrorMessage!))));
                else
                    result.Add(new CallResult<MexcOrderResult>(item));
            }

            if (result.All(x => !x.Success))
                return resultData.AsErrorWithData(new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), result.ToArray());

            return resultData.As(result.ToArray());
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<MexcOrder>> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, string? newClientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("origClientOrderId", clientOrderId);
            parameters.AddOptional("newClientOrderId", newClientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v3/order", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public Task<WebCallResult<MexcOrder[]>> CancelAllOrdersAsync(string symbol, CancellationToken ct = default)
            => CancelAllOrdersAsync(new[] { symbol });
        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult<MexcOrder[]>> CancelAllOrdersAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", string.Join(",", symbols) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v3/openOrders", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<MexcOrder>> GetOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("origClientOrderId", clientOrderId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/order", MexcExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<MexcOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<MexcOrder[]>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/openOrders", MexcExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<MexcOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<WebCallResult<MexcOrder[]>> GetOrdersAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/allOrders", MexcExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<MexcOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<MexcUserTrade[]>> GetUserTradesAsync(string symbol, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("orderId", orderId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/myTrades", MexcExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<MexcUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
