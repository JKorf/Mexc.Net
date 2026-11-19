using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures contract update
    /// </summary>
    public record MexcContractUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Display name in  English
        /// </summary>
        public string DisplayNameEnglish { get; set; } = string.Empty;

        /// <summary>
        /// Position open type
        /// </summary>
        public PositionOpenType PositionOpenType { get; set; }

        /// <summary>
        /// Base asset
        /// </summary>
        public string BaseAsset { get; set; } = string.Empty;

        /// <summary>
        /// Base asset name
        /// </summary>
        public string BaseAssetName { get; set; } = string.Empty;

        /// <summary>
        /// Quote asset
        /// </summary>
        public string QuoteAsset { get; set; } = string.Empty;

        /// <summary>
        /// Quote asset name
        /// </summary>
        public string QuoteAssetName { get; set; } = string.Empty;

        /// <summary>
        /// Settle asset
        /// </summary>
        public string SettleAsset { get; set; } = string.Empty;

        /// <summary>
        /// Contract size
        /// </summary>
        public decimal ContractSize { get; set; }

        /// <summary>
        /// Min leverage
        /// </summary>
        public decimal MinLeverage { get; set; }

        /// <summary>
        /// Max leverage
        /// </summary>
        public decimal MaxLeverage { get; set; }

        /// <summary>
        /// Price scale
        /// </summary>
        public decimal PriceScale { get; set; }

        /// <summary>
        /// Volume scale
        /// </summary>
        public decimal VolumeScale { get; set; }

        /// <summary>
        /// Quantity scale
        /// </summary>
        public decimal QuantityScale { get; set; }

        /// <summary>
        /// Price unit
        /// </summary>
        public decimal PriceUnit { get; set; }

        /// <summary>
        /// Volume unit
        /// </summary>
        public decimal VolumeUnit { get; set; }

        /// <summary>
        /// Min quantity
        /// </summary>
        public decimal MinQuantity { get; set; }

        /// <summary>
        /// Max quantity
        /// </summary>
        public decimal MaxQuantity { get; set; }

        /// <summary>
        /// Bid limit price rate
        /// </summary>
        public decimal BidLimitPriceRate { get; set; }

        /// <summary>
        /// Ask limit price rate
        /// </summary>
        public decimal AskLimitPriceRate { get; set; }

        /// <summary>
        /// Taker fee rate
        /// </summary>
        public decimal TakerFeeRate { get; set; }

        /// <summary>
        /// Maker fee rate
        /// </summary>
        public decimal MakerFeeRate { get; set; }

        /// <summary>
        /// Maintenance margin rate
        /// </summary>
        public decimal MaintenanceMarginRate { get; set; }

        /// <summary>
        /// Initial margin rate
        /// </summary>
        public decimal InitialMarginRate { get; set; }

        /// <summary>
        /// Risk base volume
        /// </summary>
        public decimal RiskBaseVolume { get; set; }

        /// <summary>
        /// Risk increase volume
        /// </summary>
        public decimal RiskIncreaseVolume { get; set; }

        /// <summary>
        /// Risk increase maintenance margin rate
        /// </summary>
        public decimal RiskIncreaseMmr { get; set; }

        /// <summary>
        /// Risk increase imr
        /// </summary>
        public decimal RiskIncreaseImr { get; set; }

        /// <summary>
        /// Risk level limit
        /// </summary>
        public decimal RiskLevelLimit { get; set; }

        /// <summary>
        /// Price coefficient variation
        /// </summary>
        public decimal PriceCoefficientVariation { get; set; }

        /// <summary>
        /// Index origin
        /// </summary>
        public string[] IndexOrigin { get; set; } = [];

        /// <summary>
        /// Max number of orders
        /// </summary>
        public int[] MaxNumberOfOrders { get; set; } = [];

        /// <summary>
        /// Status
        /// </summary>
        public ContractStatus ContractStatus { get; set; }

        /// <summary>
        /// Is new
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Is hot
        /// </summary>
        public bool IsHot { get; set; }

        /// <summary>
        /// Is hidden
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Concept plate
        /// </summary>
        public string[] ConceptPlate { get; set; } = [];

        /// <summary>
        /// Concept plate id
        /// </summary>
        public long[] ConceptPlateId { get; set; } = [];

        /// <summary>
        /// Risk limit type
        /// </summary>
        public RiskLimitType? RiskLimitType { get; set; }

        /// <summary>
        /// Trigger protection threshold
        /// </summary>
        public decimal TriggerProtect { get; set; }

        /// <summary>
        /// Supported order book depth steps
        /// </summary>
        public string[] DepthStepList { get; set; } = [];

        /// <summary>
        /// Fee rate mode code
        /// </summary>
        public int FeeRateMode { get; set; }

        /// <summary>
        /// Future type
        /// </summary>
        public int FuturesType { get; set; }

        /// <summary>
        /// Liquidation fee rate
        /// </summary>
        public decimal LiquidationFeeRate { get; set; }

        /// <summary>
        /// Limit order max size
        /// </summary>
        public decimal MaxOrderQuantityLimit { get; set; }

        /// <summary>
        /// Opening timestamp
        /// </summary>
        public DateTime? OpenTime { get; set; }

        /// <summary>
        /// Risk long short switch
        /// </summary>
        public bool RiskLongShortSwitch { get; set; }

        /// <summary>
        /// Opening countdown show option
        /// </summary>
        public int? OpeningCountdownOption { get; set; }

        /// <summary>
        /// Custom risk limit list in CUSTOM mode
        /// </summary>
        public MexcCustomRiskLimit[] CustomRiskLimits { get; set; } = [];

        /// <summary>
        /// Risk limit rate
        /// </summary>
        public decimal? RiskLimitRate { get; set; }
    }
}
