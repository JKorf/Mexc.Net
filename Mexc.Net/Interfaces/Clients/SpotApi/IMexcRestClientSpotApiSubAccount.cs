using Mexc.Net.Enums;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Mexc sub account endpoints
    /// </summary>
    public interface IMexcRestClientSpotApiSubAccount
    {
        /// <summary>
        /// Get details of the available sub-accounts
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-sub-account-list-for-master-account"/></para>
        /// </summary>
        /// <param name="name">The name of the sub-account</param>
        /// <param name="isFreeze">Filter by 'Is Freeze'</param>
        /// <param name="page">The page number</param>
        /// <param name="limit">The number of rows</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcSubUserAccount[]>> GetSubUserAccountsAsync(string? name = null, bool? isFreeze = null, int? page = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get API details of a sub-account
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-the-apikey-of-a-sub-account-for-master-account"/></para>
        /// </summary>
        /// <param name="subAccount">The name of the sub-account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcSubUserAccountApiDetails>> GetSubUserAccountApiDetailsAsync(string subAccount, CancellationToken ct = default);

        /// <summary>
        /// Perform a universal transfer between accounts
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#universal-transfer-for-master-account"/></para>
        /// </summary>
        /// <param name="asset">The name of the asset, for example `USDT`</param>
        /// <param name="amount">The amount that should be transferred</param>
        /// <param name="fromAccountType">The origin account type</param>
        /// <param name="toAccountType">The destination account type</param>
        /// <param name="fromAccount">The name of the origin account. Null = Master Account</param>
        /// <param name="toAccount">The name of the destination account. Null = Master Account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcUniversalTransferResult>> UniversalTransferAsync(string asset, decimal amount, AccountType fromAccountType, AccountType toAccountType, string? fromAccount = null, string? toAccount = null, CancellationToken ct = default);

        /// <summary>
        /// Query the universal transfer history
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-universal-transfer-history-for-master-account"/></para>
        /// </summary>
        /// <param name="fromAccountType">The origin account type</param>
        /// <param name="toAccountType">The destination account type</param>
        /// <param name="fromAccount">The name of the origin account. Null = Master Account</param>
        /// <param name="toAccount">The name of the destination account. Null = Master Account</param>
        /// <param name="startTime">The start time for the query</param>
        /// <param name="endTime">The end time for the query</param>
        /// <param name="page">The page number</param>
        /// <param name="limit">The number of rows</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcUniversalTransferPaged>> GetUniversalTransfersAsync(AccountType fromAccountType, AccountType toAccountType, string? fromAccount = null, string? toAccount = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Create a new virtual sub account
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#create-a-sub-account-for-master-account"/></para>
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="note">Notes</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CreateSubAccountAsync(string name, string note, CancellationToken ct = default);

        /// <summary>
        /// Create API key for sub account
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#create-an-apikey-for-a-sub-account-for-master-account"/></para>
        /// </summary>
        /// <param name="subAccount">Sub account</param>
        /// <param name="note">Key note</param>
        /// <param name="permissions">Permissions</param>
        /// <param name="ipAddresses">IP address whitelist</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcSubAccountApiKey>> CreateSubAccountApiKeyAsync(string subAccount, string note, string[] permissions, string[]? ipAddresses = null, CancellationToken ct = default);

        /// <summary>
        /// Delete a sub account API key
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#delete-the-apikey-of-a-sub-account-for-master-account"/></para>
        /// </summary>
        /// <param name="subAccount">Sub account</param>
        /// <param name="apiKey">The API key</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> DeleteSubAccountApiKeyAsync(string subAccount, string apiKey, CancellationToken ct = default);

        /// <summary>
        /// Get sub account balances
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-sub-account-asset"/></para>
        /// </summary>
        /// <param name="subAccount">Sub account</param>
        /// <param name="accountType">Account type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcAccountBalance[]>> GetSubAccountBalancesAsync(string subAccount, AccountType accountType, CancellationToken ct = default);
    }    

}
