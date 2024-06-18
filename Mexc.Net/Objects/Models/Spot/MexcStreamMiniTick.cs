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
    /// Mini ticker
    /// </summary>
    public record MexcStreamMiniTick : MexcStreamEvent
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last price
        /// </summary>
        [JsonProperty("p")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// 24h Change percentage in specified timezone 
        /// </summary>
        [JsonProperty("tr")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// 24h Change percentage in Utc 8 timezone
        /// </summary>
        [JsonProperty("r")]
        public decimal PriceChangePercentageUtc8 { get; set; }
        /// <summary>
        /// 24h high price
        /// </summary>
        [JsonProperty("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// 24h low price
        /// </summary>
        [JsonProperty("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("t")]
        public DateTime Timestamp { get; set; }

        // NOTE: These volumes seem to be reversed
        /// <summary>
        /// 24h volume
        /// </summary>
        [JsonProperty("q")]
        public decimal Volume { get; set; }
        /// <summary>
        /// 24h quote volume
        /// </summary>
        [JsonProperty("v")]
        public decimal QuoteVolume { get; set; }
    }
}
