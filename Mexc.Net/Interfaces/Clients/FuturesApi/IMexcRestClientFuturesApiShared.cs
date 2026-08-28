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
        IFeeRestClient,
        IFuturesOrderRestClient,
        IFuturesTriggerOrderRestClient,
        IFuturesOrderClientIdRestClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IMexcRestClientFuturesSharedApi :
        IGetKlinesEndpoint,
        IGetOrderBookEndpoint,
        IGetRecentTradesEndpoint,
        IGetFundingRateHistoryEndpoint,
        IGetFuturesSymbolsEndpoint,
        IGetFuturesTickerEndpoint,
        IGetAllFuturesTickersEndpoint,
        IGetBalancesEndpoint,
        IGetLeverageEndpoint,
        ISetLeverageEndpoint,
        IGetPositionModeEndpoint,
        ISetPositionModeEndpoint,
        IGetPositionHistoryEndpoint,
        IGetFeesEndpoint,
        IPlaceFuturesOrderEndpoint,
        IGetFuturesOrderEndpoint,
        IGetOpenFuturesOrdersEndpoint,
        IGetClosedFuturesOrdersEndpoint,
        IGetFuturesOrderTradesEndpoint,
        IGetFuturesUserTradeHistoryEndpoint,
        ICancelFuturesOrderEndpoint,
        IGetPositionsEndpoint,
        IClosePositionEndpoint,
        IPlaceFuturesTriggerOrderEndpoint,
        IGetFuturesTriggerOrderEndpoint,
        ICancelFuturesTriggerOrderEndpoint,
        IGetFuturesOrderByClientOrderIdEndpoint,
        ICancelFuturesOrderByClientOrderIdEndpoint
    {
    }
}
