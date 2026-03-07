using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// User trade
    /// </summary>
    public record MexcFuturesUserTrade
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public FuturesOrderSide Side { get; set; }
        /// <summary>
        /// ["<c>vol</c>"] Quantity
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>profit</c>"] Realized profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }
        /// <summary>
        /// ["<c>category</c>"] Category
        /// </summary>
        [JsonPropertyName("category")]
        public OrderCategory Category { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>taker</c>"] Taker
        /// </summary>
        [JsonPropertyName("taker")]
        public bool Taker { get; set; }
    }


}
