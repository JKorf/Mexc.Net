using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Mini ticker
    /// </summary>
    [SerializationModel]
    public record MexcStreamMiniTick : MexcStreamEvent
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>p</c>"] Last price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>tr</c>"] 24h Change percentage in specified timezone 
        /// </summary>
        [JsonPropertyName("tr")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// ["<c>r</c>"] 24h Change percentage in Utc 8 timezone
        /// </summary>
        [JsonPropertyName("r")]
        public decimal PriceChangePercentageUtc8 { get; set; }
        /// <summary>
        /// ["<c>h</c>"] 24h high price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] 24h low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }

        // NOTE: These volumes seem to be reversed
        /// <summary>
        /// ["<c>q</c>"] 24h volume
        /// </summary>
        [JsonPropertyName("q"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>v</c>"] 24h quote volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal QuoteVolume { get; set; }
    }
}
