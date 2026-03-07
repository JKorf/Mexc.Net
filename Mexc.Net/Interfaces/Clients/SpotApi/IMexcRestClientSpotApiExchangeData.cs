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
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#test-connectivity" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ping
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#check-server-time" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a list of symbols supported by the API
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#api-default-symbol" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/defaultSymbols
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string[]>> GetApiSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the exchange info, including symbol info
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#exchange-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbols, for example `BTCUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#order-book" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/depth
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `BTCUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows, max 500</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of the most recent trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#recent-trades-list" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/trades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `BTCUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of aggregated trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#compressed-aggregate-trades-list" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/aggTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `BTCUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcAggregatedTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#kline-candlestick-data" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/klines
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `BTCUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get average price for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#current-average-price" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/avgPrice
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `BTCUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcAveragePrice>> GetAveragePriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get 24h price statistics
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#24hr-ticker-price-change-statistics" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `BTCUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get 24h price statistics
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#24hr-ticker-price-change-statistics" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the last symbol prices
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#symbol-price-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/price
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbol, for example `BTCUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcPrice[]>> GetPricesAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get the best book prices
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#symbol-order-book-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `BTCUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcBookPrice>> GetBookPricesAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the best book prices
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#symbol-order-book-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcBookPrice[]>> GetBookPricesAsync(CancellationToken ct = default);

    }
}
