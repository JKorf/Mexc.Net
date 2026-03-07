using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Order info
    /// </summary>
    public record MexcFuturesOrder
    {
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
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>vol</c>"] Quantity
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>remainVol</c>"] Quantity open
        /// </summary>
        [JsonPropertyName("remainVol")]
        public decimal? QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public FuturesOrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>category</c>"] Category
        /// </summary>
        [JsonPropertyName("category")]
        public OrderCategory Category { get; set; }
        /// <summary>
        /// ["<c>orderType</c>"] Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>dealAvgPrice</c>"] Average fill price
        /// </summary>
        [JsonPropertyName("dealAvgPrice")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>dealVol</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("dealVol")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>orderMargin</c>"] Order margin
        /// </summary>
        [JsonPropertyName("orderMargin")]
        public decimal OrderMargin { get; set; }
        /// <summary>
        /// ["<c>takerFee</c>"] Taker fee
        /// </summary>
        [JsonPropertyName("takerFee")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// ["<c>makerFee</c>"] Maker fee
        /// </summary>
        [JsonPropertyName("makerFee")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// ["<c>profit</c>"] Profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>openType</c>"] Margin type
        /// </summary>
        [JsonPropertyName("openType")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Order status
        /// </summary>
        [JsonPropertyName("state")]
        public FuturesOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>externalOid</c>"] Client order id
        /// </summary>
        [JsonPropertyName("externalOid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>errorCode</c>"] Error code
        /// </summary>
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }
        /// <summary>
        /// ["<c>usedMargin</c>"] Used margin
        /// </summary>
        [JsonPropertyName("usedMargin")]
        public decimal UsedMargin { get; set; }
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
        /// ["<c>version</c>"] Version
        /// </summary>
        [JsonPropertyName("version")]
        public int? Version { get; set; }
    }


}
