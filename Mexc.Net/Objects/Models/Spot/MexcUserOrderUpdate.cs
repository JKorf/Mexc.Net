using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order update
    /// </summary>
    [SerializationModel]
    public record MexcUserOrderUpdate
    {
        /// <summary>
        /// ["<c>A</c>"] Quote quantity remaining
        /// </summary>
        [JsonPropertyName("A")]
        public decimal QuoteQuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>O</c>"] Creation time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>S</c>"] Order side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>V</c>"] Quantity remaining
        /// </summary>
        [JsonPropertyName("V")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("a")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Client order id
        /// </summary>
        [JsonPropertyName("c")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>i</c>"] Order id
        /// </summary>
        [JsonPropertyName("i")]
        public string? OrderId { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Is maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Order type
        /// </summary>
        [JsonPropertyName("o")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Order status
        /// </summary>
        [JsonPropertyName("s")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Quantity
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>ap</c>"] Average execution price
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>cv</c>"] Cumulative quantity
        /// </summary>
        [JsonPropertyName("cv")]
        public decimal? CumulativeQuantity { get; set; }
        /// <summary>
        /// ["<c>ca</c>"] Cumulative quote quantity
        /// </summary>
        [JsonPropertyName("ca")]
        public decimal? CumulativeQuoteQuantity { get; set; }
    }
}
