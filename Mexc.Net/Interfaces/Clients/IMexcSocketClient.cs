using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using Mexc.Net.Interfaces.Clients.SpotApi;

namespace Mexc.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Mexc websocket API
    /// </summary>
    public interface IMexcSocketClient : ISocketClient
    {
        /// <summary>
        /// Spot streams and requests
        /// </summary>
        IMexcSocketClientSpotApi SpotApi { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
