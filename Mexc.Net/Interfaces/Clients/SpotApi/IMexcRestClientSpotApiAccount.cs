using CryptoExchange.Net.Objects;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Mexc Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IMexcRestClientSpotApiAccount
    {
        /// <summary>
        /// Get account and balance info
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#account-information" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a list of user assets and deposit/withdrawal data
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-the-currency-information" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<MexcUserAsset>>> GetUserAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Withdraw funds
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-the-currency-information" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="address">Address to withdraw to</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="clientOrderId">Order id</param>
        /// <param name="network">Network to use</param>
        /// <param name="memo">Memo</param>
        /// <param name="remark">Remark</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcId>> WithdrawAsync(string asset, string address, decimal quantity, string? clientOrderId = null, string? network = null, string? memo = null, string? remark = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a withdrawal
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#cancel-withdraw" /></para>
        /// </summary>
        /// <param name="withdrawId">The id of the withdrawal to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcId>> CancelWithdrawAsync(string withdrawId, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#deposit-history-supporting-network" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Filter by limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<MexcDeposit>>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#withdraw-history-supporting-network" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Filter by limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<MexcWithdrawal>>> GetWithdrawHistoryAsync(string? asset = null, WithdrawStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Generate a deposit address
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#generate-deposit-address-supporting-network" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="network">Network</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<MexcDepositAddress>>> GenerateDepositAddressAsync(string asset, string network, CancellationToken ct = default);

        /// <summary>
        /// Get deposit addresses
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#deposit-address-supporting-network" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="network">Filter by network</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<MexcDepositAddress>>> GetDepositAddressesAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal addresses
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#withdraw-address-supporting-network" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcPaginated<IEnumerable<MexcWithdrawAddress>>>> GetWithdrawAddressesAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer between accounts
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#user-universal-transfer" /></para>
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="fromAccountType">From account</param>
        /// <param name="toAccountType">To account</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTransferId>> TransferAsync(string asset, AccountType fromAccountType, AccountType toAccountType, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get user transfer history
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-user-universal-transfer-history" /></para>
        /// </summary>
        /// <param name="fromAccount">From account type</param>
        /// <param name="toAccount">To account type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcRows<IEnumerable<MexcTransfer>>>> GetTransferHistoryAsync(AccountType fromAccount, AccountType toAccount, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a transfer
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-user-universal-transfer-history-by-tranid" /></para>
        /// </summary>
        /// <param name="transferId">Transfer id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTransfer>> GetTransferAsync(string transferId, CancellationToken ct = default);

        /// <summary>
        /// Get dust assets which can be converted
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#get-assets-that-can-be-converted-into-mx" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<MexcEligibleDust>>> GetAssetsForDustTransferAsync(CancellationToken ct = default);

        /// <summary>
        /// Convert small amount (dust) of certain assets to equal value Mx
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#dust-transfer" /></para>
        /// </summary>
        /// <param name="assets">Assets to convert</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcDustResult>> DustTransferAsync(IEnumerable<string> assets, CancellationToken ct = default);

        /// <summary>
        /// Get dust transfer log
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#dust-transfer" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcPaginated<IEnumerable<MexcDustLog>>>> GetDustLogAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Set MX deduction status
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#enable-mx-deduct" /></para>
        /// </summary>
        /// <param name="enabled">Enabled</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcDeductStatus>> SetMxDeductionAsync(bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Get MX deduction status
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-mx-deduct-status" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcDeductStatus>> GetMxDeductionStatusAsync(CancellationToken ct = default);
    }
}
