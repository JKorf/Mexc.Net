using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Kline info
    /// </summary>
    [SerializationModel]
    public record MexcStreamKline
    {
        /// <summary>
        /// ["<c>t</c>"] Start time
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// ["<c>T</c>"] End time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Volume in quote asset
        /// </summary>
        [JsonPropertyName("a"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] Highest price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>i</c>"] Interval
        /// </summary>
        [JsonPropertyName("i")]
        public KlineInterval Interval { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Lowest price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Volume
        /// </summary>
        [JsonPropertyName("v")]
        [JsonConverter(typeof(BigDecimalConverter))]
        public decimal Volume { get; set; }
    }
}
