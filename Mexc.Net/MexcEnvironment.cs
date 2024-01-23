using CryptoExchange.Net.Objects;
using Mexc.Net.Objects;
using System;

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

        internal MexcEnvironment(string name, string spotRestAddress) : base(name)
        {
            SpotRestAddress = spotRestAddress;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static MexcEnvironment Live { get; }
            = new MexcEnvironment(TradeEnvironmentNames.Live,
                                     MexcApiAddresses.Default.SpotRestAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spotRestAddress"></param>
        /// <returns></returns>
        public static MexcEnvironment CreateCustom(
                        string name,
                        string spotRestAddress)
            => new MexcEnvironment(name, spotRestAddress);
    }
}
