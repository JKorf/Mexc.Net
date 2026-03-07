using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order book
    /// </summary>
    [SerializationModel]
    public record MexcStreamOrderBook : MexcStreamEvent
    {
        /// <summary>
        /// ["<c>asks</c>"] Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public MexcStreamOrderBookEntry[] Asks { get; set; } = Array.Empty<MexcStreamOrderBookEntry>();
        /// <summary>
        /// ["<c>bids</c>"] Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public MexcStreamOrderBookEntry[] Bids { get; set; } = Array.Empty<MexcStreamOrderBookEntry>();
        /// <summary>
        /// ["<c>r</c>"] Sequence. If start/end sequence numbers are available this is the start sequence number
        /// </summary>
        [JsonPropertyName("r")]
        public long Sequence { get; set; }
        /// <summary>
        /// End sequence number
        /// </summary>
        public long? SequenceEnd { get; set; }
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [SerializationModel]
    public record MexcStreamOrderBookEntry : ISymbolOrderBookEntry
    {
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
    }
}
