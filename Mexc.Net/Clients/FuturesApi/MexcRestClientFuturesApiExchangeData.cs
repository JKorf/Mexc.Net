using System.Diagnostics;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Objects.Models;
using CryptoExchange.Net.RateLimiting.Guards;

namespace Mexc.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class MexcRestClientFuturesApiExchangeData : IMexcRestClientFuturesApiExchangeData
    {
        private readonly MexcRestClientFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal MexcRestClientFuturesApiExchangeData(MexcRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }


        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/contract/ping", MexcExchange.RateLimiter.SpotRest, 1, false,
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long>(request, null, ct).ConfigureAwait(false);
            return result.As(DateTimeConverter.ParseFromDouble(result.Data));
        }

        #endregion

        #region Get Contracts

        /// <inheritdoc />
        public async Task<WebCallResult<MexcContract[]>> GetContractsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/contract/detail", MexcExchange.RateLimiter.FuturesRest, 1, false, 
                limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<MexcContract[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transferable Assets

        /// <inheritdoc />
        public async Task<WebCallResult<string[]>> GetTransferableAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/contract/support_currencies", MexcExchange.RateLimiter.FuturesRest, 1, false,
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<string[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
