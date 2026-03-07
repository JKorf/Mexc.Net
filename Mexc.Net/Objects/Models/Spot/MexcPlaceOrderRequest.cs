using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Place order request
    /// </summary>
    public record MexcPlaceOrderRequest
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }
        /// <summary>
        /// ["<c>quantity</c>"] Order quantity
        /// </summary>
        [JsonPropertyName("quantity"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>quoteOrderQty</c>"] Quote order quantity
        /// </summary>
        [JsonPropertyName("quoteOrderQty"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Order price
        /// </summary>
        [JsonPropertyName("price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>newClientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("newClientOrderId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ClientOrderId { get; set; }
    }
}
