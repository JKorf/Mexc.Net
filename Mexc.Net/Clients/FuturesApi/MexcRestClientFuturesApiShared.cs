using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Mexc.Net.Clients.SpotApi;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Clients.FuturesApi
{
    internal partial class MexcRestClientFuturesSharedApi :
        SharedApiBase,
        IMexcRestClientFuturesApiShared,
        IMexcRestClientFuturesSharedApi
    {
        private readonly MexcRestClientFuturesApi _api;

        private const string _topicId = "MexcFutures";
        private const string _exchangeName = "Mexc";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(MexcExchange.Metadata, this);

        public MexcRestClientFuturesSharedApi(MexcRestClientFuturesApi api)
            : base(
                  api.Exchange,
                  [TradingMode.PerpetualLinear, TradingMode.PerpetualInverse],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetKlinesOptions,
                GetOrderBookOptions,
                GetRecentTradesOptions,
                GetFundingRateHistoryOptions,
                GetFuturesSymbolsOptions,
                GetFuturesTickerOptions,
                GetAllFuturesTickersOptions,
                GetBalancesOptions,
                GetPositionModeOptions,
                SetPositionModeOptions,
                GetFeeOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetPositionHistoryOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                CancelFuturesOrderOptions,
                GetPositionsOptions,
                ClosePositionOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions,
                GetFuturesOrderByClientOrderIdOptions,
                CancelFuturesOrderByClientOrderIdOptions
                );
        }

        #region Klines client

        public GetKlinesOptions GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, true, false, true, 2000, false,
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

        public async Task<HttpResult<SharedKline[]>> GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.FuturesKlineInterval)request.Interval;

            var validationError = GetKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedKline[]>(Exchange, validationError);

            int limit = 2000;
            var direction = DataDirection.Ascending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetKlinesAsync(
                symbol,
                interval,
                pageParams.StartTime,
                pageParams.EndTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequestKlines(
                     () => Pagination.NextPageFromTimeKlines(direction, request, result.Data.Max(x => x.OpenTime), limit),
                     result.Data.Length,
                     result.Data.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     request.Interval);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedKline(
                            request.Symbol, 
                            symbol, 
                            x.OpenTime,
                            x.ClosePrice,
                            x.HighPrice,
                            x.LowPrice,
                            x.OpenPrice,
                            new SharedOrderQuantity(null, x.QuoteVolume, x.Volume)))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Order Book client
        public GetOrderBookOptions GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, 0, 1500, false);
        public async Task<HttpResult<SharedOrderBook>> GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = GetOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOrderBook>(Exchange, validationError);

            var result = await _api.ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit ?? 20,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOrderBook>(result);

            return HttpResult.Ok(result, new SharedOrderBook(SharedQuantityType.Contracts, result.Data.SequenceEnd, result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Recent Trade client

        public GetRecentTradesOptions GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 100, false);
        public async Task<HttpResult<SharedTrade[]>> GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = GetRecentTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetRecentTradesAsync(
                symbol,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            return HttpResult.Ok(result, result.Data.Take(request.Limit ?? 100).Select(x =>
            new SharedTrade(request.Symbol, symbol, new SharedOrderQuantity(contractQuantity: x.Quantity), x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Funding Rate client
        public GetFundingRateHistoryOptions GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(_exchangeName, false, true, false, 1000, false);

        public async Task<HttpResult<SharedFundingRate[]>> GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFundingRateHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingRate[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.ExchangeData.GetFundingRateHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                page: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingRate[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Data.Min(x => x.SettleTime)),
                     result.Data.Data.Length,
                     result.Data.Data.Select(x => x.SettleTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Data, x => x.SettleTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedFundingRate(x.FundingRate, x.SettleTime))
                    .ToArray(), nextPageRequest);
        }
        #endregion

        #region Futures Symbol client

        public SharedSymbolCatalog? FuturesSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicId, _api.EnvironmentName, null);
        public GetFuturesSymbolsOptions GetFuturesSymbolsOptions { get; } = new GetFuturesSymbolsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedFuturesSymbol[]>> GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesSymbol[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesSymbol[]>(result);

            var resultData =
                 result.Data
                .Select(x => ParseSymbol(x))
                .ToArray();

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, _api.EnvironmentName, null, resultData);
            return HttpResult.Ok(result, SharedUtils.ApplySymbolFilter(resultData, request));
        }

        private SharedFuturesSymbol ParseSymbol(MexcContract s)
        {
            var result = new SharedFuturesSymbol(
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
                QuantityStep = s.VolumeUnit,
                DisplayName = s.DisplayNameEnglish,
                UpperPriceLimitPercentage = s.AskLimitPriceRate * 100,
                LowerPriceLimitPercentage = -s.BidLimitPriceRate * 100,
                TakerFeePercentage = s.TakerFeeRate * 100,
                MakerFeePercentage = s.MakerFeeRate * 100                
            };

            if (result.TradingMode.IsInverse())
            {
                result.QuoteAssetType = SharedAssetType.Fiat;
            }
            else
            {
                result.QuoteAssetType = SharedAssetType.Crypto;
                result.QuoteAssetSubType = SharedAssetSubType.StableCoin;
            }

            if (s.ConceptPlate.Contains("mc-trade-zone-tradfi"))
            {
                result.BaseAssetType = SharedAssetType.TradFi;
                if (s.ConceptPlate.Contains("mc-trade-zone-Stock"))
                    result.BaseAssetSubType = SharedAssetSubType.Equity;
                else if (s.ConceptPlate.Contains("mc-trade-zone-Commodities"))
                    result.BaseAssetSubType = SharedAssetSubType.Commodity;
                else if (s.ConceptPlate.Contains("mc-trade-zone-OIL"))
                    result.BaseAssetSubType = SharedAssetSubType.Commodity;
            }
            else
            {
                result.BaseAssetType = SharedAssetType.Crypto;
            }


            return result;
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbolName));
        }
        #endregion

        #region Futures Ticker client

        public GetFuturesTickerOptions GetFuturesTickerOptions { get; } = new GetFuturesTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(result);

            return HttpResult.Ok(result, new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, result.Data.Symbol),
                    result.Data.Symbol,
                    result.Data.LastPrice,
                    result.Data.HighPrice,
                    result.Data.LowPrice,
                    new SharedOrderQuantity(null, result.Data.QuoteVolume24h, result.Data.Volume24h),
                    result.Data.ChangePercentage)
                {
                    IndexPrice = result.Data.IndexPrice,
                    MarkPrice = result.Data.MarkPrice,
                    FundingRate = result.Data.FundingRate
                });
        }

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(request, ct);
        GetAllFuturesTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllFuturesTickersOptions;

        public GetAllFuturesTickersOptions GetAllFuturesTickersOptions { get; } = new GetAllFuturesTickersOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllFuturesTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(result);

            IEnumerable<MexcFuturesTicker> data = result.Data;
            if (request.TradingMode != null)
            {
                data = data.Where(x =>
                    request.TradingMode == TradingMode.PerpetualLinear ? !x.Symbol.EndsWith("_USD") : x.Symbol.EndsWith("_USD"));
            }

            return HttpResult.Ok(result, result.Data.Select(x =>
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice, 
                    new SharedOrderQuantity(null, x.QuoteVolume24h, x.Volume24h),
                    x.ChangePercentage)
                {
                    IndexPrice = x.IndexPrice,
                    MarkPrice = x.MarkPrice,
                    FundingRate = x.FundingRate
                }
            ).ToArray());
        }

        #endregion

        #region Balance Client
        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Futures);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            var result = await _api.Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedBalance[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x => 
                new SharedBalance(
                    SupportedTradingModes, 
                    x.Asset,
                    x.AvailableBalance, 
                    x.AvailableBalance + x.FrozenBalance)).ToArray());
        }

        #endregion

        #region Position Mode client
        public SharedPositionModeSelection PositionModeSettingType => SharedPositionModeSelection.PerAccount;

        public GetPositionModeOptions GetPositionModeOptions { get; } = new GetPositionModeOptions(_exchangeName);
        public async Task<HttpResult<SharedPositionModeResult>> GetPositionModeAsync(GetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = GetPositionModeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionModeResult>(Exchange, validationError);

            var result = await _api.Account.GetPositionModeAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionModeResult>(result);

            return HttpResult.Ok(result, new SharedPositionModeResult(result.Data == PositionMode.Hedge ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));
        }

        public SetPositionModeOptions SetPositionModeOptions { get; } = new SetPositionModeOptions(_exchangeName);
        public async Task<HttpResult<SharedPositionModeResult>> SetPositionModeAsync(SetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = SetPositionModeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionModeResult>(Exchange, validationError);

            var result = await _api.Account.SetPositionModeAsync(request.PositionMode == SharedPositionMode.HedgeMode ? PositionMode.Hedge : PositionMode.OneWay, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionModeResult>(result);

            return HttpResult.Ok(result, new SharedPositionModeResult(request.PositionMode));
        }
        #endregion

        #region Fee Client
        public GetFeeOptions GetFeeOptions { get; } = new GetFeeOptions(_exchangeName, true);

        public async Task<HttpResult<SharedFee>> GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = GetFeeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFee>(Exchange, validationError);

            // Get data
            var result = await _api.Account.GetTradingFeesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFee>(result);

            // Return
            return HttpResult.Ok(result, new SharedFee(result.Data.MakerFee * 100, result.Data.TakerFee * 100));
        }
        #endregion

        #region Leverage client
        public SharedLeverageSettingMode LeverageSettingType => SharedLeverageSettingMode.PerSymbol;

        public GetLeverageOptions GetLeverageOptions { get; } = new GetLeverageOptions(_exchangeName, true);
        public async Task<HttpResult<SharedLeverage>> GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.GetLeverageAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            if (!result.Data.Any())
                return HttpResult.Fail<SharedLeverage>(result, new ServerError(new ErrorInfo(ErrorType.Unknown, "Not found")));

            return HttpResult.Ok(result, new SharedLeverage(
                result.Data.FirstOrDefault(x => request.PositionSide == null 
            || (x.PositionSide == (request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short)))?.Leverage
            ?? result.Data.First().Leverage)
            {
                Side = request.PositionSide
            });
        }

        public SetLeverageOptions SetLeverageOptions { get; } = new SetLeverageOptions(_exchangeName)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(SetLeverageRequest.Side), typeof(SharedPositionSide), "Position side", SharedPositionSide.Short),
                new ParameterDescription(nameof(SetLeverageRequest.MarginMode), typeof(SharedMarginMode), "Margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedLeverage>> SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = SetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.SetLeverageAsync((int)request.Leverage,
                marginType: request.MarginMode == SharedMarginMode.Isolated ? MarginType.Isolated : MarginType.Cross,
                positionSide: request.Side == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                symbol: request.Symbol!.GetSymbol(FormatSymbol), 
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(request.Leverage));
        }
        #endregion

        #region Position History client

        public GetPositionHistoryOptions GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(_exchangeName, false, true, false, 100);
        public async Task<HttpResult<SharedPositionHistory[]>> GetPositionHistoryAsync(GetPositionHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetPositionHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionHistory[]>(Exchange, validationError);

            int limit = request.Limit ?? 20;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.Trading.GetPositionHistoryAsync(
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                page: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionHistory[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.CreateTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedPositionHistory(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                            x.OpenAveragePrice,
                            x.CloseAveragePrice,
                            new SharedOrderQuantity(contractQuantity: x.CloseVolume),
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

        #region Futures Order Client

        public SharedFeeDeductionType FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        public SharedFeeAssetType FuturesFeeAssetType => SharedFeeAssetType.InputAsset;
        public SharedOrderType[] FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        public SharedTimeInForce[] FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        public SharedQuantitySupport FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        public string GenerateClientOrderId() => ExchangeHelpers.RandomString(20);

        public PlaceFuturesOrderOptions PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, false)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.PositionSide), typeof(SharedPositionSide), "Position side", SharedPositionSide.Long),
            }
        };
        public async Task<HttpResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    GetOrderSide(request.Side, request.PositionSide!.Value),
                    GetOrderType(request.OrderType, request.TimeInForce),
                    quantity: request.Quantity?.QuantityInContracts ?? 0,
                    price: request.Price,
                    leverage: (int?)request.Leverage,
                    marginType: request.MarginMode == SharedMarginMode.Isolated ? MarginType.Isolated : MarginType.Cross,
                    reduceOnly: request.ReduceOnly,
                    clientOrderId: request.ClientOrderId,
                    stopLossPrice: request.StopLossPrice,
                    takeProfitPrice: request.TakeProfitPrice,
                    ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
        }

        private FuturesOrderType GetOrderType(SharedOrderType orderType, SharedTimeInForce? timeInForce)
        {
            if (orderType == SharedOrderType.LimitMaker)
                return FuturesOrderType.LimitMaker;
            if (orderType == SharedOrderType.Market)
                return FuturesOrderType.Market;
            if (timeInForce == SharedTimeInForce.FillOrKill)
                return FuturesOrderType.FillOrKill;
            if (timeInForce == SharedTimeInForce.ImmediateOrCancel)
                return FuturesOrderType.ImmediateOrCancel;

            return FuturesOrderType.Limit;
        }

        private FuturesOrderSide GetOrderSide(SharedOrderSide side, SharedPositionSide posSide)
        {
            if (posSide == SharedPositionSide.Long)
            {
                if (side == SharedOrderSide.Buy) return FuturesOrderSide.OpenLong;
                return FuturesOrderSide.CloseLong;
            }

            if (side == SharedOrderSide.Buy) return FuturesOrderSide.CloseShort;
            return FuturesOrderSide.OpenShort;
        }

        public GetFuturesOrderOptions GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.GetOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                order.Data.OrderType == FuturesOrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                (order.Data.OrderSide == FuturesOrderSide.CloseShort || order.Data.OrderSide == FuturesOrderSide.OpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.Quantity)),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.QuantityFilled)),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = order.Data.OrderSide == FuturesOrderSide.OpenLong || order.Data.OrderSide == FuturesOrderSide.CloseLong ? SharedPositionSide.Long : SharedPositionSide.Short,
                Leverage = order.Data.Leverage,
                TakeProfitPrice = order.Data.TakeProfitPrice,
                StopLossPrice = order.Data.StopLossPrice,
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = order.Data.MakerFee + order.Data.TakerFee,
                FeeAsset = order.Data.FeeAsset
