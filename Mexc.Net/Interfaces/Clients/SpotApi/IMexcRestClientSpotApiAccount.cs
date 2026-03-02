using Mexc.Net.Enums;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Mexc Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IMexcRestClientSpotApiAccount
    {
        /// <summary>
        /// Get account and balance info
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#account-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/account
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get KYC status for the account
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-kyc-status" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/kyc/status
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcKycStatus>> GetKycStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a list of user assets and deposit/withdrawal data
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-the-currency-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/capital/config/getall
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcUserAsset[]>> GetUserAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Withdraw funds
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#withdraw-new" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/capital/withdraw
        /// </para>
        /// </summary>
        /// <param name="asset">Asset, for example `BTC`</param>
        /// <param name="address">Address to withdraw to</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="clientOrderId">Order id</param>
        /// <param name="network">Network to use</param>
        /// <param name="memo">Memo</param>
        /// <param name="remark">Remark</param>
        /// <param name="contractAddress">Asset contract address</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcId>> WithdrawAsync(string asset, string address, decimal quantity, string? clientOrderId = null, string? network = null, string? memo = null, string? remark = null, string? contractAddress = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a withdrawal
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#cancel-withdraw" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/capital/withdraw
        /// </para>
        /// </summary>
        /// <param name="withdrawId">The id of the withdrawal to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcId>> CancelWithdrawAsync(string withdrawId, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#deposit-history-supporting-network" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/capital/deposit/hisrec
        /// </para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `BTC`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Filter by limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcDeposit[]>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#withdraw-history-supporting-network" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/capital/withdraw/history
        /// </para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `BTC`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Filter by limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcWithdrawal[]>> GetWithdrawHistoryAsync(string? asset = null, WithdrawStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Generate a deposit address
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#generate-deposit-address-supporting-network" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/capital/deposit/address
        /// </para>
        /// </summary>
        /// <param name="asset">Asset, for example `BTC`</param>
        /// <param name="network">Network</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcDepositAddress[]>> GenerateDepositAddressAsync(string asset, string network, CancellationToken ct = default);

        /// <summary>
        /// Get deposit addresses
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#deposit-address-supporting-network" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/capital/deposit/address
        /// </para>
        /// </summary>
        /// <param name="asset">Asset, for example `BTC`</param>
        /// <param name="network">Filter by network</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcDepositAddress[]>> GetDepositAddressesAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal addresses
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#withdraw-address-supporting-network" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/capital/withdraw/address
        /// </para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `BTC`</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcPaginated<MexcWithdrawAddress[]>>> GetWithdrawAddressesAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer between accounts
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#user-universal-transfer" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/capital/transfer
        /// </para>
        /// </summary>
        /// <param name="asset">The asset to transfer, for example `BTC`</param>
        /// <param name="fromAccountType">From account</param>
        /// <param name="toAccountType">To account</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTransferId>> TransferAsync(string asset, AccountType fromAccountType, AccountType toAccountType, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get user transfer history
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-user-universal-transfer-history" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/capital/transfer
        /// </para>
        /// </summary>
        /// <param name="fromAccount">From account type</param>
        /// <param name="toAccount">To account type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcRows<MexcTransfer[]>>> GetTransferHistoryAsync(AccountType fromAccount, AccountType toAccount, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a transfer
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-user-universal-transfer-history-by-tranid" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/capital/transfer/tranId
        /// </para>
        /// </summary>
        /// <param name="transferId">Transfer id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTransfer>> GetTransferAsync(string transferId, CancellationToken ct = default);

        /// <summary>
        /// Get dust assets which can be converted
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#get-assets-that-can-be-converted-into-mx" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/capital/convert/list
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcEligibleDust[]>> GetAssetsForDustTransferAsync(CancellationToken ct = default);

        /// <summary>
        /// Convert small amount (dust) of certain assets to equal value Mx
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#dust-transfer" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/capital/convert
        /// </para>
        /// </summary>
        /// <param name="assets">Assets to convert, for example `BTC`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcDustResult>> DustTransferAsync(IEnumerable<string> assets, CancellationToken ct = default);

        /// <summary>
        /// Get dust transfer log
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#dust-transfer" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/capital/convert
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcPaginated<MexcDustLog[]>>> GetDustLogAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer an asset to another user on MEXC
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#internal-transfer" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/capital/transfer/internal
        /// </para>
        /// </summary>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="toAccountType">Type of identifier to transfer to</param>
        /// <param name="toAccount">Account identifier</param>
        /// <param name="areaCode">Area code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTransferId>> TransferInternalAsync(
            string asset,
            decimal quantity,
            TransferAccountType toAccountType,
            string toAccount,
            string? areaCode = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get internal transfer history
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#internal-transfer" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/capital/transfer/internal
        /// </para>
        /// </summary>
        /// <param name="transferId">Filter by transfer id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcPaginated<MexcInternalTransfer[]>>> GetInternalTransferHistoryAsync(string? transferId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Set MX deduction status
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#enable-mx-deduct" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/mxDeduct/enable
        /// </para>
        /// </summary>
        /// <param name="enabled">Enabled</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcDeductStatus>> SetMxDeductionAsync(bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Get MX deduction status
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-mx-deduct-status" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/mxDeduct/enable
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcDeductStatus>> GetMxDeductionStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get trade fee for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-symbol-commission" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/tradeFee
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `BTCUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTradeFee>> GetTradeFeeAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in a subsequent request to user subscribe methods in the socket client. The stream will close after 60 minutes unless <see cref="KeepAliveUserStreamAsync">KeepAliveUserStreamAsync</see> is called.
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#listen-key" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/userDataStream
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#listen-key" /><br />
        /// Endpoint:<br />
        /// PUT /api/v3/userDataStream
        /// </para>
        /// </summary>
        /// <param name="listenKey"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stops the current user stream
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#listen-key" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/userDataStream
        /// </para>
        /// </summary>
        /// <param name="listenKey"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default);
    }
}
