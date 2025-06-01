using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Mexc Futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IMexcRestClientFuturesApiExchangeData
    {
        /// <summary>
        /// Get the server time
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#market-endpoints" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get contracts
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#market-endpoints" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcContract[]>> GetContractsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get transferable assets
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-transferable-currencies" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<string[]>> GetTransferableAssetsAsync(CancellationToken ct = default);
    }
}
