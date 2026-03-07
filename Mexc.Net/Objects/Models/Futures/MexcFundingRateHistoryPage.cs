namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding history page
    /// </summary>
    public record MexcFundingRateHistoryPage
    {
        /// <summary>
        /// ["<c>pageSize</c>"] Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public decimal PageSize { get; set; }
        /// <summary>
        /// ["<c>totalCount</c>"] Total count
        /// </summary>
        [JsonPropertyName("totalCount")]
        public decimal TotalCount { get; set; }
        /// <summary>
        /// ["<c>totalPage</c>"] Total pages
        /// </summary>
        [JsonPropertyName("totalPage")]
        public decimal TotalPages { get; set; }
        /// <summary>
        /// ["<c>currentPage</c>"] Current page
        /// </summary>
        [JsonPropertyName("currentPage")]
        public decimal CurrentPage { get; set; }
        /// <summary>
        /// ["<c>resultList</c>"] Data
        /// </summary>
        [JsonPropertyName("resultList")]
        public MexcFundingRateHistory[] Data { get; set; } = [];
    }

    /// <summary>
    /// Funding rate
    /// </summary>
    public record MexcFundingRateHistory
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
        /// ["<c>settleTime</c>"] Settle time
        /// </summary>
        [JsonPropertyName("settleTime")]
        public DateTime SettleTime { get; set; }
    }


}
