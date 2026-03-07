namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Mark price
    /// </summary>
    public record MexcMarkPrice
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fairPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("fairPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
