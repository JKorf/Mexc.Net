using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Best book offers info
    /// </summary>
    [SerializationModel]
    public record MexcBookPrice
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("bidPrice")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonPropertyName("bidQty"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("askPrice")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonPropertyName("askQty"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal? BestAskQuantity { get; set; }
    }
}
