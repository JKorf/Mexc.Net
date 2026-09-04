using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Models.Spot;
using System.Linq;

namespace Mexc.Net.Clients.SpotApi
{
    internal partial class MexcRestClientSpotSharedApi :
        SharedApiBase,
        IMexcRestClientSpotApiShared,
        IMexcRestClientSpotSharedApi
    {
        private readonly MexcRestClientSpotApi _api;

        private const string _topicId = "MexcSpot";
        private const string _exchangeName = "Mexc";
        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(MexcExchange.Metadata, this);

        private static readonly HashSet<string> _knownCommodities = ["GOLD(PAXG)", "GOLD(XAUT)", "SLVON", "KAG", "XU3O8", "COPXON", "PALLON", "IAUON", "GGBR", "XGZ", "UNGON"];
        private static readonly HashSet<string> _knownFiat = ["EUR", "USD", "BRL"];

        public MexcRestClientSpotSharedApi(MexcRestClientSpotApi api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetKlinesOptions,
                GetSpotSymbolsOptions,
                GetSpotTickerOptions,
                GetAllSpotTickersOptions,
                GetBookTickerOptions,
                GetRecentTradesOptions,
                GetBalancesOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                GetSpotUserTradeHistoryOptions,
                GetSpotOrderTradesOptions,
                CancelSpotOrderOptions,
                GetAssetOptions,
                GetAllAssetsOptions,
                GetDepositAddressesOptions,
                GetDepositHistoryOptions,
                GetOrderBookOptions,
                GetWithdrawalHistoryOptions,
                WithdrawOptions,
                GetFeeOptions,
                TransferOptions,
                GetSpotOrderByClientOrderIdOptions,
                CancelSpotOrderByClientOrderIdOptions
            );
        }

    }
}
