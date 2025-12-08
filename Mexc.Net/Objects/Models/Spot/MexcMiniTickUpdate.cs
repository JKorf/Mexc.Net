namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Mini ticker
    /// </summary>
    public record MexcMiniTickUpdate
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last price
        /// </summary>
        public decimal LastPrice { get; set; }
        /// <summary>
        /// 24h Change percentage in specified timezone 
        /// </summary>
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// 24h Change percentage in Utc 8 timezone
        /// </summary>
        public decimal PriceChangePercentageUtc8 { get; set; }
        /// <summary>
        /// 24h high price
        /// </summary>
        public decimal HighPrice { get; set; }
        /// <summary>
        /// 24h low price
        /// </summary>
        public decimal LowPrice { get; set; }
        /// <summary>
        /// 24h volume
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// 24h quote volume
        /// </summary>
        public decimal QuoteVolume { get; set; }
    }
}
