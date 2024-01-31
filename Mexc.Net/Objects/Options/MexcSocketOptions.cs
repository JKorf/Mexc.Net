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
        public static MexcSocketOptions Default { get; set; } = new MexcSocketOptions()
        {
            Environment = MexcEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// Options for the Spot API
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions();

        internal MexcSocketOptions Copy()
        {
            var options = Copy<MexcSocketOptions>();
            options.SpotOptions = SpotOptions.Copy<SocketApiOptions>();
            return options;
        }
    }
}