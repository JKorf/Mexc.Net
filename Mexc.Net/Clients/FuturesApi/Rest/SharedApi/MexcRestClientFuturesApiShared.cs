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

    }
}
