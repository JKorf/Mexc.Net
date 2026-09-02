using CryptoExchange.Net.SharedApis;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot socket API usage
    /// </summary>
    public interface IMexcSocketClientSpotApiShared :
        ITradeSocketClient,
        ITickerSocketClient,
        ITickersSocketClient,
        IBookTickerSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        IBalanceSocketClient,
        ISpotOrderSocketClient,
        IUserTradeSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IMexcSocketClientSpotSharedApi :
        ISubscribeTradesSocket,
        ISubscribeTickerSocket,
        ISubscribeAllTickersSocket,
        ISubscribeBookTickerSocket,
        ISubscribeKlinesSocket,
        ISubscribeOrderBookSocket,
        ISubscribeBalancesSocket,
        ISubscribeSpotOrdersSocket,
        ISubscribeUserTradesSocket
    {
    }
}
