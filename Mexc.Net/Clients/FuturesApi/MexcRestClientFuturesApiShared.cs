using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.SharedApis;
using Mexc.Net.Objects.Models.Futures;
using CryptoExchange.Net.Objects.Errors;

namespace Mexc.Net.Clients.FuturesApi
{
    internal partial class MexcRestClientFuturesApi : IMexcRestClientFuturesApiShared
    {
        private const string _topicId = "MexcFutures";

        public string Exchange => MexcExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.PerpetualLinear, TradingMode.PerpetualInverse };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Klines client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(true, false, true, 2000, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.EightHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth);

        async Task<ExchangeWebResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.FuturesKlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.FuturesKlineInterval), interval))
                return new ExchangeWebResult<SharedKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedKline[]>(Exchange, validationError);

            int limit = 2000;
            var direction = DataDirection.Ascending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetKlinesAsync(
                symbol,
                interval,
                pageParams.StartTime,
                pageParams.EndTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedKline[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequestKlines(
                     () => Pagination.NextPageFromTimeKlines(direction, request, result.Data.Max(x => x.OpenTime), limit),
                     result.Data.Length,
                     result.Data.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     request.Interval);

            return result.AsExchangeResult(
                    Exchange,
                    request.Symbol.TradingMode,
                    ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(0, 1500, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit ?? 20,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(100, false);
        async Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetRecentTradesAsync(
                symbol,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedTrade[]>(Exchange, request.Symbol.TradingMode, result.Data.Take(request.Limit ?? 100).Select(x =>
            new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(false, true, false, 1000, false);

        async Task<ExchangeWebResult<SharedFundingRate[]>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IFundingRateRestClient)this).GetFundingRateHistoryOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFundingRate[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await ExchangeData.GetFundingRateHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                page: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFundingRate[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Data.Min(x => x.SettleTime)),
                     result.Data.Data.Length,
                     result.Data.Data.Select(x => x.SettleTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return result.AsExchangeResult(
                    Exchange,
                    request.Symbol.TradingMode,
                    ExchangeHelpers.ApplyFilter(result.Data.Data, x => x.SettleTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedFundingRate(x.FundingRate, x.SettleTime))
                    .ToArray(), nextPageRequest);
        }
        #endregion

        #region Futures Symbol client

        EndpointOptions<GetSymbolsRequest> IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesSymbol[]>(Exchange, null, default);

            IEnumerable<MexcContract> data = result.Data;
            if (request.TradingMode.HasValue)
            {
                data = data.Where(x =>
                    request.TradingMode == TradingMode.PerpetualLinear ? (x.BaseAsset == x.SettleAsset):
                       (x.BaseAsset != x.SettleAsset));
            }

            var response = result.AsExchangeResult<SharedFuturesSymbol[]>(Exchange,
                request.TradingMode == null ? SupportedTradingModes : new[] { request.TradingMode.Value },
                result.Data.Select(s => new SharedFuturesSymbol(
                s.BaseAsset == s.SettleAsset ? TradingMode.PerpetualInverse : TradingMode.PerpetualLinear,
                s.BaseAsset,
                s.QuoteAsset,
                s.Symbol,
                s.ContractStatus == ContractStatus.Enabled)
                {
                    MinTradeQuantity = s.MinQuantity,
                    MaxTradeQuantity = s.MaxQuantity,
                    PriceStep = s.PriceUnit,
                    ContractSize = s.ContractSize,
                    MaxLongLeverage = s.MaxLeverage,
                    MaxShortLeverage = s.MaxLeverage,
                    QuantityStep = s.VolumeUnit
                }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, response.Data);
            return response;
        }

        async Task<ExchangeResult<SharedSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<SharedSymbol[]>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<SharedSymbol[]>(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, baseAsset));
        }

        async Task<ExchangeResult<bool>> IFuturesSymbolRestClient.SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbol));
        }

        async Task<ExchangeResult<bool>> IFuturesSymbolRestClient.SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbolName));
        }
        #endregion

        #region Futures Ticker client

        GetTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new GetTickerOptions();
        async Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var result = await ExchangeData.GetTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesTicker>(Exchange, null, default);

            return result.AsExchangeResult(Exchange,
                request.Symbol.TradingMode,
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, result.Data.Symbol),
                    result.Data.Symbol,
                    result.Data.LastPrice,
                    result.Data.HighPrice,
                    result.Data.LowPrice,
                    result.Data.Volume24h,
                    result.Data.ChangePercentage)
                {
                    IndexPrice = result.Data.IndexPrice,
                    MarkPrice = result.Data.MarkPrice,
                    FundingRate = result.Data.FundingRate
                });
        }

        GetTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new GetTickersOptions();
        async Task<ExchangeWebResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickersOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesTicker[]>(Exchange, null, default);

            IEnumerable<MexcFuturesTicker> data = result.Data;
            if (request.TradingMode != null)
            {
                data = data.Where(x =>
                    request.TradingMode == TradingMode.PerpetualLinear ? !x.Symbol.EndsWith("_USD") : x.Symbol.EndsWith("_USD"));
            }

            return result.AsExchangeResult<SharedFuturesTicker[]>(Exchange,
                request.TradingMode == null ? SupportedTradingModes : new[] { request.TradingMode.Value },
                result.Data.Select(x =>
                new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume24h, x.ChangePercentage)
                {
                    IndexPrice = x.IndexPrice,
                    MarkPrice = x.MarkPrice,
                    FundingRate = x.FundingRate
                }
            ).ToArray());
        }

        #endregion

        #region Balance Client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(AccountTypeFilter.Futures);

        async Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBalance[]>(Exchange, validationError);

            var result = await Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, SupportedTradingModes, result.Data.Select(x => new SharedBalance(x.Asset, x.AvailableBalance, x.AvailableBalance + x.FrozenBalance)).ToArray());
        }

        #endregion

        #region Position Mode client
        SharedPositionModeSelection IPositionModeRestClient.PositionModeSettingType => SharedPositionModeSelection.PerAccount;

        GetPositionModeOptions IPositionModeRestClient.GetPositionModeOptions { get; } = new GetPositionModeOptions();
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.GetPositionModeAsync(GetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).GetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.GetPositionModeAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, SupportedTradingModes, new SharedPositionModeResult(result.Data == PositionMode.Hedge ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));
        }

        SetPositionModeOptions IPositionModeRestClient.SetPositionModeOptions { get; } = new SetPositionModeOptions();
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.SetPositionModeAsync(SetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).SetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.SetPositionModeAsync(request.PositionMode == SharedPositionMode.HedgeMode ? PositionMode.Hedge : PositionMode.OneWay, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, SupportedTradingModes, new SharedPositionModeResult(request.PositionMode));
        }
        #endregion

        #region Fee Client
        EndpointOptions<GetFeeRequest> IFeeRestClient.GetFeeOptions { get; } = new EndpointOptions<GetFeeRequest>(true);

        async Task<ExchangeWebResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = ((IFeeRestClient)this).GetFeeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetTradingFeesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFee>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, SupportedTradingModes, new SharedFee(result.Data.MakerFee * 100, result.Data.TakerFee * 100));
        }
        #endregion

        #region Leverage client
        SharedLeverageSettingMode ILeverageRestClient.LeverageSettingType => SharedLeverageSettingMode.PerSymbol;

        EndpointOptions<GetLeverageRequest> ILeverageRestClient.GetLeverageOptions { get; } = new EndpointOptions<GetLeverageRequest>(true);
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).GetLeverageOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Account.GetLeverageAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, null, default);

            if (!result.Data.Any())
                return result.AsExchangeError<SharedLeverage>(Exchange, new ServerError(new ErrorInfo(ErrorType.Unknown, "Not found")));

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedLeverage(
                result.Data.FirstOrDefault(x => request.PositionSide == null 
            || (x.PositionSide == (request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short)))?.Leverage
            ?? result.Data.First().Leverage)
            {
                Side = request.PositionSide
            });
        }

        SetLeverageOptions ILeverageRestClient.SetLeverageOptions { get; } = new SetLeverageOptions()
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(SetLeverageRequest.Side), typeof(SharedPositionSide), "Position side", SharedPositionSide.Short),
                new ParameterDescription(nameof(SetLeverageRequest.MarginMode), typeof(SharedMarginMode), "Margin mode", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).SetLeverageOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Account.SetLeverageAsync((int)request.Leverage,
                marginType: request.MarginMode == SharedMarginMode.Isolated ? MarginType.Isolated : MarginType.Cross,
                positionSide: request.Side == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                symbol: request.Symbol!.GetSymbol(FormatSymbol), 
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedLeverage(request.Leverage));
        }
        #endregion

        #region Position History client

        GetPositionHistoryOptions IPositionHistoryRestClient.GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(false, true, false, 100);
        async Task<ExchangeWebResult<SharedPositionHistory[]>> IPositionHistoryRestClient.GetPositionHistoryAsync(GetPositionHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IPositionHistoryRestClient)this).GetPositionHistoryOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionHistory[]>(Exchange, validationError);

            int limit = request.Limit ?? 20;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await Trading.GetPositionHistoryAsync(
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                page: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionHistory[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.CreateTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return result.AsExchangeResult(
                    Exchange,
                    request.Symbol.TradingMode,
                    ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedPositionHistory(
                            ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol),
                            x.Symbol,
                            x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                            x.OpenAveragePrice,
                            x.CloseAveragePrice,
                            x.CloseVolume,
                            x.RealisedPnl,
                            x.CreateTime)
                        {
                            Leverage = x.Leverage,
                            PositionId = x.PositionId.ToString(),
                            RealizedPnl = x.RealisedPnl                
                        })
                    .ToArray(), nextPageRequest);
        }
        #endregion
    }
}
