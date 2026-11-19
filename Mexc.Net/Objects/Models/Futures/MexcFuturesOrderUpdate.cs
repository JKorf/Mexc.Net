using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures order update
    /// </summary>
    public record MexcFuturesOrderUpdate
    {
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Position id
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Leverage
        /// </summary>
        public int Leverage { get; set; }

        /// <summary>
        /// Order side
        /// </summary>
        public FuturesOrderSide OrderSide { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public OrderCategory Category { get; set; }

        /// <summary>
        /// Order type
        /// </summary>
        public FuturesOrderType OrderType { get; set; }

        /// <summary>
        /// Average fill price
        /// </summary>
        public decimal? AveragePrice { get; set; }

        /// <summary>
        /// Quantity filled
        /// </summary>
        public decimal QuantityFilled { get; set; }

        /// <summary>
        /// Order margin
        /// </summary>
        public decimal OrderMargin { get; set; }

        /// <summary>
        /// Taker fee
        /// </summary>
        public decimal TakerFee { get; set; }

        /// <summary>
        /// Maker fee
        /// </summary>
        public decimal MakerFee { get; set; }

        /// <summary>
        /// Profit
        /// </summary>
        public decimal Profit { get; set; }

        /// <summary>
        /// Fee asset
        /// </summary>
        public string FeeAsset { get; set; } = string.Empty;

        /// <summary>
        /// Margin type
        /// </summary>
        public MarginType MarginType { get; set; }

        /// <summary>
        /// Order status
        /// </summary>
        public FuturesOrderStatus Status { get; set; }

        /// <summary>
        /// Client order id
        /// </summary>
        public string? ClientOrderId { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        public long ErrorCode { get; set; }

        /// <summary>
        /// Used margin
        /// </summary>
        public decimal UsedMargin { get; set; }

        /// <summary>
        /// Create time
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Update time
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public long? Version { get; set; }

        /// <summary>
        /// Position mode
        /// </summary>
        public PositionMode PositionMode { get; set; }

        /// <summary>
        /// Reduce only
        /// </summary>
        public bool ReduceOnly { get; set; }

        /// <summary>
        /// Limit order type
        /// </summary>
        public LimitOrderType LimitOrderType { get; set; }

        /// <summary>
        /// Total maker and taker fee, when both are supplied
        /// </summary>
        public decimal? TotalFee { get; set; }

        /// <summary>
        /// Remaining order quantity
        /// </summary>
        public decimal RemainingQuantity { get; set; }

        /// <summary>
        /// Market order level
        /// </summary>
        public int? MarketOrderLevel { get; set; }

        /// <summary>
        /// Maker fee rate
        /// </summary>
        public decimal? MakerFeeRate { get; set; }

        /// <summary>
        /// Taker fee rate
        /// </summary>
        public decimal? TakerFeeRate { get; set; }

        /// <summary>
        /// Order source code
        /// </summary>
        public int? BusinessSource { get; set; }

        /// <summary>
        /// Buy or sell side
        /// </summary>
        public OrderSide? SideType { get; set; }

        /// <summary>
        /// Long or short position side
        /// </summary>
        public PositionSide? PositionType { get; set; }
    }
}
