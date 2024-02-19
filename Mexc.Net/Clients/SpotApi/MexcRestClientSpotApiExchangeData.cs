using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Models;
using System.Linq;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class MexcRestClientSpotApiExchangeData : IMexcRestClientSpotApiExchangeData
    {
        private readonly ILogger _logger;
        private readonly MexcRestClientSpotApi _baseClient;

        internal MexcRestClientSpotApiExchangeData(ILogger logger, MexcRestClientSpotApi baseClient)
        {
            _logger = logger;
            _baseClient = baseClient;
        }

        #region Ping

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var result = await _baseClient.SendRequestInternal<object>("/api/v3/ping", HttpMethod.Get, ct).ConfigureAwait(false);
            sw.Stop();
            return result ? result.As(sw.ElapsedMilliseconds) : result.As<long>(default!);
        }

        #endregion

        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<MexcServerTime>("/api/v3/time", HttpMethod.Get, ct).ConfigureAwait(false);
            return result.As(result.Data?.ServerTime ?? default);
        }

        #endregion

        #region Get Api Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<string>>> GetApiSymbolsAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<MexcResult<IEnumerable<string>>>("/api/v3/defaultSymbols", HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<string>>(default);

            if (result.Data.Code != 0)
                return result.AsError<IEnumerable<string>>(new ServerError(result.Data.Code, result.Data.Message!));

            return result.As(result.Data.Data!);
        }

        #endregion

        #region Get Exchange Info

        /// <inheritdoc />
        public async Task<WebCallResult<MexcExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbols", symbols != null ? string.Join(",", symbols): null);
            return await _baseClient.SendRequestInternal<MexcExchangeInfo>("/api/v3/exchangeInfo", HttpMethod.Get, ct, parameters).ConfigureAwait(false);
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
            return await _baseClient.SendRequestInternal<MexcOrderBook>("/api/v3/depth", HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcTrade>>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptional("limit", limit);
            return await _baseClient.SendRequestInternal<IEnumerable<MexcTrade>>("/api/v3/trades", HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get Aggregated Trades List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MecxAggregatedTrade>>> GetAggregatedTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            return await _baseClient.SendRequestInternal<IEnumerable<MecxAggregatedTrade>>("/api/v3/aggTrades", HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MecxKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
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
            return await _baseClient.SendRequestInternal<IEnumerable<MecxKline>>("/api/v3/klines", HttpMethod.Get, ct, parameters).ConfigureAwait(false);
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
            return await _baseClient.SendRequestInternal<MexcAveragePrice>("/api/v3/avgPrice", HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<MexcTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            return await _baseClient.SendRequestInternal<MexcTicker>("/api/v3/ticker/24hr", HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcTicker>>> GetTickersAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<MexcTicker>>("/api/v3/ticker/24hr", HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Prices

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcPrice>>> GetPricesAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbols", symbols == null ? null : string.Join(",", symbols));
            return await _baseClient.SendRequestInternal<IEnumerable<MexcPrice>>("/api/v3/ticker/price", HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get Book Prices

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<MexcBookPrice>>> GetBookPricesAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbols", symbols == null ? null : string.Join(",", symbols));
            return await _baseClient.SendRequestInternal<IEnumerable<MexcBookPrice>>("/api/v3/ticker/bookTicker", HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion
    }
}
