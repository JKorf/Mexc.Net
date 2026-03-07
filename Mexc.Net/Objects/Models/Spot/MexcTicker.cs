namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Ticker info
    /// </summary>
    [SerializationModel]
    public record MexcTicker
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>priceChange</c>"] Price change
        /// </summary>
        [JsonPropertyName("priceChange")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// ["<c>priceChangePercent</c>"] Price change percentage
        /// </summary>
        [JsonPropertyName("priceChangePercent")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// ["<c>prevClosePrice</c>"] Previous day close price
        /// </summary>
        [JsonPropertyName("prevClosePrice")]
        public decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// ["<c>lastPrice</c>"] Last price
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>bidPrice</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bidPrice")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bidQty</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("bidQty")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>askPrice</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("askPrice")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>askQty</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("askQty")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>openPrice</c>"] Open price
        /// </summary>
        [JsonPropertyName("openPrice")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>highPrice</c>"] High price
        /// </summary>
        [JsonPropertyName("highPrice")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>lowPrice</c>"] Low price
        /// </summary>
        [JsonPropertyName("lowPrice")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Volume
        /// </summary>
        [JsonPropertyName("volume"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>quoteVolume</c>"] Volume in quote asset
        /// </summary>
        [JsonPropertyName("quoteVolume"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal? QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>openTime</c>"] Open timestamp
        /// </summary>
        [JsonPropertyName("openTime")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>closeTime</c>"] Close timestamp
        /// </summary>
        [JsonPropertyName("closeTime")]
        public DateTime CloseTime { get; set; }
    }
}
