using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order book
    /// </summary>
    public record MexcStreamOrderBook : MexcStreamEvent
    {
        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public IEnumerable<MexcStreamOrderBookEntry> Asks { get; set; } = Array.Empty<MexcStreamOrderBookEntry>();
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public IEnumerable<MexcStreamOrderBookEntry> Bids { get; set; } = Array.Empty<MexcStreamOrderBookEntry>();
        /// <summary>
        /// Sequence
        /// </summary>
        [JsonPropertyName("r")]
        public string Sequence { get; set; } = string.Empty;
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    public record MexcStreamOrderBookEntry : ISymbolOrderBookEntry
    {
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
    }
}
