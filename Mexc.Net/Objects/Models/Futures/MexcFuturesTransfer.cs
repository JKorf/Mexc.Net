using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Transfer page
    /// </summary>
    public record MexcFuturesTransferPage
    {
        /// <summary>
        /// ["<c>pageSize</c>"] Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// ["<c>totalCount</c>"] Total count
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }
        /// <summary>
        /// ["<c>totalPage</c>"] Total pages
        /// </summary>
        [JsonPropertyName("totalPage")]
        public int TotalPages { get; set; }
        /// <summary>
        /// ["<c>currentPage</c>"] Current page
        /// </summary>
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>resultList</c>"] Data
        /// </summary>
        [JsonPropertyName("resultList")]
        public MexcFuturesTransfer[] Data { get; set; } = [];
    }

    /// <summary>
    /// Transfer info
    /// </summary>
    public record MexcFuturesTransfer
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>txid</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("txid")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Transfer direction
        /// </summary>
        [JsonPropertyName("type")]
        public TransferDirection Direction { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public TransferStatus Status { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }


}
