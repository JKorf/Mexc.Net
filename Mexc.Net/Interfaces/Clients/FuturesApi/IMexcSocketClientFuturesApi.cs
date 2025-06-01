using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Futures API socket subscriptions and requests
    /// </summary>
    public interface IMexcSocketClientFuturesApi : ISocketApiClient
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IMexcSocketClientFuturesApiShared SharedClient { get; }

    }
}
