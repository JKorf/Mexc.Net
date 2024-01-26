using Mexc.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order info
    /// </summary>
    public class MexcOrder
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonProperty("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        public OrderType Type { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("transactTime")]
        public DateTime Timestamp { get; set; }
    }
}
