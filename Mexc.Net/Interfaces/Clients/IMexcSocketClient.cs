using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Interfaces.Clients.SpotApi;

namespace Mexc.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Mexc websocket API
    /// </summary>
    public interface IMexcSocketClient : ISocketClient<MexcCredentials>
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
    }
}
