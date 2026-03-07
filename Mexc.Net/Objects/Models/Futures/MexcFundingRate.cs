namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public record MexcFundingRate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fundingRate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>maxFundingRate</c>"] Max funding rate
        /// </summary>
        [JsonPropertyName("maxFundingRate")]
        public decimal MaxFundingRate { get; set; }
        /// <summary>
        /// ["<c>minFundingRate</c>"] Min funding rate
        /// </summary>
        [JsonPropertyName("minFundingRate")]
        public decimal MinFundingRate { get; set; }
        /// <summary>
        /// ["<c>collectCycle</c>"] Interval in hours
        /// </summary>
        [JsonPropertyName("collectCycle")]
        public decimal Interval { get; set; }
        /// <summary>
        /// ["<c>nextSettleTime</c>"] Next settle time
        /// </summary>
        [JsonPropertyName("nextSettleTime")]
        public DateTime? NextSettleTime { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
