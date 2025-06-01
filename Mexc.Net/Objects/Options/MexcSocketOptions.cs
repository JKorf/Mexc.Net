using CryptoExchange.Net.Objects.Options;

namespace Mexc.Net.Objects.Options
{
    /// <summary>
    /// Mexc Socket client options
    /// </summary>
    public class MexcSocketOptions : SocketExchangeOptions<MexcEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static MexcSocketOptions Default { get; set; } = new MexcSocketOptions()
        {
            Environment = MexcEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public MexcSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Options for the Spot API
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions();
        /// <summary>
        /// Options for the Futures API
        /// </summary>
        public SocketApiOptions FuturesOptions { get; private set; } = new SocketApiOptions();


        internal MexcSocketOptions Set(MexcSocketOptions targetOptions)
        {
            targetOptions = base.Set<MexcSocketOptions>(targetOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            return targetOptions;
        }
    }
}