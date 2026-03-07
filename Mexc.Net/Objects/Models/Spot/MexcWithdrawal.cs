using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Withdrawal info
    /// </summary>
    [SerializationModel]
    public record MexcWithdrawal
    {
        /// <summary>
        /// ["<c>id</c>"] Withdrawal id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public string? TransactionId { get; set; }
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>address</c>"] Withdrawal address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>transferType</c>"] Transfer type
        /// </summary>
        [JsonPropertyName("transferType")]
        public TransferType TransferType { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Withdrawal status
        /// </summary>
        [JsonPropertyName("status")]
        public WithdrawStatus Status { get; set; }
        /// <summary>
        /// ["<c>transactionFee</c>"] Transaction fee
        /// </summary>
        [JsonPropertyName("transactionFee")]
        public decimal TransactionFee { get; set; }
        /// <summary>
        /// ["<c>confirmNo</c>"] Network confirmations
        /// </summary>
        [JsonPropertyName("confirmNo")]
        public int? Confirmations { get; set; }
        /// <summary>
        /// ["<c>applyTime</c>"] Apply time
        /// </summary>
        [JsonPropertyName("applyTime")]
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// ["<c>remark</c>"] Remarks
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// ["<c>memo</c>"] Memo
        /// </summary>
        [JsonPropertyName("memo")]
        public string? Memo { get; set; }
        /// <summary>
        /// ["<c>transHash</c>"] Transaction hash
        /// </summary>
        [JsonPropertyName("transHash")]
        public string? TransactionHash { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Last update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>coinId</c>"] Asset id
        /// </summary>
        [JsonPropertyName("coinId")]
        public string? AssetId { get; set; }
        /// <summary>
        /// ["<c>vcoinId</c>"] CurrencyId
        /// </summary>
        [JsonPropertyName("vcoinId")]
        public string? CurrencyId { get; set; }
    }
}
