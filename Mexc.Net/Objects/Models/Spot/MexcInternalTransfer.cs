using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Transfer info
    /// </summary>
    [SerializationModel]
    public record MexcInternalTransfer
    {
        /// <summary>
        /// Transfer id
        /// </summary>
        [JsonPropertyName("tranId")]
        public string TransferId { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// To account type
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public TransferAccountType TransferAccountType { get; set; }
        /// <summary>
        /// To account identifier
        /// </summary>
        [JsonPropertyName("toAccount")]
        public string ToAccount { get; set; } = string.Empty;
        /// <summary>
        /// From account identifier
        /// </summary>
        [JsonPropertyName("fromAccount")]
        public string FromAccount { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string TransferStatus { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }


}
