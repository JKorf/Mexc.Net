using CryptoExchange.Net.Converters;
using Mexc.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Ticker
    /// </summary>
    public record MexcFuturesTicker
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("bid1")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("ask1")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Volume in last 24h
        /// </summary>
        [JsonPropertyName("volume24")]
        public decimal Volume24h { get; set; }
        /// <summary>
        /// Volume in quote asset last 24h
        /// </summary>
        [JsonPropertyName("amount24")]
        public decimal QuoteVolume24h { get; set; }
        /// <summary>
        /// Open interest
        /// </summary>
        [JsonPropertyName("holdVol")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// Lowest price last 24h
        /// </summary>
        [JsonPropertyName("lower24Price")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Highest price last 24h
        /// </summary>
        [JsonPropertyName("high24Price")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Change percentage
        /// </summary>
        [JsonPropertyName("riseFallRate")]
        public decimal ChangePercentage { get; set; }
        /// <summary>
        /// Change value
        /// </summary>
        [JsonPropertyName("riseFallValue")]
        public decimal ChangeValue { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("fairPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Max bid price
        /// </summary>
        [JsonPropertyName("maxBidPrice")]
        public decimal MaxBidPrice { get; set; }
        /// <summary>
        /// Min ask price
        /// </summary>
        [JsonPropertyName("minAskPrice")]
        public decimal MinAskPrice { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }


}
