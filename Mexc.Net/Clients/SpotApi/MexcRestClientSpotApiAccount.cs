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
        public async Task<WebCallResult<MexcAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/account", MexcExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<MexcAccountInfo>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get KYC Status

        /// <inheritdoc />
        public async Task<WebCallResult<MexcKycStatus>> GetKycStatusAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/kyc/status", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcKycStatus>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Assets

        /// <inheritdoc />
        public async Task<WebCallResult<MexcUserAsset[]>> GetUserAssetsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/capital/config/getall", MexcExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<MexcUserAsset[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<WebCallResult<MexcId>> WithdrawAsync(string asset, string address, decimal quantity, string? clientOrderId = null, string? network = null, string? memo = null, string? remark = null, string? contractAddress = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "coin", asset },
                { "address", address }
            };
            parameters.AddString("amount", quantity);
            parameters.AddOptional("withdrawOrderId", clientOrderId);
            parameters.AddOptional("netWork", network);
            parameters.AddOptional("memo", memo);
            parameters.AddOptional("remark", remark);
            parameters.AddOptional("contractAddress", contractAddress);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/capital/withdraw", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Withdraw

        /// <inheritdoc />
        public async Task<WebCallResult<MexcId>> CancelWithdrawAsync(string withdrawId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "id", withdrawId },
            };
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v3/capital/withdraw", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposit History

        /// <inheritdoc />
        public async Task<WebCallResult<MexcDeposit[]>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("coin", asset);
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptionalString("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/capital/deposit/hisrec", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcDeposit[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdraw History

        /// <inheritdoc />
        public async Task<WebCallResult<MexcWithdrawal[]>> GetWithdrawHistoryAsync(string? asset = null, WithdrawStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("coin", asset);
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptionalString("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/capital/withdraw/history", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcWithdrawal[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Generate Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<MexcDepositAddress[]>> GenerateDepositAddressAsync(string asset, string network, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "coin", asset },
                { "network", network }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/capital/deposit/address", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcDepositAddress[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposit Addresses

        /// <inheritdoc />
        public async Task<WebCallResult<MexcDepositAddress[]>> GetDepositAddressesAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "coin", asset }
            };
            parameters.AddOptional("network", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/capital/deposit/address", MexcExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<MexcDepositAddress[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdraw Addresses

        /// <inheritdoc />
        public async Task<WebCallResult<MexcPaginated<MexcWithdrawAddress[]>>> GetWithdrawAddressesAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("coin", asset);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/capital/withdraw/address", MexcExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<MexcPaginated<MexcWithdrawAddress[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<MexcTransferId>> TransferAsync(string asset, AccountType fromAccountType, AccountType toAccountType, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "asset", asset }
            };
            parameters.AddEnum("fromAccountType", fromAccountType);
            parameters.AddEnum("toAccountType", toAccountType);
            parameters.AddString("amount", quantity);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/capital/transfer", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcTransferId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transfer History

        /// <inheritdoc />
        public async Task<WebCallResult<MexcRows<MexcTransfer[]>>> GetTransferHistoryAsync(AccountType fromAccount, AccountType toAccount, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("fromAccountType", fromAccount);
            parameters.AddEnum("toAccountType", toAccount);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("size", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/capital/transfer", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcRows<MexcTransfer[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<MexcTransfer>> GetTransferAsync(string transferId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "tranId", transferId }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/capital/transfer/tranId", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcTransfer>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Assets For Dust Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<MexcEligibleDust[]>> GetAssetsForDustTransferAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/capital/convert/list", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcEligibleDust[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Dust Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<MexcDustResult>> DustTransferAsync(IEnumerable<string> assets, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "asset", string.Join(",", assets) }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/capital/convert", MexcExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<MexcDustResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Dust Log

        /// <inheritdoc />
        public async Task<WebCallResult<MexcPaginated<MexcDustLog[]>>> GetDustLogAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/capital/convert", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcPaginated<MexcDustLog[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Internal Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<MexcTransferId>> TransferInternalAsync(
            string asset,
            decimal quantity,
            TransferAccountType toAccountType,
            string toAccount,
            string? areaCode = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "asset", asset }
            };
            parameters.AddEnum("toAccountType", toAccountType);
            parameters.Add("toAccount", toAccount);
            parameters.AddString("amount", quantity);
            parameters.AddOptional("areaCode", areaCode);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/capital/transfer/internal", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcTransferId>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Internal Transfer History

        /// <inheritdoc />
        public async Task<WebCallResult<MexcPaginated<MexcInternalTransfer[]>>> GetInternalTransferHistoryAsync(string? transferId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("tranId", transferId);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/capital/transfer/internal", MexcExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<MexcPaginated<MexcInternalTransfer[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Mx Deduction

        /// <inheritdoc />
        public async Task<WebCallResult<MexcDeductStatus>> SetMxDeductionAsync(bool enabled, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "mxDeductEnable", enabled }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/mxDeduct/enable", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcResult<MexcDeductStatus>>(request, parameters, ct).ConfigureAwait(false);
            return result.As<MexcDeductStatus>(result.Data?.Data);
        }

        #endregion

        #region Get Mx Deduction Status

        /// <inheritdoc />
        public async Task<WebCallResult<MexcDeductStatus>> GetMxDeductionStatusAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/mxDeduct/enable", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcResult<MexcDeductStatus>>(request, null, ct).ConfigureAwait(false);
            return result.As<MexcDeductStatus>(result.Data?.Data);
        }

        #endregion

        #region Get Trade Fee

        /// <inheritdoc />
        public async Task<WebCallResult<MexcTradeFee>> GetTradeFeeAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/tradeFee", MexcExchange.RateLimiter.SpotRest, 20, true);
            var result = await _baseClient.SendAsync<MexcResult<MexcTradeFee>>(request, parameters, ct).ConfigureAwait(false);
            return result.As<MexcTradeFee>(result.Data?.Data);
        }

        #endregion

        #region Create a ListenKey 
        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/userDataStream", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcListenKey>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.ListenKey!);
        }

        #endregion

        #region Ping/Keep-alive a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new ParameterCollection()
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Put, "/api/v3/userDataStream", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcResult>(request, parameters, ct).ConfigureAwait(false);            
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 0)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message!)));

            return result.AsDataless();
        }

        #endregion

        #region Invalidate a ListenKey
        /// <inheritdoc />
        public async Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new ParameterCollection()
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v3/userDataStream", MexcExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<MexcResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 0)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message!)));

            return result.AsDataless();
        }

        #endregion

    }
}
