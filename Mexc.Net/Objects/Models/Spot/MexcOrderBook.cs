using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order book info
    /// </summary>
    public record MexcOrderBook
    {
        /// <summary>
        /// Last update id
        /// </summary>
        [JsonProperty("lastUpdateId")]
        public long LastUpdateId { get; set; }

        /// <summary>
        /// The list of bids
        /// </summary>
        [JsonProperty("bids")]
        public IEnumerable<MexcOrderBookEntry> Bids { get; set; } = Array.Empty<MexcOrderBookEntry>();

        /// <summary>
        /// The list of asks
        /// </summary>
        [JsonProperty("asks")]
        public IEnumerable<MexcOrderBookEntry> Asks { get; set; } = Array.Empty<MexcOrderBookEntry>();
    }
}
