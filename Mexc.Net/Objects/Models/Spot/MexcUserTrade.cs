using Newtonsoft.Json;
using System;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// User trade info
    /// </summary>
    public record MexcUserTrade
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
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
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonProperty("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonProperty("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonProperty("commissionAsset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Is buyer
        /// </summary>
        [JsonProperty("isBuyer")]
        public bool IsBuyer { get; set; }
        /// <summary>
        /// Is maker
        /// </summary>
        [JsonProperty("isMaker")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// Is best match
        /// </summary>
        [JsonProperty("isBestMatch")]
        public bool IsBestMatch { get; set; }
        /// <summary>
        /// Is self trade
        /// </summary>
        [JsonProperty("isSelfTrade")]
        public bool IsSelfTrade { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonProperty("clientOrderId")]
        public string? ClientOrderId { get; set; }
    }
}
