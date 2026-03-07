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
    }


}