#pragma warning restore CS0618 // Type or member is obsolete
            });
        }

        public GetOpenFuturesOrdersOptions GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await _api.Trading.GetOpenOrdersAsync(ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(orders);

            var data = orders.Data.Where(x => x.Symbol == symbol).ToArray();
            return HttpResult.Ok(orders, data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId.ToString(),
                x.OrderType == FuturesOrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                (x.OrderSide == FuturesOrderSide.CloseShort || x.OrderSide == FuturesOrderSide.OpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                OrderPrice = x.Price == 0 ? null : x.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity)),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(x.QuantityFilled)),
                UpdateTime = x.UpdateTime,
                PositionSide = x.OrderSide == FuturesOrderSide.OpenLong || x.OrderSide == FuturesOrderSide.CloseLong ? SharedPositionSide.Long : SharedPositionSide.Short,
                Leverage = x.Leverage,
                TakeProfitPrice = x.TakeProfitPrice,
                StopLossPrice = x.StopLossPrice,
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = x.MakerFee + x.TakerFee,
                FeeAsset = x.FeeAsset
#pragma warning restore CS0618 // Type or member is obsolete
            }).ToArray());
        }

        public GetFuturesClosedOrdersOptions GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(90));

            // Get data
            var result = await _api.Trading.GetOrderHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                status: [FuturesOrderStatus.Canceled, FuturesOrderStatus.Filled],
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                page: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromPage(pageParams),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(90));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFuturesOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.OrderType == FuturesOrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                            (x.OrderSide == FuturesOrderSide.CloseShort || x.OrderSide == FuturesOrderSide.OpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(x.Status),
                            x.CreateTime)
                        {
                            ClientOrderId = x.ClientOrderId,
                            AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                            OrderPrice = x.Price == 0 ? null : x.Price,
                            OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity)),
                            QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(x.QuantityFilled)),
                            UpdateTime = x.UpdateTime,
                            PositionSide = x.OrderSide == FuturesOrderSide.OpenLong || x.OrderSide == FuturesOrderSide.CloseLong ? SharedPositionSide.Long : SharedPositionSide.Short,
                            Leverage = x.Leverage,
                            TakeProfitPrice = x.TakeProfitPrice,
                            StopLossPrice = x.StopLossPrice,
