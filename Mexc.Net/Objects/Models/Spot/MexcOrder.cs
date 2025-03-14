using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record MexcOrder
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId"), JsonConverter(typeof(NumberStringConverter))]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Iceberg quantity
        /// </summary>
        [JsonPropertyName("icebergQty")]
        public decimal? IcebergQty { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// The currently executed amount of quote asset. Amounts to Sum(quantity * price) of executed trades for this order
        /// </summary>
        [JsonPropertyName("cummulativeQuoteQty")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Last update timestamp
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("transactTime")]
        public DateTime Timestamp { get; set; }

        [JsonInclude, JsonPropertyName("time")]
        internal DateTime _time
        {
            get => Timestamp;
            set => Timestamp = value;
        }

        /// <summary>
        /// Original client order id
        /// </summary>
        [JsonPropertyName("origClientOrderId")]
        public string? OriginalClientOrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Is order book
        /// </summary>
        [JsonPropertyName("isWorking")]
        public bool IsWorking { get; set; }
        /// <summary>
        /// Original quote order quantity
        /// </summary>
        [JsonPropertyName("origQuoteOrderQty")]
        public decimal? QuoteQuantity { get; set; }
    }
}
