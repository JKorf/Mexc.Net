using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Order side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("v")]
        [JsonConverter(typeof(BigDecimalConverter))]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade time
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
    }
}
