namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// User trade info
    /// </summary>
    [SerializationModel]
    public record MexcUserTrade
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] Quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>quoteQty</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>commission</c>"] Fee
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>commissionAsset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("commissionAsset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>isBuyer</c>"] Is buyer
        /// </summary>
        [JsonPropertyName("isBuyer")]
        public bool IsBuyer { get; set; }
        /// <summary>
        /// ["<c>isMaker</c>"] Is maker
        /// </summary>
        [JsonPropertyName("isMaker")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// ["<c>isBestMatch</c>"] Is best match
        /// </summary>
        [JsonPropertyName("isBestMatch")]
        public bool IsBestMatch { get; set; }
        /// <summary>
        /// ["<c>isSelfTrade</c>"] Is self trade
        /// </summary>
        [JsonPropertyName("isSelfTrade")]
        public bool IsSelfTrade { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
    }
}
