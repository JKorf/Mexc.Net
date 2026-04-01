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
        /// ["<c>triggerPrice</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("triggerPrice")]
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Limit order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
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
        /// ["<c>openType</c>"] Margin type
        /// </summary>
        [JsonPropertyName("openType")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// ["<c>triggerType</c>"] Trigger type
        /// </summary>
        [JsonPropertyName("triggerType")]
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public FuturesOrderSide Side { get; set; }
        /// <summary>
        /// ["<c>positionType</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>trend</c>"] Trigger price type
        /// </summary>
        [JsonPropertyName("trend")]
        public TriggerPriceType TriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>lossTrend</c>"] Stop loss trigger price type
        /// </summary>
        [JsonPropertyName("lossTrend")]
        public TriggerPriceType StopLossPriceType { get; set; }
        /// <summary>
        /// ["<c>profitTrend</c>"] Take profit trigger price type
        /// </summary>
        [JsonPropertyName("profitTrend")]
        public TriggerPriceType TakeProfitPriceType { get; set; }
        /// <summary>
        /// ["<c>orderType</c>"] Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public FuturesOrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>positionMode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("positionMode")]
        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// ["<c>executeCycle</c>"] Execute cycle in hours
        /// </summary>
        [JsonPropertyName("executeCycle")]
        public int ExecuteCycle { get; set; }
        /// <summary>
        /// ["<c>marketOrderLevel</c>"] Market order level
        /// </summary>
        [JsonPropertyName("marketOrderLevel")]
        public int MarketOrderLevel { get; set; }
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
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>isFinished</c>"] Is finished
        /// </summary>
        [JsonPropertyName("isFinished")]
        public bool IsFinished { get; set; }
        /// <summary>
        /// ["<c>reduceOnly</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduceOnly")]
        public bool ReduceOnly { get; set; }
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
