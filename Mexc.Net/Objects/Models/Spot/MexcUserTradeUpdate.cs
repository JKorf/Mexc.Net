using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// User trade update
    /// </summary>
    [SerializationModel]
    public record MexcUserTradeUpdate
    {
        /// <summary>
        /// ["<c>T</c>"] Trade time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// ["<c>S</c>"] Trade side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide TradeSide { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("a")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Client order id
        /// </summary>
        [JsonPropertyName("c")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>i</c>"] Order id
        /// </summary>
        [JsonPropertyName("i")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>t</c>"] Trade id
        /// </summary>
        [JsonPropertyName("t")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>m</c>"] Is maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// ["<c>st</c>"] Is self trade
        /// </summary>
        [JsonPropertyName("st")]
        public bool IsSelfTrade { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Quantity
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>n</c>"] Fee
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>N</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("N")]
        public string FeeAsset { get; set; } = string.Empty;
    }
}
