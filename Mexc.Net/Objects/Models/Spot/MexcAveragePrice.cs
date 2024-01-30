using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Average price info
    /// </summary>
    public class MexcAveragePrice
    {
        /// <summary>
        /// Minutes the average is over
        /// </summary>
        [JsonProperty("mins")]
        public int Minutes { get; set; }

        /// <summary>
        /// Average price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
