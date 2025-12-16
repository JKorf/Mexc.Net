namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Kline info
    /// </summary>
    public record MexcFuturesKline
    {
        /// <summary>
        /// Open time
        /// </summary>
        public DateTime OpenTime { get; set; }

        /// <summary>
        /// Open price
        /// </summary>
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// Close price
        /// </summary>
        public decimal ClosePrice { get; set; }

        /// <summary>
        /// High price
        /// </summary>
        public decimal HighPrice { get; set; }

        /// <summary>
        /// Low price
        /// </summary>
        public decimal LowPrice { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Quote volume
        /// </summary>
        public decimal QuoteVolume { get; set; }
    }
}
