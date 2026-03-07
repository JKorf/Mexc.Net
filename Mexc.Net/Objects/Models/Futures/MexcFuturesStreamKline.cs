using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Kline info
    /// </summary>
    public record MexcFuturesStreamKline
    {
        /// <summary>
        /// ["<c>t</c>"] Open time
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime OpenTime { get; set; }

        /// <summary>
        /// ["<c>o</c>"] Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// ["<c>c</c>"] Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }

        /// <summary>
        /// ["<c>h</c>"] High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }

        /// <summary>
        /// ["<c>l</c>"] Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }

        /// <summary>
        /// ["<c>a</c>"] Volume
        /// </summary>
        [JsonPropertyName("a")]
        public decimal Volume { get; set; }

        /// <summary>
        /// ["<c>q</c>"] Quote volume
        /// </summary>
        [JsonPropertyName("q")]
        public decimal QuoteVolume { get; set; }

        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>interval</c>"] Interval
        /// </summary>
        [JsonPropertyName("interval")]
        public FuturesKlineInterval Interval { get; set; }
    }
}
