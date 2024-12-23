using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Options;
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

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
