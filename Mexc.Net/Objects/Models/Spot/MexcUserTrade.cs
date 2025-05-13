using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// User trade info
    /// </summary>
    [SerializationModel]
    public record MexcUserTrade
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonPropertyName("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("commissionAsset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Is buyer
        /// </summary>
        [JsonPropertyName("isBuyer")]
        public bool IsBuyer { get; set; }
        /// <summary>
        /// Is maker
        /// </summary>
        [JsonPropertyName("isMaker")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// Is best match
        /// </summary>
        [JsonPropertyName("isBestMatch")]
        public bool IsBestMatch { get; set; }
        /// <summary>
        /// Is self trade
        /// </summary>
        [JsonPropertyName("isSelfTrade")]
        public bool IsSelfTrade { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
    }
}
