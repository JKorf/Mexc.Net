namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Etf info
    /// </summary>
    [SerializationModel]
    public record MexcEtf
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>netValue</c>"] Net value
        /// </summary>
        [JsonPropertyName("netValue")]
        public decimal NetValue { get; set; }
        /// <summary>
        /// ["<c>feeRate</c>"] Fee rate
        /// </summary>
        [JsonPropertyName("feeRate")]
        public decimal FeeRate { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Target leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>realLeverage</c>"] Real leverage
        /// </summary>
        [JsonPropertyName("realLeverage")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// ["<c>mergedTimes</c>"] Times merged
        /// </summary>
        [JsonPropertyName("mergedTimes")]
        public int MergedTimes { get; set; }
        /// <summary>
        /// ["<c>lastMergedTime</c>"] Last merge time
        /// </summary>
        [JsonPropertyName("lastMergedTime")]
        public DateTime LastMergeTime { get; set; }
        /// <summary>
        /// ["<c>basket</c>"] Basket
        /// </summary>
        [JsonPropertyName("basket")]
        public decimal Basket { get; set; }
    }
}
