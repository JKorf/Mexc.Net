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
        /// ["<c>a</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>A</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("A")]
        [JsonConverter(typeof(BigDecimalConverter))]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>b</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>B</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("B")]
        [JsonConverter(typeof(BigDecimalConverter))]
        public decimal BestBidQuantity { get; set; }
    }
}
