using Newtonsoft.Json;
using Mexc.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order update
    /// </summary>
    public record MexcUserOrderUpdate
    {
        /// <summary>
        /// Quote quantity remaining
        /// </summary>
        [JsonProperty("A")]
        public decimal QuoteQuantityRemaining { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("O")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonProperty("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Quantity remaining
        /// </summary>
        [JsonProperty("V")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonProperty("a")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonProperty("c")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("i")]
        public string? OrderId { get; set; }
        /// <summary>
        /// Is maker
        /// </summary>
        [JsonProperty("m")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonProperty("o")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonProperty("s")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("v")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Average exceution price
        /// </summary>
        [JsonProperty("ap")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Cumulative quantity
        /// </summary>
        [JsonProperty("cv")]
        public decimal? CumulativeQuantity { get; set; }
        /// <summary>
        /// Cumulative quote quantity
        /// </summary>
        [JsonProperty("ca")]
        public decimal? CumulativeQuoteQuantity { get; set; }
    }
}
