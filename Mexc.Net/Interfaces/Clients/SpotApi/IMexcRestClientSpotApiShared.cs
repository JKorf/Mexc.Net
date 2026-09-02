using CryptoExchange.Net.SharedApis;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot rest API usage
    /// </summary>
    public interface IMexcRestClientSpotApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IDepositRestClient,
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        ISpotOrderRestClient,
        ISpotSymbolRestClient,
        ISpotTickerRestClient,
        //ITradeHistoryRestClient
        IWithdrawalRestClient,
        IWithdrawRestClient,
        IFeeRestClient,
        IBookTickerRestClient,
        ITransferRestClient,
        ISpotOrderClientIdRestClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IMexcRestClientSpotSharedApi :
        IGetAssetRest,
        IGetAllAssetsRest,
        IGetBalancesRest,
        IGetDepositAddressesRest,
        IGetDepositHistoryRest,
        IGetKlinesRest,
        IGetRecentTradesRest,
        IGetOrderBookRest,
        IPlaceSpotOrderRest,
        IGetSpotOrderRest,
        IGetOpenSpotOrdersRest,
        IGetClosedSpotOrdersRest,
        IGetSpotOrderTradesRest,
        ICancelSpotOrderRest,
        IGetSpotUserTradeHistoryRest,
        IGetSpotSymbolsRest,
        IGetSpotTickerRest,
        IGetAllSpotTickersRest,
        IGetWithdrawalHistoryRest,
        IWithdrawRest,
        IGetFeesRest,
        IGetBookTickerRest,
        ITransferRest,
        IGetSpotOrderByClientOrderIdRest,
        ICancelSpotOrderByClientOrderIdRest
    {
    }
}
