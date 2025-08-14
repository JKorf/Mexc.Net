using System.Diagnostics;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Models;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class MexcRestClientSpotApiExchangeData : IMexcRestClientSpotApiExchangeData
    {
        private readonly MexcRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal MexcRestClientSpotApiExchangeData(MexcRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Ping

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/ping", MexcExchange.RateLimiter.SpotRest, 1);
            var result = await _baseClient.SendAsync<object>(request, null, ct).ConfigureAwait(false);
            sw.Stop();
            return result ? result.As(sw.ElapsedMilliseconds) : result.As<long>(default!);
        }

        #endregion

        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/time", MexcExchange.RateLimiter.SpotRest, 1);
            var result = await _baseClient.SendAsync<MexcServerTime>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.ServerTime ?? default);
        }

        #endregion

        #region Get Api Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<string[]>> GetApiSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/defaultSymbols", MexcExchange.RateLimiter.SpotRest, 1);
            var result = await _baseClient.SendAsync<MexcResult<string[]>>(request, null, ct).ConfigureAwait(false);
            if (!result)
                return result.As<string[]>(default);

            if (result.Data.Code != 0)
                return result.AsError<string[]>(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message!)));

            return result.As(result.Data.Data!);
        }

        #endregion

        #region Get Exchange Info

        /// <inheritdoc />
        public async Task<WebCallResult<MexcExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbols", symbols != null ? string.Join(",", symbols): null);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/exchangeInfo", MexcExchange.RateLimiter.SpotRest, 10);
            return await _baseClient.SendAsync<MexcExchangeInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<MexcOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 5000);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/depth", MexcExchange.RateLimiter.SpotRest, 1);
            return await _baseClient.SendAsync<MexcOrderBook>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<WebCallResult<MexcTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/trades", MexcExchange.RateLimiter.SpotRest, 5);
            return await _baseClient.SendAsync<MexcTrade[]>(request, parameters, ct).ConfigureAwait(false);            
        }

        #endregion

        #region Get Aggregated Trades List

        /// <inheritdoc />
        public async Task<WebCallResult<MexcAggregatedTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/aggTrades", MexcExchange.RateLimiter.SpotRest, 1);
            return await _baseClient.SendAsync<MexcAggregatedTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<MexcKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/klines", MexcExchange.RateLimiter.SpotRest, 1);
            return await _baseClient.SendAsync<MexcKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Average Price

        /// <inheritdoc />
        public async Task<WebCallResult<MexcAveragePrice>> GetAveragePriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/avgPrice", MexcExchange.RateLimiter.SpotRest, 1);
            return await _baseClient.SendAsync<MexcAveragePrice>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<MexcTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/ticker/24hr", MexcExchange.RateLimiter.SpotRest, 1);
            return await _baseClient.SendAsync<MexcTicker>(request, parameters, ct, 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<MexcTicker[]>> GetTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/ticker/24hr", MexcExchange.RateLimiter.SpotRest, 40);
            return await _baseClient.SendAsync<MexcTicker[]>(request, null, ct, 40).ConfigureAwait(false);
        }

        #endregion

        #region Get Prices

        /// <inheritdoc />
        public async Task<WebCallResult<MexcPrice[]>> GetPricesAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbols", symbols == null ? null : string.Join(",", symbols));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/ticker/price", MexcExchange.RateLimiter.SpotRest, 2);
            return await _baseClient.SendAsync<MexcPrice[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Book Price

        /// <inheritdoc />
        public async Task<WebCallResult<MexcBookPrice>> GetBookPricesAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/ticker/bookTicker", MexcExchange.RateLimiter.SpotRest, 1);
            return await _baseClient.SendAsync<MexcBookPrice>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Book Prices

        /// <inheritdoc />
        public async Task<WebCallResult<MexcBookPrice[]>> GetBookPricesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/ticker/bookTicker", MexcExchange.RateLimiter.SpotRest, 1);
            return await _baseClient.SendAsync<MexcBookPrice[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
