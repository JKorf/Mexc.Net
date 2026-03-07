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
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fullName</c>"] The full base asset name
        /// </summary>
        [JsonPropertyName("fullName")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] The status of the symbol
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// ["<c>baseAsset</c>"] The base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseAssetPrecision</c>"] The precision of the base asset
        /// </summary>
        [JsonPropertyName("baseAssetPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>quoteAsset</c>"] The quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quotePrecision</c>"] The precision of the quote asset
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>quoteCommissionPrecision</c>"] Quote asset fee precision
        /// </summary>
        [JsonPropertyName("quoteCommissionPrecision")]
        public int QuoteAssetFeePrecision { get; set; }
        /// <summary>
        /// ["<c>baseCommissionPrecision</c>"] Base asset fee precision
        /// </summary>
        [JsonPropertyName("baseCommissionPrecision")]
        public int BaseAssetFeePrecision { get; set; }
        /// <summary>
        /// ["<c>orderTypes</c>"] Allowed order types
        /// </summary>
        [JsonPropertyName("orderTypes")]
        public OrderType[] OrderTypes { get; set; } = Array.Empty<OrderType>();
        /// <summary>
        /// ["<c>quoteOrderQtyMarketAllowed</c>"] Quote quantity market orders allowed
        /// </summary>
        [JsonPropertyName("quoteOrderQtyMarketAllowed")]
        public bool QuoteOrderQuantityMarketAllowed { get; set; }
        /// <summary>
        /// ["<c>isSpotTradingAllowed</c>"] Spot trading orders allowed
        /// </summary>
        [JsonPropertyName("isSpotTradingAllowed")]
        public bool IsSpotTradingAllowed { get; set; }
        /// <summary>
        /// ["<c>isMarginTradingAllowed</c>"] Margin trading orders allowed
        /// </summary>
        [JsonPropertyName("isMarginTradingAllowed")]
        public bool IsMarginTradingAllowed { get; set; }
        /// <summary>
        /// ["<c>quoteAmountPrecision</c>"] Quote quantity precision
        /// </summary>
        [JsonPropertyName("quoteAmountPrecision")]
        public decimal QuoteQuantityPrecision { get; set; }
        /// <summary>
        /// ["<c>baseSizePrecision</c>"] Quote quantity precision
        /// </summary>
        [JsonPropertyName("baseSizePrecision")]
        public decimal BaseQuantityPrecision { get; set; }

        /// <summary>
        /// ["<c>permissions</c>"] Permissions types
        /// </summary>
        [JsonPropertyName("permissions")]
        public string[] Permissions { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>maxQuoteAmount</c>"] Max quote quantity for a single order
        /// </summary>
        [JsonPropertyName("maxQuoteAmount")]
        public decimal MaxQuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>makerCommission</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// ["<c>takerCommission</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// ["<c>quoteAmountPrecisionMarket</c>"] Quote quantity precision for market orders
        /// </summary>
        [JsonPropertyName("quoteAmountPrecisionMarket")]
        public decimal QuoteQuantityPrecisionMarket { get; set; }
        /// <summary>
        /// ["<c>maxQuoteAmountMarket</c>"] Max quote quantity for a single market order
        /// </summary>
        [JsonPropertyName("maxQuoteAmountMarket")]
        public decimal MaxQuoteQuantityMarket { get; set; }
        /// <summary>
        /// ["<c>tradeSideType</c>"] The trade sides that are enabled
        /// </summary>
        [JsonPropertyName("tradeSideType")]
        public TradeSidesStatus TradeSidesEnabled { get; set; }
    }
}
