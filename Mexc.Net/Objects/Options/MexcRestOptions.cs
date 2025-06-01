using CryptoExchange.Net.Objects.Options;

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
        internal static MexcRestOptions Default { get; set; } = new MexcRestOptions()
        {
            Environment = MexcEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// ctor
        /// </summary>
        public MexcRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Broker id
        /// </summary>
        public string? BrokerId { get; set; }

        /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new RestApiOptions();

        /// <summary>
        /// Futures API options
        /// </summary>
        public RestApiOptions FuturesOptions { get; private set; } = new RestApiOptions();

        internal MexcRestOptions Set(MexcRestOptions targetOptions)
        {
            targetOptions = base.Set<MexcRestOptions>(targetOptions);
            targetOptions.ReceiveWindow = ReceiveWindow;
            targetOptions.BrokerId = BrokerId;
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            return targetOptions;
        }
    }
}
