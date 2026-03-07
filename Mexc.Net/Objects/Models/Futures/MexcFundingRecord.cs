using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding record page
    /// </summary>
    public record MexcFundingRecordPage
    {
        /// <summary>
        /// ["<c>pageSize</c>"] Page quantity
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageQuantity { get; set; }
        /// <summary>
        /// ["<c>totalCount</c>"] Total count
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }
        /// <summary>
        /// ["<c>totalPage</c>"] Total page
        /// </summary>
        [JsonPropertyName("totalPage")]
        public int TotalPage { get; set; }
        /// <summary>
        /// ["<c>currentPage</c>"] Current page
        /// </summary>
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>resultList</c>"] Data
        /// </summary>
        [JsonPropertyName("resultList")]
        public MexcFundingRecord[] Data { get; set; } = [];
    }

    /// <summary>
    /// Funding record
    /// </summary>
    public record MexcFundingRecord
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionType</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>positionValue</c>"] Position value
        /// </summary>
        [JsonPropertyName("positionValue")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// ["<c>funding</c>"] Funding
        /// </summary>
        [JsonPropertyName("funding")]
        public decimal Funding { get; set; }
        /// <summary>
        /// ["<c>rate</c>"] Rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }
        /// <summary>
        /// ["<c>settleTime</c>"] Settle time
        /// </summary>
        [JsonPropertyName("settleTime")]
        public DateTime SettleTime { get; set; }
    }


}
