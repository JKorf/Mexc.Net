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
        /// ["<c>tranId</c>"] Transfer id
        /// </summary>
        [JsonPropertyName("tranId")]
        public string TransferId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>toAccountType</c>"] To account type
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public TransferAccountType TransferAccountType { get; set; }
        /// <summary>
        /// ["<c>toAccount</c>"] To account identifier
        /// </summary>
        [JsonPropertyName("toAccount")]
        public string ToAccount { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fromAccount</c>"] From account identifier
        /// </summary>
        [JsonPropertyName("fromAccount")]
        public string FromAccount { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string TransferStatus { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }


}
