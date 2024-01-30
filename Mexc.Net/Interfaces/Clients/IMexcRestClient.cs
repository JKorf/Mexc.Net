using CryptoExchange.Net.Interfaces;
using Mexc.Net.Interfaces.Clients.SpotApi;

namespace Mexc.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Mexc Rest API. 
    /// </summary>
    public interface IMexcRestClient : IRestClient
    {
        /// <summary>
        /// Spot API endpoints
        /// </summary>
        IMexcRestClientSpotApi SpotApi { get; }
    }
}
