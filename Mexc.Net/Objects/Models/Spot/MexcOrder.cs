using Mexc.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order info
    /// </summary>
    public record MexcOrder
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
        /// Quantity filled
        /// </summary>
        [JsonProperty("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// The currently executed amount of quote asset. Amounts to Sum(quantity * price) of executed trades for this order
        /// </summary>
        [JsonProperty("cummulativeQuoteQty")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonProperty("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonProperty("timeInForce")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Last update timestamp
        /// </summary>
        [JsonProperty("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("transactTime")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("time")]
        internal DateTime _time
        {
            get => Timestamp;
            set => Timestamp = value;
        }

        /// <summary>
        /// Original client order id
        /// </summary>
        [JsonProperty("origClientOrderId")]
        public string? OriginalClientOrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonProperty("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonProperty("stopPrice")]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Is order book
        /// </summary>
        [JsonProperty("isWorking")]
        public bool IsWorking { get; set; }
        /// <summary>
        /// Original quote order quantity
        /// </summary>
        [JsonProperty("origQuoteOrderQty")]
        public decimal? QuoteQuantity { get; set; }
    }
}
