using CryptoExchange.Net.SharedApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    public interface IMexcRestClientSpotApiShared :
        IAssetRestClient,
        IBalanceRestClient,
        IDepositRestClient,
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        ISpotOrderRestClient,
        ISpotSymbolRestClient,
        ITickerRestClient,
        //ITradeHistoryRestClient
        IWithdrawalRestClient,
        IWithdrawRestClient
    {
    }
}
