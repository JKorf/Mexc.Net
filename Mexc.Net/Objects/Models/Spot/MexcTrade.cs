using CryptoExchange.Net.Converters.SystemTextJson;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record MexcTrade
    {
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Traded quantity
        /// </summary>
        [JsonPropertyName("qty"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade value
        /// </summary>
        [JsonPropertyName("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Buyer was maker
        /// </summary>
        [JsonPropertyName("isBuyerMaker")]
        public bool IsBuyerMaker { get; set; }
        /// <summary>
        /// Best price match
        /// </summary>
        [JsonPropertyName("isBestMatch")]
        public bool IsBestMatch { get; set; }
    }
}
