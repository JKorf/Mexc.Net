using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Models;
using System.Linq;
using System.Security.Cryptography;

namespace Mexc.Net.Clients.SpotApi
{
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

            return await _baseClient.SendRequestInternal<MexcOrder>("/api/v3/order", HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
