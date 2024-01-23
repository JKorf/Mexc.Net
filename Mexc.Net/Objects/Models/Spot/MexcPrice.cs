using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Price info
    /// </summary>
    public class MexcPrice
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
