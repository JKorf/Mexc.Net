using CryptoExchange.Net.Objects.Options;
using Mexc.Net.Interfaces.Clients.FuturesApi;
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
        /// <see cref="IMexcSocketClientSpotApi"/>
        IMexcSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Futures streams and requests
        /// </summary>
        /// <see cref="IMexcSocketClientFuturesApi"/>
        IMexcSocketClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
