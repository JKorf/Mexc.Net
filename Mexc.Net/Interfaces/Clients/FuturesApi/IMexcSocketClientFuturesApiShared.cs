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
        IPositionSocketClient,
        IUserTradeSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IMexcSocketClientFuturesSharedApi :
        ISubscribeKlinesSocket,
        ISubscribeOrderBookSocket,
        ISubscribeTickerSocket,
        ISubscribeAllTickersSocket,
        ISubscribeTradesSocket,
        ISubscribeBalancesSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribePositionsSocket,
        ISubscribeUserTradesSocket
    {
    }
}
