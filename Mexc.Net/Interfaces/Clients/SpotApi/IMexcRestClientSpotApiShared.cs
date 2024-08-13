using CryptoExchange.Net.SharedApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    public interface IMexcRestClientSpotApiShared :
        ITickerRestClient,
        ISpotSymbolRestClient,
        IKlineRestClient,
        ITradeRestClient,
        IBalanceRestClient
    {
    }
}
