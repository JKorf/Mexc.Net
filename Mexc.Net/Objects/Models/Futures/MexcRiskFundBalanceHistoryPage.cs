namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Risk fund balance history page
    /// </summary>
    public record MexcRiskFundBalanceHistoryPage
    {
        /// <summary>
        /// ["<c>pageSize</c>"] Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public decimal PageSize { get; set; }
        /// <summary>
        /// ["<c>totalCount</c>"] Total number of results
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
        public MexcRiskFundBalanceHistory[] Data { get; set; } = [];
    }

    /// <summary>
    /// Risk fund balance item
    /// </summary>
    public record MexcRiskFundBalanceHistory
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>snapshotTime</c>"] Snapshot time
        /// </summary>
        [JsonPropertyName("snapshotTime")]
        public DateTime SnapshotTime { get; set; }
    }


}
