using CryptoExchange.Net.Converters;
using Mexc.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Kline info
    /// </summary>
    public record MexcFuturesKlines
    {
        /// <summary>
        /// Open time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime[] OpenTime { get; set; } = [];

        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("open")]
        public decimal[] OpenPrice { get; set; } = [];

        /// <summary>
        /// Close price
        /// </summary>
        [JsonPropertyName("close")]
        public decimal[] ClosePrice { get; set; } = [];

        /// <summary>
        /// High price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal[] HighPrice { get; set; } = [];

        /// <summary>
        /// Low price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal[] LowPrice { get; set; } = [];

        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal[] Volume { get; set; } = [];

        /// <summary>
        /// Quote volume
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal[] QuoteVolume { get; set; } = [];
    }
}
