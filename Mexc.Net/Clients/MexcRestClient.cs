using CryptoExchange.Net.Clients;
using Mexc.Net.Clients.SpotApi;
using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace Mexc.Net.Clients
{
    /// <inheritdoc />
    public class MexcRestClient : BaseRestClient, IMexcRestClient
    {
        /// <inheritdoc />
        public IMexcRestClientSpotApi SpotApi { get; }

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the MexcRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public MexcRestClient(Action<MexcRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the MexcRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public MexcRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<MexcRestOptions> options) : base(loggerFactory, "Mexc")
        {
            Initialize(options.Value);

            SpotApi = AddApiClient(new MexcRestClientSpotApi(_logger, httpClient, options.Value));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<MexcRestOptions> optionsDelegate)
        {
            MexcRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials apiCredentials)
        {
            SpotApi.SetApiCredentials(apiCredentials);
        }
    }
}
