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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId"), JsonConverter(typeof(NumberStringConverter))]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>origQty</c>"] Quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>icebergQty</c>"] Iceberg quantity
        /// </summary>
        [JsonPropertyName("icebergQty")]
        public decimal? IcebergQty { get; set; }
        /// <summary>
        /// ["<c>executedQty</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>cummulativeQuoteQty</c>"] The currently executed amount of quote asset. Amounts to Sum(quantity * price) of executed trades for this order
        /// </summary>
        [JsonPropertyName("cummulativeQuoteQty")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Last update timestamp
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>transactTime</c>"] Timestamp
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
        /// ["<c>origClientOrderId</c>"] Original client order id
        /// </summary>
        [JsonPropertyName("origClientOrderId")]
        public string? OriginalClientOrderId { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>stopPrice</c>"] Stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// ["<c>isWorking</c>"] Is order book
        /// </summary>
        [JsonPropertyName("isWorking")]
        public bool IsWorking { get; set; }
        /// <summary>
        /// ["<c>origQuoteOrderQty</c>"] Original quote order quantity
        /// </summary>
        [JsonPropertyName("origQuoteOrderQty")]
        public decimal? QuoteQuantity { get; set; }
    }
}
