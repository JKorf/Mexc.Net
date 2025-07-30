using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Withdrawal id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public string? TransactionId { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Withdrawal address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Transfer type
        /// </summary>
        [JsonPropertyName("transferType")]
        public TransferType TransferType { get; set; }
        /// <summary>
        /// Withdrawal status
        /// </summary>
        [JsonPropertyName("status")]
        public WithdrawStatus Status { get; set; }
        /// <summary>
        /// Transaction fee
        /// </summary>
        [JsonPropertyName("transactionFee")]
        public decimal TransactionFee { get; set; }
        /// <summary>
        /// Network confirmations
        /// </summary>
        [JsonPropertyName("confirmNo")]
        public int? Confirmations { get; set; }
        /// <summary>
        /// Apply time
        /// </summary>
        [JsonPropertyName("applyTime")]
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// Remarks
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        [JsonPropertyName("memo")]
        public string? Memo { get; set; }
        /// <summary>
        /// Transaction hash
        /// </summary>
        [JsonPropertyName("transHash")]
        public string? TransactionHash { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Asset id
        /// </summary>
        [JsonPropertyName("coinId")]
        public string? AssetId { get; set; }
        /// <summary>
        /// CurrencyId
        /// </summary>
        [JsonPropertyName("vcoinId")]
        public string? CurrencyId { get; set; }
    }
}
