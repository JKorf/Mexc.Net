using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Interfaces.Clients.SpotApi;

namespace Mexc.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Mexc Rest API. 
    /// </summary>
    public interface IMexcRestClient : IRestClient<MexcCredentials>
    {
        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IMexcRestClientSpotApi"/>
        IMexcRestClientSpotApi SpotApi { get; }

        /// <summary>
        /// Futures API endpoints
        /// </summary>
        /// <see cref="IMexcRestClientFuturesApi"/>
        IMexcRestClientFuturesApi FuturesApi { get; }
    }
}
