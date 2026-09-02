using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Interfaces.Clients.SpotApi;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class MexcRestClientSpotApiSubAccount : IMexcRestClientSpotApiSubAccount
    {
        private readonly MexcRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal MexcRestClientSpotApiSubAccount(MexcRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<HttpResult<MexcSubUserAccount[]>> GetSubUserAccountsAsync(string? name = null, bool? isFreeze = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("subAccount", name);
            parameters.Add("isFreeze", isFreeze);
            parameters.Add("page", page);
            parameters.Add("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/sub-account/list", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcSubUserAccounts>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<MexcSubUserAccount[]>(result);

            return HttpResult.Ok(result, result.Data.SubAccounts);
        }

        /// <inheritdoc />
        public Task<HttpResult<MexcUniversalTransferResult>> UniversalTransferAsync(string asset, decimal amount, AccountType fromAccountType, AccountType toAccountType, string? fromAccount = null, string? toAccount = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                {"asset", asset },
                {"amount", amount },
            };
            parameters.Add("fromAccountType", fromAccountType);
            parameters.Add("toAccountType", toAccountType);
            parameters.Add("fromAccount", fromAccount);
            parameters.Add("toAccount", toAccount);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/capital/sub-account/universalTransfer", MexcExchange.RateLimiter.SpotRest, 1, true);
            return _baseClient.SendAsync<MexcUniversalTransferResult>(request, parameters, ct);
        }

        /// <inheritdoc />
        public Task<HttpResult<MexcUniversalTransferPaged>> GetUniversalTransfersAsync(AccountType fromAccountType, AccountType toAccountType, string? fromAccount = null, string? toAccount = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("fromAccountType", fromAccountType);
            parameters.Add("toAccountType", toAccountType);
            parameters.Add("fromAccount", fromAccount);
            parameters.Add("toAccount", toAccount);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/capital/sub-account/universalTransfer", MexcExchange.RateLimiter.SpotRest, 1, true);
            return _baseClient.SendAsync<MexcUniversalTransferPaged>(request, parameters, ct);
        }

        /// <inheritdoc />
        public Task<HttpResult<MexcSubUserAccountApiDetails>> GetSubUserAccountApiDetailsAsync(string subAccount, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                {"subAccount", subAccount},
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/sub-account/apiKey", MexcExchange.RateLimiter.SpotRest, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return _baseClient.SendAsync<MexcSubUserAccountApiDetails>(request, parameters, ct);
        }

        /// <inheritdoc />
        public async Task<HttpResult> CreateSubAccountAsync(string name, string note, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                {"subAccount", name},
                {"note", note},
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/sub-account/virtualSubAccount", MexcExchange.RateLimiter.SpotRest, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            var result = await _baseClient.SendAsync<MexcSubaccountResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 0)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        /// <inheritdoc />
        public async Task<HttpResult<MexcSubAccountApiKey>> CreateSubAccountApiKeyAsync(string subAccount, string note, string[] permissions, string[]? ipAddresses = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                {"subAccount", subAccount},
                {"note", note},
                {"permissions", string.Join(",", permissions)},
            };
            parameters.Add("ip", ipAddresses == null ? null : string.Join(",", ipAddresses));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/sub-account/apiKey", MexcExchange.RateLimiter.SpotRest, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<MexcSubAccountApiKey>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult> DeleteSubAccountApiKeyAsync(string subAccount, string apiKey, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                {"subAccount", subAccount},
                {"apiKey", apiKey},
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v3/sub-account/apiKey", MexcExchange.RateLimiter.SpotRest, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            var result = await _baseClient.SendAsync<MexcSubaccountResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 0)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        /// <inheritdoc />
        public async Task<HttpResult<MexcAccountBalance[]>> GetSubAccountBalancesAsync(string subAccount, AccountType accountType, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                {"subAccount", subAccount},
            };
            parameters.Add("accountType", accountType);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/sub-account/asset", MexcExchange.RateLimiter.SpotRest, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            var result = await _baseClient.SendAsync<MexcAccountBalances>(request, parameters, ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<MexcAccountBalance[]>(result);

            return HttpResult.Ok(result, result.Data.Balances);
        }

    }
}
