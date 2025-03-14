using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Book ticker
    /// </summary>
    [SerializationModel]
    public record MexcStreamBookTick : MexcStreamEvent
    {
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonPropertyName("A")]
        [JsonConverter(typeof(BigDecimalConverter))]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonPropertyName("B")]
        [JsonConverter(typeof(BigDecimalConverter))]
        public decimal BestBidQuantity { get; set; }
    }
}
