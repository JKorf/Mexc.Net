namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Aggregated trade info
    /// </summary>
    [SerializationModel]
    public record MexcAggregatedTrade
    {
        /// <summary>
        /// ["<c>p</c>"] Trade price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Trade quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>T</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Buyer was maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsBuyerMaker { get; set; }
        /// <summary>
        /// ["<c>M</c>"] Best price match
        /// </summary>
        [JsonPropertyName("M")]
        public bool IsBestMatch { get; set; }
    }
}
