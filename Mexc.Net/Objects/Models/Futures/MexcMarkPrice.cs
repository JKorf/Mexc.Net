namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Mark price
    /// </summary>
    public record MexcMarkPrice
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("fairPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
