using CryptoExchange.Net.SharedApis;
using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Options;
using Microsoft.Extensions.Options;

namespace Mexc.Net.Clients
{
    /// <inheritdoc />
    public class MexcSharedApiClient : SharedApiClientBase, IMexcSharedApiClient
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
            IMexcSocketClient socketClient,
            IOptions<MexcOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                  restClient.SpotApi.SharedApi,
                  restClient.FuturesApi.SharedApi,
                  socketClient.SpotApi.SharedApi,
                  socketClient.FuturesApi.SharedApi
                  )
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.FuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.FuturesApi.SharedApi;
        }
    }
}
