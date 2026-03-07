using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// 
    /// </summary>
    public record MexcContract
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>displayName</c>"] Display name
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>displayNameEn</c>"] Display name english
        /// </summary>
        [JsonPropertyName("displayNameEn")]
        public string DisplayNameEnglish { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionOpenType</c>"] Position open type
        /// </summary>
        [JsonPropertyName("positionOpenType")]
        public PositionOpenType PositionOpenType { get; set; }
        /// <summary>
        /// ["<c>baseCoin</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseCoin")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteCoin</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteCoin")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>settleCoin</c>"] Settle asset
        /// </summary>
        [JsonPropertyName("settleCoin")]
        public string SettleAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractSize</c>"] Contract size
        /// </summary>
        [JsonPropertyName("contractSize")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// ["<c>minLeverage</c>"] Min leverage
        /// </summary>
        [JsonPropertyName("minLeverage")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// ["<c>maxLeverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>priceScale</c>"] Price scale
        /// </summary>
        [JsonPropertyName("priceScale")]
        public decimal PriceScale { get; set; }
        /// <summary>
        /// ["<c>volScale</c>"] Volume scale
        /// </summary>
        [JsonPropertyName("volScale")]
        public decimal VolumeScale { get; set; }
        /// <summary>
        /// ["<c>amountScale</c>"] Quantity scale
        /// </summary>
        [JsonPropertyName("amountScale")]
        public decimal QuantityScale { get; set; }
        /// <summary>
        /// ["<c>priceUnit</c>"] Price unit
        /// </summary>
        [JsonPropertyName("priceUnit")]
        public decimal PriceUnit { get; set; }
        /// <summary>
        /// ["<c>volUnit</c>"] Volume unit
        /// </summary>
        [JsonPropertyName("volUnit")]
        public decimal VolumeUnit { get; set; }
        /// <summary>
        /// ["<c>minVol</c>"] Min quantity
        /// </summary>
        [JsonPropertyName("minVol")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// ["<c>maxVol</c>"] Max quantity
        /// </summary>
        [JsonPropertyName("maxVol")]
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// ["<c>bidLimitPriceRate</c>"] Bid limit price rate
        /// </summary>
        [JsonPropertyName("bidLimitPriceRate")]
        public decimal BidLimitPriceRate { get; set; }
        /// <summary>
        /// ["<c>askLimitPriceRate</c>"] Ask limit price rate
        /// </summary>
        [JsonPropertyName("askLimitPriceRate")]
        public decimal AskLimitPriceRate { get; set; }
        /// <summary>
        /// ["<c>takerFeeRate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>makerFeeRate</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>maintenanceMarginRate</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintenanceMarginRate")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>initialMarginRate</c>"] Initial margin rate
        /// </summary>
        [JsonPropertyName("initialMarginRate")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>riskBaseVol</c>"] Risk base volume
        /// </summary>
        [JsonPropertyName("riskBaseVol")]
        public decimal RiskBaseVolume { get; set; }
        /// <summary>
        /// ["<c>riskIncrVol</c>"] Risk increase volume
        /// </summary>
        [JsonPropertyName("riskIncrVol")]
        public decimal RiskIncreaseVolume { get; set; }
        /// <summary>
        /// ["<c>riskIncrMmr</c>"] Risk increase maintenance margin rate
        /// </summary>
        [JsonPropertyName("riskIncrMmr")]
        public decimal RiskIncreaseMmr { get; set; }
        /// <summary>
        /// ["<c>riskIncrImr</c>"] Risk increase imr
        /// </summary>
        [JsonPropertyName("riskIncrImr")]
        public decimal RiskIncreaseImr { get; set; }
        /// <summary>
        /// ["<c>riskLevelLimit</c>"] Risk level limit
        /// </summary>
        [JsonPropertyName("riskLevelLimit")]
        public decimal RiskLevelLimit { get; set; }
        /// <summary>
        /// ["<c>priceCoefficientVariation</c>"] Price coefficient variation
        /// </summary>
        [JsonPropertyName("priceCoefficientVariation")]
        public decimal PriceCoefficientVariation { get; set; }
        /// <summary>
        /// ["<c>indexOrigin</c>"] Index origin
        /// </summary>
        [JsonPropertyName("indexOrigin")]
        public string[] IndexOrigin { get; set; } = [];
        /// <summary>
        /// ["<c>maxNumOrders</c>"] Max number of orders
        /// </summary>
        [JsonPropertyName("maxNumOrders")]
        public int[] MaxNumberOfOrders { get; set; } = [];
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public ContractStatus ContractStatus { get; set; }
        /// <summary>
        /// ["<c>isNew</c>"] Is new
        /// </summary>
        [JsonPropertyName("isNew")]
        public bool IsNew { get; set; }
        /// <summary>
        /// ["<c>isHot</c>"] Is hot
        /// </summary>
        [JsonPropertyName("isHot")]
        public bool IsHot { get; set; }
        /// <summary>
        /// ["<c>isHidden</c>"] Is hidden
        /// </summary>
        [JsonPropertyName("isHidden")]
        public bool IsHidden { get; set; }
        /// <summary>
        /// ["<c>conceptPlate</c>"] Concept plate
        /// </summary>
        [JsonPropertyName("conceptPlate")]
        public string[] ConceptPlate { get; set; } = [];
        /// <summary>
        /// ["<c>riskLimitType</c>"] Risk limit type
        /// </summary>
        [JsonPropertyName("riskLimitType")]
        public RiskLimitType RiskLimitType { get; set; }
        /// <summary>
        /// ["<c>marketOrderMaxLevel</c>"] Market order max level
        /// </summary>
        [JsonPropertyName("marketOrderMaxLevel")]
        public int MarketOrderMaxLevel { get; set; }
        /// <summary>
        /// ["<c>marketOrderPriceLimitRate1</c>"] Market order price limit rate 1
        /// </summary>
        [JsonPropertyName("marketOrderPriceLimitRate1")]
        public decimal MarketOrderPriceLimitRate1 { get; set; }
        /// <summary>
        /// ["<c>marketOrderPriceLimitRate2</c>"] Market order price limit rate 2
        /// </summary>
        [JsonPropertyName("marketOrderPriceLimitRate2")]
        public decimal MarketOrderPriceLimitRate2 { get; set; }
        /// <summary>
        /// ["<c>triggerProtect</c>"] Trigger protect
        /// </summary>
        [JsonPropertyName("triggerProtect")]
        public decimal TriggerProtect { get; set; }
        /// <summary>
        /// ["<c>appraisal</c>"] Appraisal
        /// </summary>
        [JsonPropertyName("appraisal")]
        public decimal Appraisal { get; set; }
        /// <summary>
        /// ["<c>showAppraisalCountdown</c>"] Show appraisal countdown
        /// </summary>
        [JsonPropertyName("showAppraisalCountdown")]
        public decimal ShowAppraisalCountdown { get; set; }
        /// <summary>
        /// ["<c>automaticDelivery</c>"] Automatic delivery
        /// </summary>
        [JsonPropertyName("automaticDelivery")]
        public decimal AutomaticDelivery { get; set; }
        /// <summary>
        /// ["<c>apiAllowed</c>"] Api allowed
        /// </summary>
        [JsonPropertyName("apiAllowed")]
        public bool ApiAllowed { get; set; }
    }


}
