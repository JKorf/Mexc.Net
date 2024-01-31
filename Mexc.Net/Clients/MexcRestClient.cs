using CryptoExchange.Net;
using Mexc.Net.Clients.SpotApi;
using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Options;
using Microsoft.Extensions.Logging;
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
        public MexcRestClient(Action<MexcRestOptions> optionsDelegate) : this(null, null, optionsDelegate)
        {
        }

        /// <summary>
        /// Create a new instance of the MexcRestClient using provided options
        /// </summary>
        public MexcRestClient(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) : this(httpClient, loggerFactory, null)
        {
        }

        /// <summary>
        /// Create a new instance of the MexcRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public MexcRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, Action<MexcRestOptions>? optionsDelegate = null) : base(loggerFactory, "Mexc")
        {
            var options = MexcRestOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            SpotApi = AddApiClient(new MexcRestClientSpotApi(_logger, httpClient, options));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<MexcRestOptions> optionsDelegate)
        {
            var options = MexcRestOptions.Default.Copy();
            optionsDelegate(options);
            MexcRestOptions.Default = options;
        }
    }
}
