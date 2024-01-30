using CryptoExchange.Net.Converters;
using Mexc.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Deposit info
    /// </summary>
    public class MexcDeposit
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
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
        /// Status
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(EnumConverter))]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Insert time
        /// </summary>
        [JsonProperty("insertTime")]
        public DateTime InsertTime { get; set; }
        /// <summary>
        /// Confirmations needed for unlocking the funds
        /// </summary>
        [JsonProperty("unlockConfirm")]
        public int UnlockConfirmations { get; set; }
        /// <summary>
        /// Current confirmations
        /// </summary>
        [JsonProperty("confirmTimes")]
        public int Confirmations { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        [JsonProperty("memo")]
        public string? Memo { get; set; }
    }
}
