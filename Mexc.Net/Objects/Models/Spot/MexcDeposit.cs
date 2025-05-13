using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Deposit info
    /// </summary>
    [SerializationModel]
    public record MexcDeposit
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
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
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Insert time
        /// </summary>
        [JsonPropertyName("insertTime")]
        public DateTime InsertTime { get; set; }
        /// <summary>
        /// Confirmations needed for unlocking the funds
        /// </summary>
        [JsonPropertyName("unlockConfirm")]
        public int UnlockConfirmations { get; set; }
        /// <summary>
        /// Current confirmations
        /// </summary>
        [JsonPropertyName("confirmTimes")]
        public int Confirmations { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        [JsonPropertyName("memo")]
        public string? Memo { get; set; }
    }
}
