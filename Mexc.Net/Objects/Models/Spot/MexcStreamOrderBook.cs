using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public MexcStreamOrderBookEntry[] Asks { get; set; } = Array.Empty<MexcStreamOrderBookEntry>();
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public MexcStreamOrderBookEntry[] Bids { get; set; } = Array.Empty<MexcStreamOrderBookEntry>();
        /// <summary>
        /// Sequence. If start/end sequence numbers are available this is the start sequence number
        /// </summary>
        [JsonPropertyName("r")]
        public string Sequence { get; set; } = string.Empty;
        /// <summary>
        /// End sequence number
        /// </summary>
        public string? SequenceEnd { get; set; } = string.Empty;
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [SerializationModel]
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
