using CryptoExchange.Net.Objects.Options;
using System;

namespace Mexc.Net.Objects.Options
{
    /// <summary>
    /// Options for the MexcRestClient
    /// </summary>
    public class MexcRestOptions : RestExchangeOptions<MexcEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static MexcRestOptions Default { get; set; } = new MexcRestOptions()
        {
            Environment = MexcEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new RestApiOptions();

        internal MexcRestOptions Copy()
        {
            var options = Copy<MexcRestOptions>();
            options.ReceiveWindow = ReceiveWindow;
            options.SpotOptions = SpotOptions.Copy<RestApiOptions>();
            return options;
        }
    }
}
