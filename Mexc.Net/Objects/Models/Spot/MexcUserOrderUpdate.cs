using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Quote quantity remaining
        /// </summary>
        [JsonPropertyName("A")]
        public decimal QuoteQuantityRemaining { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Quantity remaining
        /// </summary>
        [JsonPropertyName("V")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonPropertyName("a")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("c")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("i")]
        public string? OrderId { get; set; }
        /// <summary>
        /// Is maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("o")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("s")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Average execution price
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Cumulative quantity
        /// </summary>
        [JsonPropertyName("cv")]
        public decimal? CumulativeQuantity { get; set; }
        /// <summary>
        /// Cumulative quote quantity
        /// </summary>
        [JsonPropertyName("ca")]
        public decimal? CumulativeQuoteQuantity { get; set; }
    }
}
