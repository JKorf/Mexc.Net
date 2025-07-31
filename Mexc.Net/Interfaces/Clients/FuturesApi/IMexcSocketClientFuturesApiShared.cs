using CryptoExchange.Net.SharedApis;

namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures socket API usage
    /// </summary>
    public interface IMexcSocketClientFuturesApiShared :
        IKlineSocketClient,
        IOrderBookSocketClient,
        ITickerSocketClient,
        ITickersSocketClient,
        ITradeSocketClient,
        IBalanceSocketClient,
        IFuturesOrderSocketClient,
        IPositionSocketClient
    {
    }
}
