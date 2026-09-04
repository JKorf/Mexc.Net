using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Interfaces.Clients.SpotApi;

namespace Mexc.Net.Clients
{
    /// <inheritdoc />
    public class MexcSharedApiClient : IMexcSharedApiClient
    {
        /// <inheritdoc />
        public IMexcRestClientSpotSharedApi SpotRest { get; }
        /// <inheritdoc />
        public IMexcRestClientFuturesSharedApi FuturesRest { get; }
        /// <inheritdoc />
        public IMexcSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IMexcSocketClientFuturesSharedApi FuturesSocket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public MexcSharedApiClient(
            IMexcRestClient restClient,
            IMexcSocketClient socketClient)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.FuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.FuturesApi.SharedApi;
        }
    }
}
