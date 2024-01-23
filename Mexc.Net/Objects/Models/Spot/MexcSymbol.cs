using Mexc.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbol info
    /// </summary>
    public class MexcSymbol
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The full base asset name
        /// </summary>
        [JsonProperty("fullName")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonProperty("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the base asset
        /// </summary>
        [JsonProperty("baseAssetPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// The quote asset
        /// </summary>
        [JsonProperty("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the quote asset
        /// </summary>
        [JsonProperty("quotePrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// Quote asset commission precision
        /// </summary>
        [JsonProperty("quoteCommissionPrecision")]
        public int QuoteAssetCommissionPrecision { get; set; }
        /// <summary>
        /// Base asset commission precision
        /// </summary>
        [JsonProperty("baseCommissionPrecision")]
        public int BaseAssetCommissionPrecision { get; set; }
        /// <summary>
        /// Allowed order types
        /// </summary>
        [JsonProperty("orderTypes")]
        public IEnumerable<SpotOrderType> OrderTypes { get; set; } = Array.Empty<SpotOrderType>();
        /// <summary>
        /// Quote quantity market orders allowed
        /// </summary>
        [JsonProperty("quoteOrderQtyMarketAllowed")]
        public bool QuoteOrderQuantityMarketAllowed { get; set; }
        /// <summary>
        /// Spot trading orders allowed
        /// </summary>
        [JsonProperty("isSpotTradingAllowed")]
        public bool IsSpotTradingAllowed { get; set; }
        /// <summary>
        /// Margin trading orders allowed
        /// </summary>
        [JsonProperty("isMarginTradingAllowed")]
        public bool IsMarginTradingAllowed { get; set; }
        /// <summary>
        /// Quote quantity precision
        /// </summary>
        [JsonProperty("quoteAmountPrecision")]
        public decimal QuoteQuantityPrecision { get; set; }
        /// <summary>
        /// Quote quantity precision
        /// </summary>
        [JsonProperty("baseSizePrecision")]
        public decimal BaseQuantityPrecision { get; set; }

        /// <summary>
        /// Permissions types
        /// </summary>
        public IEnumerable<string> Permissions { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Max quote quantity for a single order
        /// </summary>
        [JsonProperty("maxQuoteAmount")]
        public decimal MaxQuoteQuantity { get; set; }
        /// <summary>
        /// Maker commission rate
        /// </summary>
        [JsonProperty("makerCommission")]
        public decimal MakerCommission { get; set; }
        /// <summary>
        /// Taker commission rate
        /// </summary>
        [JsonProperty("takerCommission")]
        public decimal TakerCommission { get; set; }
        /// <summary>
        /// Quote quantity precision for market orders
        /// </summary>
        [JsonProperty("quoteAmountPrecisionMarket")]
        public decimal QuoteQuantityPrecisionMarket { get; set; }
        /// <summary>
        /// Max quote quantity for a single market order
        /// </summary>
        [JsonProperty("maxQuoteAmountMarket")]
        public decimal MaxQuoteQuantityMarket { get; set; }

    }
}
