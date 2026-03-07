using CryptoExchange.Net.Converters;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Order book
    /// </summary>
    public record MexcFuturesOrderBook
    {
        /// <summary>
        /// ["<c>asks</c>"] Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public MexcOrderBookFuturesEntry[] Asks { get; set; } = [];
        /// <summary>
        /// ["<c>bids</c>"] Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public MexcOrderBookFuturesEntry[] Bids { get; set; } = [];
        /// <summary>
        /// ["<c>version</c>"] Version
        /// </summary>
        [JsonPropertyName("version")]
        public long Version { get; set; }
        /// <summary>
        /// ["<c>end</c>"] Last update id in the update
        /// </summary>
        [JsonPropertyName("end")]
        public long? SequenceEnd { get; set; }
        /// <summary>
        /// ["<c>begin</c>"] First update id in the update
        /// </summary>
        [JsonPropertyName("begin")]
        public long? SequenceStart { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Futures book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<MexcOrderBookFuturesEntry>))]
    public record MexcOrderBookFuturesEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// The price of this order book entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of this price in the order book
        /// </summary>
        [ArrayProperty(1), JsonConverter(typeof(BigDecimalConverter))]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The number of orders
        /// </summary>
        [ArrayProperty(2)]
        public int Count { get; set; }
    }
}
