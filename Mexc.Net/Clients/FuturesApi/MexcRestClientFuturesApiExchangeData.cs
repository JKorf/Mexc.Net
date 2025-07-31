using System.Diagnostics;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Objects.Models;
using CryptoExchange.Net.RateLimiting.Guards;

namespace Mexc.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class MexcRestClientFuturesApiExchangeData : IMexcRestClientFuturesApiExchangeData
    {
        private readonly MexcRestClientFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal MexcRestClientFuturesApiExchangeData(MexcRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }


        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/contract/ping", MexcExchange.RateLimiter.SpotRest, 1, false,
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long>(request, null, ct).ConfigureAwait(false);
            return result.As(DateTimeConverter.ParseFromDouble(result.Data));
        }

        #endregion

        #region Get Contracts

        /// <inheritdoc />
        public async Task<WebCallResult<MexcContract[]>> GetSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/contract/detail", MexcExchange.RateLimiter.FuturesRest, 1, false, 
                limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<MexcContract[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transferable Assets

        /// <inheritdoc />
        public async Task<WebCallResult<string[]>> GetTransferableAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/contract/support_currencies", MexcExchange.RateLimiter.FuturesRest, 1, false,
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<string[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contract/depth/{symbol}", MexcExchange.RateLimiter.FuturesRest, 1, false,
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Price

        /// <inheritdoc />
        public async Task<WebCallResult<MexcIndexPrice>> GetIndexPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contract/index_price/{symbol}", MexcExchange.RateLimiter.FuturesRest, 1, false,
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcIndexPrice>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Mark Price

        /// <inheritdoc />
        public async Task<WebCallResult<MexcMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contract/fair_price/{symbol}", MexcExchange.RateLimiter.FuturesRest, 1, false,
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcMarkPrice>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contract/funding_rate/{symbol}", MexcExchange.RateLimiter.FuturesRest, 1, false,
                limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFundingRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalSeconds("start", startTime);
            parameters.AddOptionalSeconds("end", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contract/kline/{symbol}", MexcExchange.RateLimiter.FuturesRest, 1, false, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesKlines>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.As<MexcFuturesKline[]>(default);

            var klines = new List<MexcFuturesKline>();
            for(var i = 0; i < result.Data.OpenTime.Length; i++)
            {
                klines.Add(new MexcFuturesKline
                {
                    ClosePrice = result.Data.ClosePrice[i],
                    HighPrice = result.Data.HighPrice[i],
                    LowPrice = result.Data.LowPrice[i],
                    OpenPrice = result.Data.OpenPrice[i],
                    OpenTime = result.Data.OpenTime[i],
                    QuoteVolume = result.Data.QuoteVolume[i],
                    Volume = result.Data.Volume[i],
                });
            }

            return result.As<MexcFuturesKline[]>(klines.ToArray());
        }

        #endregion

        #region Get Index Price Klines

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesKline[]>> GetIndexPriceKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalSeconds("start", startTime);
            parameters.AddOptionalSeconds("end", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contract/kline/index_price/{symbol}", MexcExchange.RateLimiter.FuturesRest, 1, false, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesKlines>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.As<MexcFuturesKline[]>(default);

            var klines = new List<MexcFuturesKline>();
            for (var i = 0; i < result.Data.OpenTime.Length; i++)
            {
                klines.Add(new MexcFuturesKline
                {
                    ClosePrice = result.Data.ClosePrice[i],
                    HighPrice = result.Data.HighPrice[i],
                    LowPrice = result.Data.LowPrice[i],
                    OpenPrice = result.Data.OpenPrice[i],
                    OpenTime = result.Data.OpenTime[i],
                    QuoteVolume = result.Data.QuoteVolume[i],
                    Volume = result.Data.Volume[i],
                });
            }

            return result.As<MexcFuturesKline[]>(klines.ToArray());
        }

        #endregion

        #region Get Mark Price Klines

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesKline[]>> GetMarkPriceKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalSeconds("start", startTime);
            parameters.AddOptionalSeconds("end", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contract/kline/fair_price/{symbol}", MexcExchange.RateLimiter.FuturesRest, 1, false, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesKlines>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.As<MexcFuturesKline[]>(default);

            var klines = new List<MexcFuturesKline>();
            for (var i = 0; i < result.Data.OpenTime.Length; i++)
            {
                klines.Add(new MexcFuturesKline
                {
                    ClosePrice = result.Data.ClosePrice[i],
                    HighPrice = result.Data.HighPrice[i],
                    LowPrice = result.Data.LowPrice[i],
                    OpenPrice = result.Data.OpenPrice[i],
                    OpenTime = result.Data.OpenTime[i],
                    QuoteVolume = result.Data.QuoteVolume[i],
                    Volume = result.Data.Volume[i],
                });
            }

            return result.As<MexcFuturesKline[]>(klines.ToArray());
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contract/deals/{symbol}", MexcExchange.RateLimiter.FuturesRest, 1, false, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/contract/ticker", MexcExchange.RateLimiter.FuturesRest, 1, false, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesTicker>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFuturesTicker[]>> GetTickersAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/contract/ticker", MexcExchange.RateLimiter.FuturesRest, 1, false, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Risk Fund Balances

        /// <inheritdoc />
        public async Task<WebCallResult<MexcRiskFundBalance[]>> GetRiskFundBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/contract/risk_reverse", MexcExchange.RateLimiter.FuturesRest, 1, false, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcRiskFundBalance[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Risk Fund Balance History

        /// <inheritdoc />
        public async Task<WebCallResult<MexcRiskFundBalanceHistoryPage>> GetRiskFundBalanceHistoryAsync(string symbol, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("page_num", page ?? 1);
            parameters.Add("page_size", pageSize ?? 20);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/contract/risk_reverse/history", MexcExchange.RateLimiter.FuturesRest, 1, false, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcRiskFundBalanceHistoryPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<MexcFundingRateHistoryPage>> GetFundingRateHistoryAsync(string symbol, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("page_num", page ?? 1);
            parameters.Add("page_size", pageSize ?? 20);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v1/contract/funding_rate/history", MexcExchange.RateLimiter.FuturesRest, 1, false, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFundingRateHistoryPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
