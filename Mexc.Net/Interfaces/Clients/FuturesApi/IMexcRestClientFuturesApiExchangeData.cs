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
        Task<WebCallResult<MexcContract[]>> GetSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get transferable assets
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-transferable-currencies" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<string[]>> GetTransferableAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get order book
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-contract-s-depth-information" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get index price for a symbol
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-index-price" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcIndexPrice>> GetIndexPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get mark price for a symbol
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-fair-price" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate info
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-funding-rate" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get klines
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#k-line-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get index price klines
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#k-line-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesKline[]>> GetIndexPriceKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get mark price klines
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#k-line-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesKline[]>> GetMarkPriceKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-transaction-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="limit">Number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get ticker for a symbol
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-trend-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get tickers 
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-trend-data" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get risk fund balances
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-all-contract-risk-fund-balance" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcRiskFundBalance[]>> GetRiskFundBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get risk fund balance history
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-risk-fund-balance-history" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcRiskFundBalanceHistoryPage>> GetRiskFundBalanceHistoryAsync(string symbol, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-funding-rate-history" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFundingRateHistoryPage>> GetFundingRateHistoryAsync(string symbol, int? page = null, int? pageSize = null, CancellationToken ct = default);

    }
}
