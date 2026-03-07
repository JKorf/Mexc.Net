namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Kline info
    /// </summary>
    public record MexcFuturesKlines
    {
        /// <summary>
        /// ["<c>time</c>"] Open time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime[] OpenTime { get; set; } = [];

        /// <summary>
        /// ["<c>open</c>"] Open price
        /// </summary>
        [JsonPropertyName("open")]
        public decimal[] OpenPrice { get; set; } = [];

        /// <summary>
        /// ["<c>close</c>"] Close price
        /// </summary>
        [JsonPropertyName("close")]
        public decimal[] ClosePrice { get; set; } = [];

        /// <summary>
        /// ["<c>high</c>"] High price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal[] HighPrice { get; set; } = [];

        /// <summary>
        /// ["<c>low</c>"] Low price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal[] LowPrice { get; set; } = [];

        /// <summary>
        /// ["<c>vol</c>"] Volume
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal[] Volume { get; set; } = [];

        /// <summary>
        /// ["<c>amount</c>"] Quote volume
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal[] QuoteVolume { get; set; } = [];
    }
}
