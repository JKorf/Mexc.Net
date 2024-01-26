using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Etf info
    /// </summary>
    public class MexcEtf
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Net value
        /// </summary>
        [JsonProperty("netValue")]
        public decimal NetValue { get; set; }
        /// <summary>
        /// Fee rate
        /// </summary>
        [JsonProperty("feeRate")]
        public decimal FeeRate { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Target leverage
        /// </summary>
        [JsonProperty("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Real leverage
        /// </summary>
        [JsonProperty("realLeverage")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// Times merged
        /// </summary>
        [JsonProperty("mergedTimes")]
        public int MergedTimes { get; set; }
        /// <summary>
        /// Last merge time
        /// </summary>
        [JsonProperty("lastMergedTime")]
        public DateTime LastMergeTime { get; set; }
        /// <summary>
        /// Basket
        /// </summary>
        [JsonProperty("basket")]
        public decimal Basket { get; set; }
    }
}
