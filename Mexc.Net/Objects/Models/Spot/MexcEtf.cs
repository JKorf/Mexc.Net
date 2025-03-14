using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Etf info
    /// </summary>
    [SerializationModel]
    public record MexcEtf
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Net value
        /// </summary>
        [JsonPropertyName("netValue")]
        public decimal NetValue { get; set; }
        /// <summary>
        /// Fee rate
        /// </summary>
        [JsonPropertyName("feeRate")]
        public decimal FeeRate { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Target leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Real leverage
        /// </summary>
        [JsonPropertyName("realLeverage")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// Times merged
        /// </summary>
        [JsonPropertyName("mergedTimes")]
        public int MergedTimes { get; set; }
        /// <summary>
        /// Last merge time
        /// </summary>
        [JsonPropertyName("lastMergedTime")]
        public DateTime LastMergeTime { get; set; }
        /// <summary>
        /// Basket
        /// </summary>
        [JsonPropertyName("basket")]
        public decimal Basket { get; set; }
    }
}
