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
    /// Book ticker
    /// </summary>
    public record MexcStreamBookTick : MexcStreamEvent
    {
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonProperty("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonProperty("A")]
        [JsonConverter(typeof(BigDecimalConverter))]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonProperty("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonProperty("B")]
        [JsonConverter(typeof(BigDecimalConverter))]
        public decimal BestBidQuantity { get; set; }
    }
}
