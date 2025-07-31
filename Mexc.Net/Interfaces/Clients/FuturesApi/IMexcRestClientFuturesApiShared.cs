using CryptoExchange.Net.SharedApis;

namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures rest API usage
    /// </summary>
    public interface IMexcRestClientFuturesApiShared :
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        IFundingRateRestClient,
        IFuturesSymbolRestClient,
        IFuturesTickerRestClient,
        IBalanceRestClient,
        ILeverageRestClient,
        IPositionModeRestClient,
        IPositionHistoryRestClient,
        IFeeRestClient
    {
    }
}
