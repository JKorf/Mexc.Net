using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Interfaces.Clients.SpotApi;
using System.Linq;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class MexcRestClientSpotApiTrading : IMexcRestClientSpotApiTrading
    {
        private readonly ILogger _logger;
        private readonly MexcRestClientSpotApi _baseClient;

        internal MexcRestClientSpotApiTrading(ILogger logger, MexcRestClientSpotApi baseClient)
        {
            _logger = logger;
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

            var result = await _baseClient.SendRequestInternal<object>("/api/v3/order/test", HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            var result = await _baseClient.SendRequestInternal<MexcOrder>("/api/v3/order", HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            var result = await _baseClient.SendRequestInternal<MexcOrder>("/api/v3/order", HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new CryptoExchange.Net.CommonObjects.OrderId { Id = result.Data.OrderId, SourceObject = result.Data });

            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcOrder>>> CancelAllOrdersAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", string.Join(",", symbols) }
            };

            var result = await _baseClient.SendRequestInternal<IEnumerable<MexcOrder>>("/api/v3/openOrders", HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<MexcOrder>("/api/v3/order", HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<IEnumerable<MexcOrder>>("/api/v3/openOrders", HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<IEnumerable<MexcOrder>>("/api/v3/allOrders", HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders

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

            return await _baseClient.SendRequestInternal<IEnumerable<MexcUserTrade>>("/api/v3/myTrades", HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
