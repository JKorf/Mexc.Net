using CryptoExchange.Net.Converters.SystemTextJson;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Ticker info
    /// </summary>
    [SerializationModel]
    public record MexcTicker
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Price change
        /// </summary>
        [JsonPropertyName("priceChange")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// Price change percentage
        /// </summary>
        [JsonPropertyName("priceChangePercent")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// Previous day close price
        /// </summary>
        [JsonPropertyName("prevClosePrice")]
        public decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("bidPrice")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonPropertyName("bidQty")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("askPrice")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonPropertyName("askQty")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("openPrice")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonPropertyName("highPrice")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonPropertyName("lowPrice")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("volume"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal Volume { get; set; }
        /// <summary>
        /// Volume in quote asset
        /// </summary>
        [JsonPropertyName("quoteVolume"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal? QuoteVolume { get; set; }
        /// <summary>
        /// Open timestamp
        /// </summary>
        [JsonPropertyName("openTime")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Close timestamp
        /// </summary>
        [JsonPropertyName("closeTime")]
        public DateTime CloseTime { get; set; }
    }
}
