using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures position update
    /// </summary>
    public record MexcPositionUpdate
    {
        /// <summary>
        /// Position id
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Position side
        /// </summary>
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// Margin type
        /// </summary>
        public MarginType MarginType { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public PositionStatus Status { get; set; }

        /// <summary>
        /// Holding volume
        /// </summary>
        public decimal PositionSize { get; set; }

        /// <summary>
        /// Frozen volume
        /// </summary>
        public decimal FrozenVolume { get; set; }

        /// <summary>
        /// Close volume
        /// </summary>
        public decimal CloseVolume { get; set; }

        /// <summary>
        /// Hold average price
        /// </summary>
        public decimal HoldAveragePrice { get; set; }

        /// <summary>
        /// Open average price
        /// </summary>
        public decimal OpenAveragePrice { get; set; }

        /// <summary>
        /// Close average price
        /// </summary>
        public decimal CloseAveragePrice { get; set; }

        /// <summary>
        /// Liquidate price
        /// </summary>
        public decimal LiquidationPrice { get; set; }

        /// <summary>
        /// Original initial margin
        /// </summary>
        public decimal OriginalInitialMargin { get; set; }

        /// <summary>
        /// Initial margin
        /// </summary>
        public decimal InitialMargin { get; set; }

        /// <summary>
        /// Hold fee
        /// </summary>
        public decimal HoldFee { get; set; }

        /// <summary>
        /// Realised profit and loss
        /// </summary>
        public decimal RealisedPnl { get; set; }

        /// <summary>
        /// Profit and loss
        /// </summary>
        public decimal? Pnl { get; set; }

        /// <summary>
        /// Auto deleverage level
        /// </summary>
        public int? AdlLevel { get; set; }

        /// <summary>
        /// Leverage
        /// </summary>
        public int Leverage { get; set; }

        /// <summary>
        /// Margin ratio
        /// </summary>
        public decimal? MarginRatio { get; set; }

        /// <summary>
        /// Create time
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Update time
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Auto margin enabled
        /// </summary>
        public bool AutoMargin { get; set; }

        /// <summary>
        /// Update version
        /// </summary>
        public long Version { get; set; }

        /// <summary>
        /// New open average price
        /// </summary>
        public decimal? NewOpenAveragePrice { get; set; }

        /// <summary>
        /// New close average price
        /// </summary>
        public decimal? NewCloseAveragePrice { get; set; }

        /// <summary>
        /// Close profit and loss
        /// </summary>
        public decimal? CloseProfitAndLoss { get; set; }

        /// <summary>
        /// Fee
        /// </summary>
        public decimal? Fee { get; set; }

        /// <summary>
        /// Average holding price at full precision
        /// </summary>
        public decimal? HoldAveragePriceFullyScale { get; set; }

        /// <summary>
        /// Average opening price at full precision
        /// </summary>
        public decimal? OpenAveragePriceFullyScale { get; set; }

        /// <summary>
        /// Maintenance margin
        /// </summary>
        public decimal? MaintenanceMargin { get; set; }

        /// <summary>
        /// Position fee deductions by asset
        /// </summary>
        public MexcPositionFeeDeduction[] FeeDeductions { get; set; } = [];

        /// <summary>
        /// Maker fee rate
        /// </summary>
        public decimal? MakerFeeRate { get; set; }

        /// <summary>
        /// Taker fee rate
        /// </summary>
        public decimal? TakerFeeRate { get; set; }
    }

    /// <summary>
    /// Fee deducted from a futures position
    /// </summary>
    public record MexcPositionFeeDeduction
    {
        /// <summary>
        /// Asset used to pay the fee
        /// </summary>
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Fee in the deduction asset
        /// </summary>
        public decimal DeductedFee { get; set; }

        /// <summary>
        /// Fee converted to the settlement asset
        /// </summary>
        public decimal SettlementFee { get; set; }
    }
}
