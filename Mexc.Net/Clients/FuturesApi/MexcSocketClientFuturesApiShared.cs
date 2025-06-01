using Mexc.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;

namespace Mexc.Net.Clients.FuturesApi
{
    internal partial class MexcSocketClientFuturesApi : IMexcSocketClientFuturesApiShared
    {
        private const string _topicId = "MexcFutures";

        public string Exchange => MexcExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.PerpetualLinear };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
    }
}
