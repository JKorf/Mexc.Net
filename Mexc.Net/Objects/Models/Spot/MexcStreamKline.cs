using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Start time
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Volume in quote asset
        /// </summary>
        [JsonPropertyName("a"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// Highest price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Interval
        /// </summary>
        [JsonPropertyName("i")]
        public KlineInterval Interval { get; set; }
        /// <summary>
        /// Lowest price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("v")]
        [JsonConverter(typeof(BigDecimalConverter))]
        public decimal Volume { get; set; }
    }
}
