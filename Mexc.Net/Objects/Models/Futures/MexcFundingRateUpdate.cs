namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public record MexcFundingRateUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal FundingRate { get; set; }        
    }
}
