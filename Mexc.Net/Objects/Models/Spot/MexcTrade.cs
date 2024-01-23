using Newtonsoft.Json;
using System;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class MexcTrade
    {
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Traded quantity
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade value
        /// </summary>
        [JsonProperty("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Buyer was maker
        /// </summary>
        [JsonProperty("isBuyerMaker")]
        public bool IsBuyerMaker { get; set; }
        /// <summary>
        /// Best price match
        /// </summary>
        [JsonProperty("isBestMatch")]
        public bool IsBestMatch { get; set; }
    }
}
