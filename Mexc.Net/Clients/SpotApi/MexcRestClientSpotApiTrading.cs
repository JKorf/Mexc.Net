﻿using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Interfaces.Clients.SpotApi;

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
            if (result)
                _baseClient.InvokeOrderPlaced(new CryptoExchange.Net.CommonObjects.OrderId { Id = result.Data.OrderId, SourceObject = result.Data });

            return result;
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
            if (result)
                _baseClient.InvokeOrderCanceled(new CryptoExchange.Net.CommonObjects.OrderId { Id = result.Data.OrderId, SourceObject = result.Data });

            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<MexcOrder>>> CancelAllOrdersAsync(string symbol, CancellationToken ct = default)
            => CancelAllOrdersAsync(new[] { symbol });
        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcOrder>>> CancelAllOrdersAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", string.Join(",", symbols) }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v3/openOrders", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<IEnumerable<MexcOrder>>(request, parameters, ct).ConfigureAwait(false);
            if (result)
            {
                foreach(var item in result.Data)
                    _baseClient.InvokeOrderCanceled(new CryptoExchange.Net.CommonObjects.OrderId { Id = item.OrderId, SourceObject = result.Data });
            }
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
        public async Task<WebCallResult<IEnumerable<MexcOrder>>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/openOrders", MexcExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<IEnumerable<MexcOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcOrder>>> GetOrdersAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/allOrders", MexcExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<IEnumerable<MexcOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcUserTrade>>> GetUserTradesAsync(string symbol, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
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
            return await _baseClient.SendAsync<IEnumerable<MexcUserTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
