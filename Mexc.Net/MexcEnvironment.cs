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