#pragma warning disable CS0618 // Type or member is obsolete
                            Fee = x.MakerFee + x.TakerFee,
                            FeeAsset = x.FeeAsset
#pragma warning restore CS0618 // Type or member is obsolete
                        })
                    .ToArray(), nextPageRequest);
        }

        public GetFuturesOrderTradesOptions GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id"));

            var orders = await _api.Trading.GetOrderTradesAsync(orderId: orderId, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity)),
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.Taker ? SharedRole.Taker : SharedRole.Maker
            }).ToArray());
        }

        Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetFuturesUserTradeHistoryAsync(request, pageRequest, ct);
        GetFuturesUserTradeHistoryOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions => GetFuturesUserTradeHistoryOptions;

        public GetFuturesUserTradeHistoryOptions GetFuturesUserTradeHistoryOptions { get; } = new GetFuturesUserTradeHistoryOptions(_exchangeName, false, true, true, 1000);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFuturesUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(90));

            // Get data
            var result = await _api.Trading.GetUserTradesAsync(request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                page: pageParams.Page ?? 1,
                pageSize: pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromPage(pageParams),
                     result.Data.Length,
                     result.Data.Select(x => x.Timestamp),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(90));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.Id.ToString(),
                            x.Side == FuturesOrderSide.OpenLong || x.Side == FuturesOrderSide.CloseShort ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity)),
                            x.Price,
                            x.Timestamp)
                        {
                            Fee = x.Fee,
                            FeeAsset = x.FeeAsset,
                            Role = x.Taker ? SharedRole.Taker : SharedRole.Maker
                        })
                    .ToArray(), nextPageRequest);
        }

        public CancelFuturesOrderOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelOrdersAsync([orderId]).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.SingleOrDefault()?.OrderId.ToString()!));
        }

        public GetPositionsOptions GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true);
        public async Task<HttpResult<SharedPosition[]>> GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);

            var result = await _api.Trading.GetPositionsAsync(request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPosition[]>(result);
            
            return HttpResult.Ok(result, result.Data.Select(x => 
            new SharedPosition(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol, 
                new SharedOrderQuantity(contractQuantity: Math.Abs(x.PositionSize)),
                x.UpdateTime)
            {
                Id = x.PositionId.ToString(),
                LiquidationPrice = x.LiquidationPrice,
                AverageOpenPrice = x.OpenAveragePrice,
                Leverage = x.Leverage,
                PositionSide = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
            }).ToArray());
        }

        public ClosePositionOptions ClosePositionOptions { get; } = new ClosePositionOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ClosePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.PositionSide == SharedPositionSide.Long ? FuturesOrderSide.CloseLong : FuturesOrderSide.CloseShort,
                FuturesOrderType.Market,
                0,
                flashClose: true,
                reduceOnly: true,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
        }

        private SharedOrderStatus ParseOrderStatus(FuturesOrderStatus status)
        {
            if (status == FuturesOrderStatus.Open)
                return SharedOrderStatus.Open;

            if (status == FuturesOrderStatus.Filled)
                return SharedOrderStatus.Filled;

            if (status == FuturesOrderStatus.Canceled || status == FuturesOrderStatus.Invalid)
                return SharedOrderStatus.Canceled;

            return SharedOrderStatus.Unknown;
        }
        #endregion

        #region Trigger Order Client
        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesTriggerOrderRequest.MarginMode), typeof(SharedMarginMode), "Margin mode", SharedMarginMode.Cross)
            }
        };
        public async Task<HttpResult<SharedId>> PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetTriggerOrderParameters(request.PositionSide, request.OrderDirection);
            var validationError = PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlacePlanOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                side: side,
                orderType: request.OrderPrice == null ? FuturesOrderType.Market : FuturesOrderType.Limit,
                executeCycle: ExecuteCycle.OneWeek,
                triggerType: request.PriceDirection == SharedTriggerPriceDirection.PriceBelow ? TriggerType.LessThanOrEqual : TriggerType.MoreThanOrEqual,
                marginType: request.MarginMode == SharedMarginMode.Isolated ? MarginType.Isolated : MarginType.Cross,
                leverage: (int?)request.Leverage,
                quantity: request.Quantity.QuantityInContracts ?? 0,
                triggerPrice: request.TriggerPrice,
                price: request.OrderPrice,
                triggerPriceType: GetWorkingType(request),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.ToString()));
        }

        private TriggerPriceType GetWorkingType(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.TriggerPriceType == null)
                return TriggerPriceType.LastPrice;

            if (request.TriggerPriceType == SharedTriggerPriceType.LastPrice)
                return TriggerPriceType.LastPrice;

            if (request.TriggerPriceType == SharedTriggerPriceType.MarkPrice)
                return TriggerPriceType.MarkPrice;

            return TriggerPriceType.IndexPrice;
        }

        public GetFuturesTriggerOrderOptions GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesTriggerOrder>> GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var id))
                throw new ArgumentException($"Invalid order id");

            var orders = await _api.Trading.GetTriggerOrdersAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(orders);

            var order = orders.Data.SingleOrDefault(x => x.Id == id);
            if (order == null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(orders, new ServerError(new ErrorInfo(ErrorType.UnknownOrder, $"Order with id {id} not found")));

            MexcFuturesOrder? placedOrder = null;
            if (order.OrderId > 0)
            {
                var placedOrderResult = await _api.Trading.GetOrderAsync(order.OrderId, ct: ct).ConfigureAwait(false);
                if (!placedOrderResult.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(placedOrderResult, new ServerError(new ErrorInfo(ErrorType.UnknownOrder, $"Order with id {id} not found")));

                placedOrder = placedOrderResult.Data;
            }

            // Return
            return HttpResult.Ok(orders, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Symbol),
                order.Symbol,
                order.Id.ToString(),
                order.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                order.Side == FuturesOrderSide.OpenShort || order.Side == FuturesOrderSide.OpenLong ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerStatus(order),
                order.TriggerPrice,
                order.Side == FuturesOrderSide.OpenLong || order.Side == FuturesOrderSide.CloseLong ? SharedPositionSide.Long : SharedPositionSide.Short,
                order.CreateTime
                )
            {
                AveragePrice = placedOrder?.AveragePrice == 0 ? null : placedOrder?.AveragePrice,
                OrderPrice = order.Price == 0 ? null : order.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Quantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: placedOrder?.QuantityFilled),
                UpdateTime = order.UpdateTime,
                PlacedOrderId = order.OrderId.ToString(),
            });
        }

        private SharedTriggerOrderStatus ParseTriggerStatus(MexcFuturesTriggerOrder data)
        {
            if (data.Status == TpSlStatus.Executed)
                return SharedTriggerOrderStatus.Triggered;

            if (data.Status == TpSlStatus.Canceled || data.Status == TpSlStatus.Failed)
                return SharedTriggerOrderStatus.CanceledOrRejected;

            if (data.Status == TpSlStatus.Untriggered)
                return SharedTriggerOrderStatus.Active;

            return SharedTriggerOrderStatus.Unknown;
        }

        public CancelFuturesTriggerOrderOptions CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelPlanOrdersAsync([new MexcCancelRequest { Symbol = request.Symbol!.GetSymbol(FormatSymbol), OrderId = orderId }], ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(orderId.ToString()));
        }


        private FuturesOrderSide GetTriggerOrderParameters(SharedPositionSide positionSide, SharedTriggerOrderDirection orderDirection)
        {
            if (orderDirection == SharedTriggerOrderDirection.Enter)
                return positionSide == SharedPositionSide.Long ? FuturesOrderSide.OpenLong : FuturesOrderSide.OpenShort;
            else
                // PriceAbove + Exit = TakeProfit Sell order
                return positionSide == SharedPositionSide.Long ? FuturesOrderSide.CloseLong : FuturesOrderSide.CloseShort;
        }

        #endregion

        #region Futures Client Id Order Client

        public GetFuturesOrderByClientOrderIdOptions GetFuturesOrderByClientOrderIdOptions { get; } = new GetFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderByClientOrderIdAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                order.Data.OrderType == FuturesOrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                (order.Data.OrderSide == FuturesOrderSide.CloseShort || order.Data.OrderSide == FuturesOrderSide.OpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.Quantity)),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.QuantityFilled)),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = order.Data.OrderSide == FuturesOrderSide.OpenLong || order.Data.OrderSide == FuturesOrderSide.CloseLong ? SharedPositionSide.Long : SharedPositionSide.Short,
                Leverage = order.Data.Leverage,
                TakeProfitPrice = order.Data.TakeProfitPrice,
                StopLossPrice = order.Data.StopLossPrice,
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = order.Data.MakerFee + order.Data.TakerFee,
                FeeAsset = order.Data.FeeAsset
#pragma warning restore CS0618 // Type or member is obsolete
            });
        }

        public CancelFuturesOrderByClientOrderIdOptions CancelFuturesOrderByClientOrderIdOptions { get; } = new CancelFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrdersByClientOrderIdsAsync(new[] { new MexcCancelRequest { ClientOrderId = request.OrderId, Symbol = request.Symbol!.GetSymbol(FormatSymbol) } }, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }
        #endregion
    }
}
