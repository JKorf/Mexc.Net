using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures plan order update
    /// </summary>
    public record MexcPlanOrderUpdate
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Trigger price
        /// </summary>
        public decimal? TriggerPrice { get; set; }

        /// <summary>
        /// Limit order price
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Stop loss price
        /// </summary>
        public decimal? StopLossPrice { get; set; }

        /// <summary>
        /// Take profit price
        /// </summary>
        public decimal? TakeProfitPrice { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public TpSlStatus Status { get; set; }

        /// <summary>
        /// Margin type
        /// </summary>
        public MarginType? MarginType { get; set; }

        /// <summary>
        /// Trigger type
        /// </summary>
        public TriggerType TriggerType { get; set; }

        /// <summary>
        /// Order side
        /// </summary>
        public FuturesOrderSide Side { get; set; }

        /// <summary>
        /// Trigger price type
        /// </summary>
        public TriggerPriceType TriggerPriceType { get; set; }

        /// <summary>
        /// Stop loss trigger price type
        /// </summary>
        public TriggerPriceType? StopLossPriceType { get; set; }

        /// <summary>
        /// Take profit trigger price type
        /// </summary>
        public TriggerPriceType? TakeProfitPriceType { get; set; }

        /// <summary>
        /// Order type
        /// </summary>
        public FuturesOrderType OrderType { get; set; }

        /// <summary>
        /// Position mode
        /// </summary>
        public PositionMode? PositionMode { get; set; }

        /// <summary>
        /// Execution cycle
        /// </summary>
        public ExecuteCycle? ExecuteCycle { get; set; }

        /// <summary>
        /// Market order level
        /// </summary>
        public int MarketOrderLevel { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Leverage
        /// </summary>
        public decimal Leverage { get; set; }

        /// <summary>
        /// Reduce only
        /// </summary>
        public bool ReduceOnly { get; set; }

        /// <summary>
        /// Create time
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Update time
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Whether guaranteed stop loss is enabled
        /// </summary>
        public bool? EnsureStopLoss { get; set; }

        /// <summary>
        /// Additional taker fee rate
        /// </summary>
        public decimal? ExtraTakerFeeRate { get; set; }
    }
}
