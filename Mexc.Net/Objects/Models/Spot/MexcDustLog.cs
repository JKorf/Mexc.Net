using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Dust log entry
    /// </summary>
    public class MexcDustLog
    {
        /// <summary>
        /// Total converted
        /// </summary>
        [JsonProperty("totalConvert")]
        public decimal TotalConverted { get; set; }
        /// <summary>
        /// Total fee
        /// </summary>
        [JsonProperty("totalFee")]
        public decimal TotalFee { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("convertTime")]
        public DateTime ConvertTime { get; set; }
        /// <summary>
        /// Details
        /// </summary>
        [JsonProperty("convertDetails")]
        public IEnumerable<MexcDustLogDetails> Details { get; set; } = Array.Empty<MexcDustLogDetails>();
    }
    
    /// <summary>
    /// Dust log details
    /// </summary>
    public class MexcDustLogDetails
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Converted
        /// </summary>
        [JsonProperty("convert")]
        public decimal Converted { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
    }
}
