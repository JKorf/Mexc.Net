using Newtonsoft.Json;
using Mexc.Net.Enums;
using System;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Kline info
    /// </summary>
    public record MexcStreamKline
    {
        /// <summary>
        /// Start time
        /// </summary>
        [JsonProperty("t")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [JsonProperty("T")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Volume in quote asset
        /// </summary>
        [JsonProperty("a")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonProperty("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// Highest price
        /// </summary>
        [JsonProperty("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Interval
        /// </summary>
        [JsonProperty("i")]
        public KlineInterval Interval { get; set; }
        /// <summary>
        /// Lowest price
        /// </summary>
        [JsonProperty("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonProperty("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonProperty("v")]
        [JsonConverter(typeof(BigDecimalConverter))]
        public decimal Volume { get; set; }
    }
}
