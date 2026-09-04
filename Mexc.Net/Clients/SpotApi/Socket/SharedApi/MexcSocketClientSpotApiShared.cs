using Mexc.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using CryptoExchange.Net;

namespace Mexc.Net.Clients.SpotApi
{
    internal partial class MexcSocketClientSpotSharedApi
        : SharedApiBase, 
        IMexcSocketClientSpotApiShared,
        IMexcSocketClientSpotSharedApi
    {
        private readonly MexcSocketClientSpotApi _api;

        private const string _topicId = "MexcSpot";
        private const string _exchangeName = "Mexc";

        public override  SharedClientInfo Discover() => SharedUtils.GetClientInfo(MexcExchange.Metadata, this);

        public MexcSocketClientSpotSharedApi(MexcSocketClientSpotApi api)
            : base(
                  SharedTransport.Socket,
                  api.Exchange,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeBalanceOptions,
                SubscribeSpotOrderOptions,
                SubscribeUserTradeOptions,
                SubscribeAllTickersOptions
                );
        }

    }
}
