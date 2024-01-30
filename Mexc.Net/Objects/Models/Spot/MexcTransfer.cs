using Mexc.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Transfer info
    /// </summary>
    public class MexcTransfer
    {
        /// <summary>
        /// Transfer id
        /// </summary>
        [JsonProperty("tranId")]
        public string TransferId { get; set; } = string.Empty;
        /// <summary>
        /// Client transfer id
        /// </summary>
        [JsonProperty("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// From account type
        /// </summary>
        [JsonProperty("fromAccountType")]
        public AccountType FromAccountType { get; set; }
        /// <summary>
        /// To account type
        /// </summary>
        [JsonProperty("toAccountType")]
        public AccountType ToAccountType { get; set; }
        /// <summary>
        /// From symbol
        /// </summary>
        [JsonProperty("fromSymbol")]
        public string? FromSymbol { get; set; } = string.Empty;
        /// <summary>
        /// To symbol
        /// </summary>
        [JsonProperty("toSymbol")]
        public string? ToSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
