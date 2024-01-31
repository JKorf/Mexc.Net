using CryptoExchange.Net.Objects;
using Mexc.Net.Objects;

namespace Mexc.Net
{
    /// <summary>
    /// Mexc environments
    /// </summary>
    public class MexcEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Spot Rest API address
        /// </summary>
        public string SpotRestAddress { get; }
        /// <summary>
        /// Spot Socket API address
        /// </summary>
        public string SpotSocketAddress { get; }

        internal MexcEnvironment(string name, string spotRestAddress, string spotSocketAddress) : base(name)
        {
            SpotRestAddress = spotRestAddress;
            SpotSocketAddress = spotSocketAddress;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static MexcEnvironment Live { get; }
            = new MexcEnvironment(TradeEnvironmentNames.Live,
                                     MexcApiAddresses.Default.SpotRestAddress,
                                     MexcApiAddresses.Default.SpotSocketAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spotRestAddress"></param>
        /// <param name="spotSocketAddress"></param>
        /// <returns></returns>
        public static MexcEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketAddress)
            => new MexcEnvironment(name, spotRestAddress, spotSocketAddress);
    }
}
