using Newtonsoft.Json;
using System;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Ticker info
    /// </summary>
    public record MexcTicker
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Price change
        /// </summary>
        [JsonProperty("priceChange")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// Price change percentage
        /// </summary>
        [JsonProperty("priceChangePercent")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// Previous day close price
        /// </summary>
        [JsonProperty("prevClosePrice")]
        public decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        [JsonProperty("lastPrice")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonProperty("bidPrice")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonProperty("askPrice")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonProperty("openPrice")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonProperty("highPrice")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonProperty("lowPrice")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonProperty("volume"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal Volume { get; set; }
        /// <summary>
        /// Volume in quote asset
        /// </summary>
        [JsonProperty("quoteVolume")]
        public decimal? QuoteVolume { get; set; }
        /// <summary>
        /// Open timestamp
        /// </summary>
        [JsonProperty("openTime")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Close timestamp
        /// </summary>
        [JsonProperty("closeTime")]
        public DateTime CloseTime { get; set; }
    }
}
