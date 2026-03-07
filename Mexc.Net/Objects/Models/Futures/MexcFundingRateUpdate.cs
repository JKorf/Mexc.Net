namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public record MexcFundingRateUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal FundingRate { get; set; }        
    }
}
