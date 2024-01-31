using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using Mexc.Net.Clients.SpotApi;
using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Options;
using Microsoft.Extensions.Logging;
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
        /// <param name="loggerFactory">The logger factory</param>
        public MexcSocketClient(ILoggerFactory? loggerFactory = null) : this((x) => { }, loggerFactory)
        {
        }

        /// <summary>
        /// Create a new instance of MexcSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public MexcSocketClient(Action<MexcSocketOptions> optionsDelegate) : this(optionsDelegate, null)
        {
        }

        /// <summary>
        /// Create a new instance of MexcSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public MexcSocketClient(Action<MexcSocketOptions> optionsDelegate, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Mexc")
        {
            var options = MexcSocketOptions.Default.Copy();
            optionsDelegate(options);
            Initialize(options);

            SpotApi = AddApiClient(new MexcSocketClientSpotApi(_logger, options));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<MexcSocketOptions> optionsDelegate)
        {
            var options = MexcSocketOptions.Default.Copy();
            optionsDelegate(options);
            MexcSocketOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
        }
    }
}
