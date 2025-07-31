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
        /// <summary>
        /// Futures Rest API address
        /// </summary>
        public string FuturesRestAddress { get; }
        /// <summary>
        /// Futures Socket API address
        /// </summary>
        public string FuturesSocketAddress { get; }

        internal MexcEnvironment(string name, string spotRestAddress, string spotSocketAddress, string futuresRestAddress, string futuresSocketAddress) : base(name)
        {
            SpotRestAddress = spotRestAddress;
            SpotSocketAddress = spotSocketAddress;
            FuturesRestAddress = futuresRestAddress;
            FuturesSocketAddress = futuresSocketAddress;
        }

        /// <summary>
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public MexcEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the Mexc environment by name
        /// </summary>
        public static MexcEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             "" => Live,
             null => Live,
             _ => default
         };

        /// <summary>
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static MexcEnvironment Live { get; }
            = new MexcEnvironment(TradeEnvironmentNames.Live,
                                     MexcApiAddresses.Default.SpotRestAddress,
                                     MexcApiAddresses.Default.SpotSocketAddress,
                                     MexcApiAddresses.Default.FuturesRestAddress,
                                     MexcApiAddresses.Default.FuturesSocketAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        public static MexcEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketAddress,
                        string futuresRestAddress,
                        string futuresSocketAddress)
            => new MexcEnvironment(name, spotRestAddress, spotSocketAddress, futuresRestAddress, futuresSocketAddress);
    }
}
