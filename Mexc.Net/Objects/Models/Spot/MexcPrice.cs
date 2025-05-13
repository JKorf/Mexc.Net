using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Price info
    /// </summary>
    [SerializationModel]
    public record MexcPrice
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
