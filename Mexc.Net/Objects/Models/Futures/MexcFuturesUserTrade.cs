using CryptoExchange.Net.Converters;
using Mexc.Net.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// User trade
    /// </summary>
    public record MexcFuturesUserTrade
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public FuturesOrderSide Side { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Realized profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        [JsonPropertyName("category")]
        public OrderCategory Category { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Taker
        /// </summary>
        [JsonPropertyName("taker")]
        public bool Taker { get; set; }
    }


}
