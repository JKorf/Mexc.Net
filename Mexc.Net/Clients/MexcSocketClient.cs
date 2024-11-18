using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Mexc.Net.Clients.SpotApi;
using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Clients
{
    /// <inheritdoc />
    public class MexcSocketClient : BaseSocketClient, IMexcSocketClient
    {
        /// <inheritdoc />
        public IMexcSocketClientSpotApi SpotApi { get; set; }

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of MexcSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public MexcSocketClient(Action<MexcSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of MexcSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public MexcSocketClient(IOptions<MexcSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Mexc")
        {
            Initialize(options.Value);

            SpotApi = AddApiClient(new MexcSocketClientSpotApi(_logger, options.Value));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<MexcSocketOptions> optionsDelegate)
        {
            MexcSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
        }
    }
}
