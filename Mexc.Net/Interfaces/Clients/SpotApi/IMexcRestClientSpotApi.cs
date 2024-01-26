using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    public interface IMexcRestClientSpotApi: IRestApiClient
    {
        IMexcRestClientSpotApiAccount Account { get; }
        IMexcRestClientSpotApiExchangeData ExchangeData { get; }
        IMexcRestClientSpotApiTrading Trading { get; }
    }
}
