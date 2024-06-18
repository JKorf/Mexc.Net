using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Account update info
    /// </summary>
    public record MexcAccountUpdate
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("c")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// New free quantity
        /// </summary>
        [JsonProperty("f")]
        public decimal Free { get; set; }
        /// <summary>
        /// Changed free quantity
        /// </summary>
        [JsonProperty("fd")]
        public decimal FreeChange { get; set; }
        /// <summary>
        /// New frozen quantity
        /// </summary>
        [JsonProperty("l")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// Changed frozen quantity
        /// </summary>
        [JsonProperty("ld")]
        public decimal FrozenChange { get; set; }
        /// <summary>
        /// Trigger update type
        /// </summary>
        [JsonProperty("o")]
        public string UpdateType { get; set; } = string.Empty;
    }
}
