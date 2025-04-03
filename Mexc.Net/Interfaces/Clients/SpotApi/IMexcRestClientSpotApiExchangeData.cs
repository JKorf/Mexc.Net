using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Mexc Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IMexcRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Ping the server, returns the response time in milliseconds
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#test-connectivity" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the server time
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#check-server-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a list of symbols supported by the API
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#api-default-symbol" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string[]>> GetApiSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the exchange info, including symbol info
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#exchange-information" /></para>
        /// </summary>
        /// <param name="symbols">Filter by symbols, for example `BTCUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current order book
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#order-book" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="limit">Number of rows, max 500</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of the most recent trades
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#recent-trades-list" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="limit">Number of rows, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of aggregated trades
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#compressed-aggregate-trades-list" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Number of rows, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcAggregatedTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#kline-candlestick-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Number of rows, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get average price for a symbol
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#current-average-price" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcAveragePrice>> GetAveragePriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get 24h price statistics
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#24hr-ticker-price-change-statistics" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get 24h price statistics
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#24hr-ticker-price-change-statistics" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the last symbol prices
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#symbol-price-ticker" /></para>
        /// </summary>
        /// <param name="symbols">Filter by symbol, for example `BTCUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcPrice[]>> GetPricesAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get the best book prices
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#symbol-order-book-ticker" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `BTCUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcBookPrice>> GetBookPricesAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the best book prices
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#symbol-order-book-ticker" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcBookPrice[]>> GetBookPricesAsync(CancellationToken ct = default);

    }
}
