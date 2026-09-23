using CryptoExchange.Net.SharedApis;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Interfaces.Clients.SpotApi;

namespace Mexc.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Mexc
    /// </summary>
    public interface IMexcSharedApiClient : ISharedApiClientBase
    {
        /// <summary>
        /// Spot REST shared API implementations
        /// </summary>
        IMexcRestClientSpotSharedApi SpotRest { get; }

        /// <summary>
        /// Futures REST shared API implementations
        /// </summary>
        IMexcRestClientFuturesSharedApi FuturesRest { get; }

        /// <summary>
        /// Spot WebSocket shared API implementations
        /// </summary>
        IMexcSocketClientSpotSharedApi SpotSocket { get; }

        /// <summary>
        /// Futures WebSocket shared API implementations
        /// </summary>
        IMexcSocketClientFuturesSharedApi FuturesSocket { get; }
    }
}
