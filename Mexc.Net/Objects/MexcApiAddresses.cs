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
        /// The address used by the MexcClient for the Futures Rest API
        /// </summary>
        public string FuturesRestAddress { get; set; } = "";
        /// <summary>
        /// The address used by the MexcClient for the Futures Socket API
        /// </summary>
        public string FuturesSocketAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the mexc.com API
        /// </summary>
        public static MexcApiAddresses Default = new MexcApiAddresses
        {
            SpotRestAddress = "https://api.mexc.com",
            SpotSocketAddress = "ws://wbs-api.mexc.com/ws",
            FuturesRestAddress = "https://contract.mexc.com",
            FuturesSocketAddress = "wss://contract.mexc.com/edge"
        };
    }
}
