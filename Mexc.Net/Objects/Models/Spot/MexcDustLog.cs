using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Dust log entry
    /// </summary>
    [SerializationModel]
    public record MexcDustLog
    {
        /// <summary>
        /// Total converted
        /// </summary>
        [JsonPropertyName("totalConvert")]
        public decimal TotalConverted { get; set; }
        /// <summary>
        /// Total fee
        /// </summary>
        [JsonPropertyName("totalFee")]
        public decimal TotalFee { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("convertTime")]
        public DateTime ConvertTime { get; set; }
        /// <summary>
        /// Details
        /// </summary>
        [JsonPropertyName("convertDetails")]
        public MexcDustLogDetails[] Details { get; set; } = Array.Empty<MexcDustLogDetails>();
    }
    
    /// <summary>
    /// Dust log details
    /// </summary>
    [SerializationModel]
    public record MexcDustLogDetails
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Converted
        /// </summary>
        [JsonPropertyName("convert")]
        public decimal Converted { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
    }
}
