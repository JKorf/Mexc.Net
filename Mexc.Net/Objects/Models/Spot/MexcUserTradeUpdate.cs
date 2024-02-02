using Newtonsoft.Json;
using Mexc.Net.Enums;
using System;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// User trade update
    /// </summary>
    public class MexcUserTradeUpdate
    {
        /// <summary>
        /// Trade time
        /// </summary>
        [JsonProperty("T")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Trade side
        /// </summary>
        [JsonProperty("S")]
        public OrderSide TradeSide { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonProperty("a")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonProperty("c")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("i")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonProperty("t")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// Is maker
        /// </summary>
        [JsonProperty("m")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// Is self trade
        /// </summary>
        [JsonProperty("st")]
        public bool IsSelfTrade { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("v")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonProperty("n")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonProperty("N")]
        public string FeeAsset { get; set; } = string.Empty;
    }
}
