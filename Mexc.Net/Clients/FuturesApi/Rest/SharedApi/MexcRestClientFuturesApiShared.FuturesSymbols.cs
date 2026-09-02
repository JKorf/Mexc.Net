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
    internal partial class MexcRestClientFuturesSharedApi
    {
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
    }
}
