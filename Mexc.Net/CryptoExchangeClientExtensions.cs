using Mexc.Net.Clients;
using Mexc.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.Net.Clients
{
    public static class CryptoExchangeClientExtensions
    {
        public static IMexcRestClient Mexc(this ICryptoExchangeClient baseClient) => baseClient.TryGet<IMexcRestClient>() ?? new MexcRestClient();
    }
}
