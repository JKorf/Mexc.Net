using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Eligible dust asset
    /// </summary>
    [SerializationModel]
    public record MexcEligibleDust
    {
        /// <summary>
        /// Resulting Mx
        /// </summary>
        [JsonPropertyName("convertMx")]
        public decimal ConvertMx { get; set; }
        /// <summary>
        /// Dust worth
        /// </summary>
        [JsonPropertyName("convertUsdt")]
        public decimal ConvertUsdt { get; set; }
        /// <summary>
        /// Current balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
