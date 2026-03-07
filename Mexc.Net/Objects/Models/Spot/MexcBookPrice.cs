namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Best book offers info
    /// </summary>
    [SerializationModel]
    public record MexcBookPrice
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bidPrice</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bidPrice")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bidQty</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("bidQty"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>askPrice</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("askPrice")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>askQty</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("askQty"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal? BestAskQuantity { get; set; }
    }
}
