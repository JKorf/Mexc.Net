using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbol info
    /// </summary>
    [SerializationModel]
    public record MexcSymbol
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The full base asset name
        /// </summary>
        [JsonPropertyName("fullName")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the base asset
        /// </summary>
        [JsonPropertyName("baseAssetPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// The quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the quote asset
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// Quote asset fee precision
        /// </summary>
        [JsonPropertyName("quoteCommissionPrecision")]
        public int QuoteAssetFeePrecision { get; set; }
        /// <summary>
        /// Base asset fee precision
        /// </summary>
        [JsonPropertyName("baseCommissionPrecision")]
        public int BaseAssetFeePrecision { get; set; }
        /// <summary>
        /// Allowed order types
        /// </summary>
        [JsonPropertyName("orderTypes")]
        public OrderType[] OrderTypes { get; set; } = Array.Empty<OrderType>();
        /// <summary>
        /// Quote quantity market orders allowed
        /// </summary>
        [JsonPropertyName("quoteOrderQtyMarketAllowed")]
        public bool QuoteOrderQuantityMarketAllowed { get; set; }
        /// <summary>
        /// Spot trading orders allowed
        /// </summary>
        [JsonPropertyName("isSpotTradingAllowed")]
        public bool IsSpotTradingAllowed { get; set; }
        /// <summary>
        /// Margin trading orders allowed
        /// </summary>
        [JsonPropertyName("isMarginTradingAllowed")]
        public bool IsMarginTradingAllowed { get; set; }
        /// <summary>
        /// Quote quantity precision
        /// </summary>
        [JsonPropertyName("quoteAmountPrecision")]
        public decimal QuoteQuantityPrecision { get; set; }
        /// <summary>
        /// Quote quantity precision
        /// </summary>
        [JsonPropertyName("baseSizePrecision")]
        public decimal BaseQuantityPrecision { get; set; }

        /// <summary>
        /// Permissions types
        /// </summary>
        [JsonPropertyName("permissions")]
        public string[] Permissions { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Max quote quantity for a single order
        /// </summary>
        [JsonPropertyName("maxQuoteAmount")]
        public decimal MaxQuoteQuantity { get; set; }
        /// <summary>
        /// Maker fee rate
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// Quote quantity precision for market orders
        /// </summary>
        [JsonPropertyName("quoteAmountPrecisionMarket")]
        public decimal QuoteQuantityPrecisionMarket { get; set; }
        /// <summary>
        /// Max quote quantity for a single market order
        /// </summary>
        [JsonPropertyName("maxQuoteAmountMarket")]
        public decimal MaxQuoteQuantityMarket { get; set; }
        /// <summary>
        /// The trade sides that are enabled
        /// </summary>
        [JsonPropertyName("tradeSideType")]
        public TradeSidesStatus TradeSidesEnabled { get; set; }
    }
}
