namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Balance info
    /// </summary>
    public record MexcFuturesBalance
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>displayCurrency</c>"] Display asset
        /// </summary>
        [JsonPropertyName("displayCurrency")]
        public string DisplayAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionMargin</c>"] Position margin
        /// </summary>
        [JsonPropertyName("positionMargin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// ["<c>cashBalance</c>"] Cash balance
        /// </summary>
        [JsonPropertyName("cashBalance")]
        public decimal CashBalance { get; set; }
        /// <summary>
        /// ["<c>frozenBalance</c>"] Frozen balance
        /// </summary>
        [JsonPropertyName("frozenBalance")]
        public decimal FrozenBalance { get; set; }
        /// <summary>
        /// ["<c>equity</c>"] Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// ["<c>unrealized</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>bonus</c>"] Bonus
        /// </summary>
        [JsonPropertyName("bonus")]
        public decimal Bonus { get; set; }
        /// <summary>
        /// ["<c>availableCash</c>"] Available cash
        /// </summary>
        [JsonPropertyName("availableCash")]
        public decimal? AvailableCash { get; set; }
        /// <summary>
        /// ["<c>availableOpen</c>"] Available open
        /// </summary>
        [JsonPropertyName("availableOpen")]
        public decimal? AvailableOpen { get; set; }
        /// <summary>
        /// ["<c>debtAmount</c>"] Debt
        /// </summary>
        [JsonPropertyName("debtAmount")]
        public decimal? Debt { get; set; }
        /// <summary>
        /// ["<c>contributeMarginAmount</c>"] Contribute margin quantity
        /// </summary>
        [JsonPropertyName("contributeMarginAmount")]
        public decimal? ContributeMarginQuantity { get; set; }
        /// <summary>
        /// ["<c>vcoinId</c>"] Asset id
        /// </summary>
        [JsonPropertyName("vcoinId")]
        public string? AssetId { get; set; }
    }
}
