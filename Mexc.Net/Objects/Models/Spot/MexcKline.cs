using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters;
using Mexc.Net.Converters;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Kline/candlestick info
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<MexcKline>))]
    [SerializationModel]
    public record MexcKline
    {
        /// <summary>
        /// The time this candlestick opened
        /// </summary>
        [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// The price at which this candlestick opened
        /// </summary>
        [ArrayProperty(1)]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// The highest price in this candlestick
        /// </summary>
        [ArrayProperty(2)]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// The lowest price in this candlestick
        /// </summary>
        [ArrayProperty(3)]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        [ArrayProperty(4)]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [ArrayProperty(5), JsonConverter(typeof(BigDecimalConverter))]
        public decimal Volume { get; set; }
        /// <summary>
        /// Close Time
        /// </summary>
        [ArrayProperty(6), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// Volume in quote asset
        /// </summary>
        [ArrayProperty(7)]
        public decimal QuoteVolume { get; set; }
    }
}
