using Mexc.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// 
    /// </summary>
    public record MexcContract
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Display name
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Display name english
        /// </summary>
        [JsonPropertyName("displayNameEn")]
        public string DisplayNameEnglish { get; set; } = string.Empty;
        /// <summary>
        /// Position open type
        /// </summary>
        [JsonPropertyName("positionOpenType")]
        public PositionOpenType PositionOpenType { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("baseCoin")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quoteCoin")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Settle asset
        /// </summary>
        [JsonPropertyName("settleCoin")]
        public string SettleAsset { get; set; } = string.Empty;
        /// <summary>
        /// Contract size
        /// </summary>
        [JsonPropertyName("contractSize")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// Min leverage
        /// </summary>
        [JsonPropertyName("minLeverage")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Price scale
        /// </summary>
        [JsonPropertyName("priceScale")]
        public decimal PriceScale { get; set; }
        /// <summary>
        /// Volume scale
        /// </summary>
        [JsonPropertyName("volScale")]
        public decimal VolumeScale { get; set; }
        /// <summary>
        /// Quantity scale
        /// </summary>
        [JsonPropertyName("amountScale")]
        public decimal QuantityScale { get; set; }
        /// <summary>
        /// Price unit
        /// </summary>
        [JsonPropertyName("priceUnit")]
        public decimal PriceUnit { get; set; }
        /// <summary>
        /// Volume unit
        /// </summary>
        [JsonPropertyName("volUnit")]
        public decimal VolumeUnit { get; set; }
        /// <summary>
        /// Min quantity
        /// </summary>
        [JsonPropertyName("minVol")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// Max quantity
        /// </summary>
        [JsonPropertyName("maxVol")]
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// Bid limit price rate
        /// </summary>
        [JsonPropertyName("bidLimitPriceRate")]
        public decimal BidLimitPriceRate { get; set; }
        /// <summary>
        /// Ask limit price rate
        /// </summary>
        [JsonPropertyName("askLimitPriceRate")]
        public decimal AskLimitPriceRate { get; set; }
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintenanceMarginRate")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// Initial margin rate
        /// </summary>
        [JsonPropertyName("initialMarginRate")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// Risk base volume
        /// </summary>
        [JsonPropertyName("riskBaseVol")]
        public decimal RiskBaseVolume { get; set; }
        /// <summary>
        /// Risk increase volume
        /// </summary>
        [JsonPropertyName("riskIncrVol")]
        public decimal RiskIncreaseVolume { get; set; }
        /// <summary>
        /// Risk increase maintenance margin rate
        /// </summary>
        [JsonPropertyName("riskIncrMmr")]
        public decimal RiskIncreaseMmr { get; set; }
        /// <summary>
        /// Risk increase imr
        /// </summary>
        [JsonPropertyName("riskIncrImr")]
        public decimal RiskIncreaseImr { get; set; }
        /// <summary>
        /// Risk level limit
        /// </summary>
        [JsonPropertyName("riskLevelLimit")]
        public decimal RiskLevelLimit { get; set; }
        /// <summary>
        /// Price coefficient variation
        /// </summary>
        [JsonPropertyName("priceCoefficientVariation")]
        public decimal PriceCoefficientVariation { get; set; }
        /// <summary>
        /// Index origin
        /// </summary>
        [JsonPropertyName("indexOrigin")]
        public string[] IndexOrigin { get; set; } = [];
        /// <summary>
        /// Max number of orders
        /// </summary>
        [JsonPropertyName("maxNumOrders")]
        public int[] MaxNumberOfOrders { get; set; } = [];
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("state")]
        public ContractStatus ContractStatus { get; set; }
        /// <summary>
        /// Is new
        /// </summary>
        [JsonPropertyName("isNew")]
        public bool IsNew { get; set; }
        /// <summary>
        /// Is hot
        /// </summary>
        [JsonPropertyName("isHot")]
        public bool IsHot { get; set; }
        /// <summary>
        /// Is hidden
        /// </summary>
        [JsonPropertyName("isHidden")]
        public bool IsHidden { get; set; }
        /// <summary>
        /// Concept plate
        /// </summary>
        [JsonPropertyName("conceptPlate")]
        public string[] ConceptPlate { get; set; } = [];
        /// <summary>
        /// Risk limit type
        /// </summary>
        [JsonPropertyName("riskLimitType")]
        public RiskLimitType RiskLimitType { get; set; }
        /// <summary>
        /// Market order max level
        /// </summary>
        [JsonPropertyName("marketOrderMaxLevel")]
        public int MarketOrderMaxLevel { get; set; }
        /// <summary>
        /// Market order price limit rate 1
        /// </summary>
        [JsonPropertyName("marketOrderPriceLimitRate1")]
        public decimal MarketOrderPriceLimitRate1 { get; set; }
        /// <summary>
        /// Market order price limit rate 2
        /// </summary>
        [JsonPropertyName("marketOrderPriceLimitRate2")]
        public decimal MarketOrderPriceLimitRate2 { get; set; }
        /// <summary>
        /// Trigger protect
        /// </summary>
        [JsonPropertyName("triggerProtect")]
        public decimal TriggerProtect { get; set; }
        /// <summary>
        /// Appraisal
        /// </summary>
        [JsonPropertyName("appraisal")]
        public decimal Appraisal { get; set; }
        /// <summary>
        /// Show appraisal countdown
        /// </summary>
        [JsonPropertyName("showAppraisalCountdown")]
        public decimal ShowAppraisalCountdown { get; set; }
        /// <summary>
        /// Automatic delivery
        /// </summary>
        [JsonPropertyName("automaticDelivery")]
        public decimal AutomaticDelivery { get; set; }
        /// <summary>
        /// Api allowed
        /// </summary>
        [JsonPropertyName("apiAllowed")]
        public bool ApiAllowed { get; set; }
    }


}
