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
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#market-endpoints" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/ping
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get contract
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#market-endpoints" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/detail
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcContract>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get contracts
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#market-endpoints" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/detail
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcContract[]>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get transferable assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-transferable-currencies" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/support_currencies
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<string[]>> GetTransferableAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-contract-s-depth-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/depth/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Limit</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get index price for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-index-price" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/index_price/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcIndexPrice>> GetIndexPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get mark price for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-fair-price" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/fair_price/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate info
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-funding-rate" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/funding_rate/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get funding rates for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-funding-rate" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/funding_rate
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFundingRate[]>> GetFundingRatesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get klines
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#k-line-data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/kline/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>start</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end</c>"] Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get index price klines
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#k-line-data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/kline/index_price/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>start</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end</c>"] Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesKline[]>> GetIndexPriceKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get mark price klines
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#k-line-data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/kline/fair_price/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>start</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end</c>"] Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesKline[]>> GetMarkPriceKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-transaction-data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/deals/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get ticker for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-trend-data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get tickers
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-trend-data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/ticker
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get risk fund balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-all-contract-risk-fund-balance" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/risk_reverse
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcRiskFundBalance[]>> GetRiskFundBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get risk fund balance history
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-risk-fund-balance-history" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/risk_reverse/history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="page">["<c>page_num</c>"] Page number</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcRiskFundBalanceHistoryPage>> GetRiskFundBalanceHistoryAsync(string symbol, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-contract-funding-rate-history" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/funding_rate/history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="page">["<c>page_num</c>"] Page number</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFundingRateHistoryPage>> GetFundingRateHistoryAsync(string symbol, int? page = null, int? pageSize = null, CancellationToken ct = default);

    }
}
