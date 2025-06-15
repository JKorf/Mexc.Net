namespace Mexc.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class MexcApiAddresses
    {
        /// <summary>
        /// The address used by the MexcClient for the Spot Rest API
        /// </summary>
        public string SpotRestAddress { get; set; } = "";
        /// <summary>
        /// The address used by the MexcClient for the Spot Socket API
        /// </summary>
        public string SpotSocketAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the mexc.com API
        /// </summary>
        public static MexcApiAddresses Default = new MexcApiAddresses
        {
            SpotRestAddress = "https://api.mexc.com",
            SpotSocketAddress = "ws://wbs-api.mexc.com/ws"
        };
    }
}
