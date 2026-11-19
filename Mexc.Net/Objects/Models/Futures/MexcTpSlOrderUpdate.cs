using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures take-profit and stop-loss order update
    /// </summary>
    public record MexcTpSlOrderUpdate
    {
        /// <summary>
        /// Tp/Sl order id
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
        /// Position id
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// Stop loss trigger price type
        /// </summary>
        public TriggerPriceType? StopLossPriceType { get; set; }

        /// <summary>
        /// Take profit trigger price type
        /// </summary>
        public TriggerPriceType? TakeProfitPriceType { get; set; }

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
        /// Trigger side
        /// </summary>
        public TriggerSide TriggerSide { get; set; }

        /// <summary>
        /// Position side
        /// </summary>
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Quantity placed
        /// </summary>
        public decimal? QuantityPlaced { get; set; }

        /// <summary>
        /// Placed order id
        /// </summary>
        public long PlacedOrderId { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public long Version { get; set; }

        /// <summary>
        /// Is finished
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        /// Profit loss vol type
        /// </summary>
        public ProfitLossVolumeType? ProfitLossVolType { get; set; }

        /// <summary>
        /// Take profit quantity
        /// </summary>
        public decimal? TakeProfitQuantity { get; set; }

        /// <summary>
        /// Stop loss quantity
        /// </summary>
        public decimal? StopLossQuantity { get; set; }

        /// <summary>
        /// Create time
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Update time
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Volume type
        /// </summary>
        public VolumeType? VolumeType { get; set; }

        /// <summary>
        /// Take profit reverse
        /// </summary>
        public FuturesBool? TakeProfitReverse { get; set; }

        /// <summary>
        /// Stop loss reverse
        /// </summary>
        public FuturesBool? StopLossReverse { get; set; }

        /// <summary>
        /// Close try times
        /// </summary>
        public int? CloseTryTimes { get; set; }

        /// <summary>
        /// Reverse try times
        /// </summary>
        public int? ReverseTryTimes { get; set; }

        /// <summary>
        /// Reverse error code
        /// </summary>
        public int ReverseErrorCode { get; set; }

        /// <summary>
        /// Stop loss type
        /// </summary>
        public TpSlType? StopLossType { get; set; }

        /// <summary>
        /// Stop loss order price
        /// </summary>
        public decimal? StopLossOrderPrice { get; set; }

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
