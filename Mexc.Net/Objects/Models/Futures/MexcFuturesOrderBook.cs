using CryptoExchange.Net.Converters;
using Mexc.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Order book
    /// </summary>
    public record MexcFuturesOrderBook
    {
        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public MexcOrderBookFuturesEntry[] Asks { get; set; } = [];
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public MexcOrderBookFuturesEntry[] Bids { get; set; } = [];
        /// <summary>
        /// Version
        /// </summary>
        [JsonPropertyName("version")]
        public long Version { get; set; }
        /// <summary>
        /// Last update id in the update
        /// </summary>
        [JsonPropertyName("end")]
        public long? SequenceEnd { get; set; }
        /// <summary>
        /// First update id in the update
        /// </summary>
        [JsonPropertyName("begin")]
        public long? SequenceStart { get; set; }
        /// <summary>
        /// Timestamp
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
