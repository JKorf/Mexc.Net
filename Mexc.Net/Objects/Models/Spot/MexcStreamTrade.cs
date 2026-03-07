using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record MexcStreamTrade
    {
        /// <summary>
        /// ["<c>S</c>"] Order side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Quantity
        /// </summary>
        [JsonPropertyName("v")]
        [JsonConverter(typeof(BigDecimalConverter))]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Trade time
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
    }
}
