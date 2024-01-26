using CryptoExchange.Net.Converters;
using Mexc.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Withdrawal info
    /// </summary>
    public class MexcWithdrawal
    {
        /// <summary>
        /// Withdrawal id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("txId")]
        public string? TransactionId { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Withdrawal address
        /// </summary>
        [JsonProperty("address")]
        public string? Address { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Transfer type
        /// </summary>
        [JsonProperty("transferType"), JsonConverter(typeof(EnumConverter))]
        public TranfserType TransferType { get; set; }
        /// <summary>
        /// Withdrawal status
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(EnumConverter))]
        public WithdrawStatus Status { get; set; }
        /// <summary>
        /// Transaction fee
        /// </summary>
        [JsonProperty("transactionFee")]
        public decimal TransactionFee { get; set; }
        /// <summary>
        /// Network confirmations
        /// </summary>
        [JsonProperty("confirmNo")]
        public int? Confirmations { get; set; }
        /// <summary>
        /// Aply time
        /// </summary>
        [JsonProperty("applyTime")]
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// Remarks
        /// </summary>
        [JsonProperty("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        [JsonProperty("memo")]
        public string? Memo { get; set; }
    }
}
