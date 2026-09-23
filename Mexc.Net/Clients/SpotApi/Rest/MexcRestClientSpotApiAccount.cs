using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Models;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class MexcRestClientSpotApiAccount : IMexcRestClientSpotApiAccount
    {
        private readonly MexcRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal MexcRestClientSpotApiAccount(MexcRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Account Info

        /// <inheritdoc />
        public async Task<HttpResult<MexcAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/account", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcAccountInfo>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get KYC Status

        /// <inheritdoc />
        public async Task<HttpResult<MexcKycStatus>> GetKycStatusAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/kyc/status", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcKycStatus>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Assets

        /// <inheritdoc />
        public async Task<HttpResult<MexcUserAsset[]>> GetUserAssetsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/capital/config/getall", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcUserAsset[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<HttpResult<MexcId>> WithdrawAsync(string asset, string address, decimal quantity, string? clientOrderId = null, string? network = null, string? memo = null, string? remark = null, string? contractAddress = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "coin", asset },
                { "address", address }
            };
            parameters.Add("amount", quantity);
            parameters.Add("withdrawOrderId", clientOrderId);
            parameters.Add("netWork", network);
            parameters.Add("memo", memo);
            parameters.Add("remark", remark);
            parameters.Add("contractAddress", contractAddress);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/capital/withdraw", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Withdraw

        /// <inheritdoc />
        public async Task<HttpResult<MexcId>> CancelWithdrawAsync(string withdrawId, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "id", withdrawId },
            };
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v3/capital/withdraw", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposit History

        /// <inheritdoc />
        public async Task<HttpResult<MexcDeposit[]>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("coin", asset);
            parameters.Add("status", status);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/capital/deposit/hisrec", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcDeposit[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdraw History

        /// <inheritdoc />
        public async Task<HttpResult<MexcWithdrawal[]>> GetWithdrawHistoryAsync(string? asset = null, WithdrawStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("coin", asset);
            parameters.Add("status", status);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/capital/withdraw/history", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcWithdrawal[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Generate Deposit Address

        /// <inheritdoc />
        public async Task<HttpResult<MexcDepositAddress[]>> GenerateDepositAddressAsync(string asset, string network, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "coin", asset },
                { "network", network }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/capital/deposit/address", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcDepositAddress[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposit Addresses

        /// <inheritdoc />
        public async Task<HttpResult<MexcDepositAddress[]>> GetDepositAddressesAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "coin", asset }
            };
            parameters.Add("network", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/capital/deposit/address", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcDepositAddress[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdraw Addresses

        /// <inheritdoc />
        public async Task<HttpResult<MexcPaginated<MexcWithdrawAddress[]>>> GetWithdrawAddressesAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("coin", asset);
            parameters.Add("page", page);
            parameters.Add("limit", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/capital/withdraw/address", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcPaginated<MexcWithdrawAddress[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<HttpResult<MexcTransferId>> TransferAsync(string asset, AccountType fromAccountType, AccountType toAccountType, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "asset", asset }
            };
            parameters.Add("fromAccountType", fromAccountType);
            parameters.Add("toAccountType", toAccountType);
            parameters.Add("amount", quantity);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/capital/transfer", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcTransferId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transfer History

        /// <inheritdoc />
        public async Task<HttpResult<MexcRows<MexcTransfer[]>>> GetTransferHistoryAsync(AccountType fromAccount, AccountType toAccount, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("fromAccountType", fromAccount);
            parameters.Add("toAccountType", toAccount);
            parameters.Add("endTime", endTime);
            parameters.Add("startTime", startTime);
            parameters.Add("page", page);
            parameters.Add("size", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/capital/transfer", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcRows<MexcTransfer[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transfer

        /// <inheritdoc />
        public async Task<HttpResult<MexcTransfer>> GetTransferAsync(string transferId, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "tranId", transferId }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/capital/transfer/tranId", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcTransfer>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Assets For Dust Transfer

        /// <inheritdoc />
        public async Task<HttpResult<MexcEligibleDust[]>> GetAssetsForDustTransferAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/capital/convert/list", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcEligibleDust[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Dust Transfer

        /// <inheritdoc />
        public async Task<HttpResult<MexcDustResult>> DustTransferAsync(IEnumerable<string> assets, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "asset", string.Join(",", assets) }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/capital/convert", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcDustResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Dust Log

        /// <inheritdoc />
        public async Task<HttpResult<MexcPaginated<MexcDustLog[]>>> GetDustLogAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("limit", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/capital/convert", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcPaginated<MexcDustLog[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Internal Transfer

        /// <inheritdoc />
        public async Task<HttpResult<MexcTransferId>> TransferInternalAsync(
            string asset,
            decimal quantity,
            TransferAccountType toAccountType,
            string toAccount,
            string? areaCode = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "asset", asset }
            };
            parameters.Add("toAccountType", toAccountType);
            parameters.Add("toAccount", toAccount);
            parameters.Add("amount", quantity);
            parameters.Add("areaCode", areaCode);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/capital/transfer/internal", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcTransferId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Internal Transfer History

        /// <inheritdoc />
        public async Task<HttpResult<MexcPaginated<MexcInternalTransfer[]>>> GetInternalTransferHistoryAsync(string? transferId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("tranId", transferId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("limit", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/capital/transfer/internal", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcPaginated<MexcInternalTransfer[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Mx Deduction

        /// <inheritdoc />
        public async Task<HttpResult<MexcDeductStatus>> SetMxDeductionAsync(bool enabled, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "mxDeductEnable", enabled }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/mxDeduct/enable", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcResult<MexcDeductStatus>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<MexcDeductStatus>(result);

            return HttpResult.Ok(result, result.Data.Data!);
        }

        #endregion

        #region Get Mx Deduction Status

        /// <inheritdoc />
        public async Task<HttpResult<MexcDeductStatus>> GetMxDeductionStatusAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/mxDeduct/enable", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcResult<MexcDeductStatus>>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<MexcDeductStatus>(result);

            return HttpResult.Ok(result, result.Data.Data!);
        }

        #endregion

        #region Get Trade Fee

        /// <inheritdoc />
        public async Task<HttpResult<MexcTradeFee>> GetTradeFeeAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/tradeFee", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcResult<MexcTradeFee>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<MexcTradeFee>(result);

            return HttpResult.Ok(result, result.Data.Data!);
        }

        #endregion

        #region Create a ListenKey 
        /// <inheritdoc />
        public async Task<HttpResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/userDataStream", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcListenKey>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<string>(result);

            return HttpResult.Ok(result, result.Data.ListenKey!);
        }

        #endregion

        #region Ping/Keep-alive a ListenKey

        /// <inheritdoc />
        public async Task<HttpResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Put, _baseClient.BaseAddress, "/api/v3/userDataStream", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 0)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message!)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Invalidate a ListenKey
        /// <inheritdoc />
        public async Task<HttpResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings)
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v3/userDataStream", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 0)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message!)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Get Rebate History

        /// <inheritdoc />
        public async Task<HttpResult<MexcPaginated<MexcRebate[]>>> GetRebateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/rebate/taxQuery", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcPaginated<MexcRebate[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Rebate Details

        /// <inheritdoc />
        public async Task<HttpResult<MexcPaginated<MexcRebateDetails[]>>> GetRebateDetailsAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/rebate/detail", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcPaginated<MexcRebateDetails[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Rebate Kickback

        /// <inheritdoc />
        public async Task<HttpResult<MexcPaginated<MexcRebateDetails[]>>> GetRebateKickbackAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/rebate/detail/kickback", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcPaginated<MexcRebateDetails[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Affiliate Commission

        /// <inheritdoc />
        public async Task<HttpResult<MexcAffiliateCommissions>> GetAffiliateCommissionAsync(string? uid = null, string? inviteCode = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._spotParameterSerializationSettings);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("uid", uid);
            parameters.Add("inviteCode", inviteCode);
            parameters.Add("page", page);
            parameters.Add("pageSize", page);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/rebate/affiliate/commission", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcResult<MexcAffiliateCommissions>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<MexcAffiliateCommissions>(result);

            return HttpResult.Ok(result, result.Data.Data!);
        }

        #endregion
    }
}
