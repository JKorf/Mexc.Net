using Newtonsoft.Json;
using Mexc.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Interfaces;
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
        [JsonProperty("asks")]
        public IEnumerable<MexcStreamOrderBookEntry> Asks { get; set; } = Array.Empty<MexcStreamOrderBookEntry>();
        /// <summary>
        /// Bids
        /// </summary>
        [JsonProperty("bids")]
        public IEnumerable<MexcStreamOrderBookEntry> Bids { get; set; } = Array.Empty<MexcStreamOrderBookEntry>();
        /// <summary>
        /// Sequence
        /// </summary>
        [JsonProperty("r")]
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
        [JsonProperty("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("v")]
        public decimal Quantity { get; set; }
    }
}
