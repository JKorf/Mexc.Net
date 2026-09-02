using Mexc.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using CryptoExchange.Net;

namespace Mexc.Net.Clients.FuturesApi
{
    internal partial class MexcSocketClientFuturesSharedApi :
        SharedApiBase,
        IMexcSocketClientFuturesApiShared,
        IMexcSocketClientFuturesSharedApi
    {
        private readonly MexcSocketClientFuturesApi _api;

        private const string _topicId = "MexcFutures";
        private const string _exchangeName = "Mexc";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(MexcExchange.Metadata, this);

        public MexcSocketClientFuturesSharedApi(MexcSocketClientFuturesApi api)
            : base(
                  api.Exchange,
                  [TradingMode.PerpetualLinear, TradingMode.PerpetualInverse],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeTickerOptions,
                SubscribeAllTickersOptions,
                SubscribeTradeOptions,
                SubscribeBalanceOptions,
                SubscribePositionOptions,
                SubscribeFuturesOrderOptions,
                SubscribeUserTradeOptions
                );
        }

    }
}
