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
using Mexc.Net.Objects.Models;
using System.Linq;
using CryptoExchange.Net;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class MexcRestClientSpotApiAccount : IMexcRestClientSpotApiAccount
    {
        private readonly ILogger _logger;
        private readonly MexcRestClientSpotApi _baseClient;

        internal MexcRestClientSpotApiAccount(ILogger logger, MexcRestClientSpotApi baseClient)
        {
            _logger = logger;
            _baseClient = baseClient;
        }

        #region Get Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<MexcAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<MexcAccountInfo>("/api/v3/account", HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        #endregion

        #region Get User Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcUserAsset>>> GetUserAssetsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<MexcUserAsset>>("/api/v3/capital/config/getall", HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
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
            var result = await _baseClient.SendRequestInternal<IEnumerable<MexcId>>("/api/v3/capital/withdraw", HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            return result.As<MexcId>(result.Data?.Single());
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
            return await _baseClient.SendRequestInternal<MexcId>("/api/v3/capital/withdraw", HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposit History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcDeposit>>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("coin", asset);
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptionalString("limit", limit);
            return await _baseClient.SendRequestInternal<IEnumerable<MexcDeposit>>("/api/v3/capital/deposit/hisrec", HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdraw History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcWithdrawal>>> GetWithdrawHistoryAsync(string? asset = null, WithdrawStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("coin", asset);
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptionalString("limit", limit);
            return await _baseClient.SendRequestInternal<IEnumerable<MexcWithdrawal>>("/api/v3/capital/withdraw/history", HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Generate Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcDepositAddress>>> GenerateDepositAddressAsync(string asset, string network, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "coin", asset },
                { "network", network }
            };
            return await _baseClient.SendRequestInternal<IEnumerable<MexcDepositAddress>>("/api/v3/capital/deposit/address", HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposit Addresses

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcDepositAddress>>> GetDepositAddressesAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "coin", asset }
            };
            parameters.AddOptional("network", network);
            return await _baseClient.SendRequestInternal<IEnumerable<MexcDepositAddress>>("/api/v3/capital/deposit/address", HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdraw Addresses

        /// <inheritdoc />
        public async Task<WebCallResult<MexcPaginated<IEnumerable<MexcWithdrawAddress>>>> GetWithdrawAddressesAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("coin", asset);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", pageSize);
            return await _baseClient.SendRequestInternal<MexcPaginated<IEnumerable<MexcWithdrawAddress>>>("/api/v3/capital/withdraw/address", HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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


            var result = await _baseClient.SendRequestInternal<IEnumerable<MexcTransferId>>("/api/v3/capital/transfer", HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            return result.As<MexcTransferId>(result.Data?.Single());
        }

        #endregion

        #region Get Transfer History

        /// <inheritdoc />
        public async Task<WebCallResult<MexcRows<IEnumerable<MexcTransfer>>>> GetTransferHistoryAsync(AccountType fromAccount, AccountType toAccount, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("fromAccountType", fromAccount);
            parameters.AddEnum("toAccountType", toAccount);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("size", pageSize);
            return await _baseClient.SendRequestInternal<MexcRows<IEnumerable<MexcTransfer>>>("/api/v3/capital/transfer", HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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
            return await _baseClient.SendRequestInternal<MexcTransfer> ("/api/v3/capital/transfer/tranId", HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Assets For Dust Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcEligibleDust>>> GetAssetsForDustTransferAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<MexcEligibleDust>>("/api/v3/capital/convert/list", HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
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
            return await _baseClient.SendRequestInternal<MexcDustResult>("/api/v3/capital/convert", HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        #endregion

        #region Get Dust Log

        /// <inheritdoc />
        public async Task<WebCallResult<MexcPaginated<IEnumerable<MexcDustLog>>>> GetDustLogAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", pageSize);
            return await _baseClient.SendRequestInternal<MexcPaginated<IEnumerable<MexcDustLog>>>("/api/v3/capital/convert", HttpMethod.Get, ct, parameters,true).ConfigureAwait(false);
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

            var result = await _baseClient.SendRequestInternal<MexcResult<MexcDeductStatus>>("/api/v3/mxDeduct/enable", HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            return result.As<MexcDeductStatus>(result.Data?.Data);
        }

        #endregion

        #region Get Mx Deduction Status

        /// <inheritdoc />
        public async Task<WebCallResult<MexcDeductStatus>> GetMxDeductionStatusAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<MexcResult<MexcDeductStatus>>("/api/v3/mxDeduct/enable", HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
            return result.As<MexcDeductStatus>(result.Data?.Data);
        }

        #endregion

        #region Get Mx Deduction Status

        /// <inheritdoc />
        public async Task<WebCallResult<MexcTradeFee>> GetTradeFeeAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var result = await _baseClient.SendRequestInternal<MexcResult<MexcTradeFee>>("/api/v3/tradeFee", HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
            return result.As<MexcTradeFee>(result.Data?.Data);
        }

        #endregion

        #region Create a ListenKey 
        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<MexcListenKey>("/api/v3/userDataStream", HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
            return result.As(result.Data?.ListenKey!);
        }

        #endregion

        #region Ping/Keep-alive a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new Dictionary<string, object>
            {
                { "listenKey", listenKey }
            };

            var result = await _baseClient.SendRequestInternal<MexcResult>("/api/v3/userDataStream", HttpMethod.Put, ct, parameters, signed: true).ConfigureAwait(false);
            if (result.Data.Code != 0)
                return result.AsDatalessError(new ServerError(result.Data.Code, result.Data.Message!));

            return result.AsDataless();
        }

        #endregion

        #region Invalidate a ListenKey
        /// <inheritdoc />
        public async Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new Dictionary<string, object>
            {
                { "listenKey", listenKey }
            };

            var result = await _baseClient.SendRequestInternal<MexcResult>("/api/v3/userDataStream", HttpMethod.Delete, ct, parameters, signed: true).ConfigureAwait(false);
            if (result.Data.Code != 0)
                return result.AsDatalessError(new ServerError(result.Data.Code, result.Data.Message!));

            return result.AsDataless();
        }

        #endregion

    }
}
