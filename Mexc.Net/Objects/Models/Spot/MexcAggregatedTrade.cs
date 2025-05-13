using CryptoExchange.Net.Converters.SystemTextJson;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Aggregated trade info
    /// </summary>
    [SerializationModel]
    public record MexcAggregatedTrade
    {
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Trade quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Buyer was maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsBuyerMaker { get; set; }
        /// <summary>
        /// Best price match
        /// </summary>
        [JsonPropertyName("M")]
        public bool IsBestMatch { get; set; }
    }
}
