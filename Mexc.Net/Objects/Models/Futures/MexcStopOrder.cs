using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Stop order
    /// </summary>
    public record MexcStopOrder
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionId</c>"] Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public long PositionId { get; set; }
        /// <summary>
        /// ["<c>stopLossPrice</c>"] Stop loss price
        /// </summary>
        [JsonPropertyName("stopLossPrice")]
        public decimal? StopLossPrice { get; set; }
        /// <summary>
        /// ["<c>takeProfitPrice</c>"] Take profit price
        /// </summary>
        [JsonPropertyName("takeProfitPrice")]
        public decimal? TakeProfitPrice { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public FuturesOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>triggerSide</c>"] Trigger side
        /// </summary>
        [JsonPropertyName("triggerSide")]
        public TriggerSide TriggerSide { get; set; }
        /// <summary>
        /// ["<c>positionType</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>vol</c>"] Quantity
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>realityVol</c>"] Actual quantity
        /// </summary>
        [JsonPropertyName("realityVol")]
        public decimal ActualQuantity { get; set; }
        /// <summary>
        /// ["<c>placeOrderId</c>"] Placed order id
        /// </summary>
        [JsonPropertyName("placeOrderId")]
        public long? PlacedOrderId { get; set; }
        /// <summary>
        /// ["<c>errorCode</c>"] Error code
        /// </summary>
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }
        /// <summary>
        /// ["<c>version</c>"] Version
        /// </summary>
        [JsonPropertyName("version")]
        public int Version { get; set; }
        /// <summary>
        /// ["<c>isFinished</c>"] Is finished
        /// </summary>
        [JsonPropertyName("isFinished")]
        public bool IsFinished { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }


}
