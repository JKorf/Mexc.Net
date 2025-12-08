namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public record MexcFundingRate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Max funding rate
        /// </summary>
        [JsonPropertyName("maxFundingRate")]
        public decimal MaxFundingRate { get; set; }
        /// <summary>
        /// Min funding rate
        /// </summary>
        [JsonPropertyName("minFundingRate")]
        public decimal MinFundingRate { get; set; }
        /// <summary>
        /// Interval in hours
        /// </summary>
        [JsonPropertyName("collectCycle")]
        public decimal Interval { get; set; }
        /// <summary>
        /// Next settle time
        /// </summary>
        [JsonPropertyName("nextSettleTime")]
        public DateTime? NextSettleTime { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
