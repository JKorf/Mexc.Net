using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Eligible dust asset
    /// </summary>
    public class MexcEligibleDust
    {
        /// <summary>
        /// Resulting Mx
        /// </summary>
        [JsonProperty("convertMx")]
        public decimal ConvertMx { get; set; }
        /// <summary>
        /// Dust worth
        /// </summary>
        [JsonProperty("convertUsdt")]
        public decimal ConvertUsdt { get; set; }
        /// <summary>
        /// Current balance
        /// </summary>
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}
