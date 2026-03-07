namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Dust log entry
    /// </summary>
    [SerializationModel]
    public record MexcDustLog
    {
        /// <summary>
        /// ["<c>totalConvert</c>"] Total converted
        /// </summary>
        [JsonPropertyName("totalConvert")]
        public decimal TotalConverted { get; set; }
        /// <summary>
        /// ["<c>totalFee</c>"] Total fee
        /// </summary>
        [JsonPropertyName("totalFee")]
        public decimal TotalFee { get; set; }
        /// <summary>
        /// ["<c>convertTime</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("convertTime")]
        public DateTime ConvertTime { get; set; }
        /// <summary>
        /// ["<c>convertDetails</c>"] Details
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>convert</c>"] Converted
        /// </summary>
        [JsonPropertyName("convert")]
        public decimal Converted { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
    }
}
