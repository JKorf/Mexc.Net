namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record MexcTrade
    {
        /// <summary>
        /// ["<c>price</c>"] Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] Traded quantity
        /// </summary>
        [JsonPropertyName("qty"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>quoteQty</c>"] Trade value
        /// </summary>
        [JsonPropertyName("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>isBuyerMaker</c>"] Buyer was maker
        /// </summary>
        [JsonPropertyName("isBuyerMaker")]
        public bool IsBuyerMaker { get; set; }
        /// <summary>
        /// ["<c>isBestMatch</c>"] Best price match
        /// </summary>
        [JsonPropertyName("isBestMatch")]
        public bool IsBestMatch { get; set; }
    }
}
