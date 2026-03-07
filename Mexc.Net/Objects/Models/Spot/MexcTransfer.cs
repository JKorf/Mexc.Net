using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Transfer info
    /// </summary>
    [SerializationModel]
    public record MexcTransfer
    {
        /// <summary>
        /// ["<c>tranId</c>"] Transfer id
        /// </summary>
        [JsonPropertyName("tranId")]
        public string TransferId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientTranId</c>"] Client transfer id
        /// </summary>
        [JsonPropertyName("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;
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
        /// ["<c>fromAccountType</c>"] From account type
        /// </summary>
        [JsonPropertyName("fromAccountType")]
        public AccountType FromAccountType { get; set; }
        /// <summary>
        /// ["<c>toAccountType</c>"] To account type
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public AccountType ToAccountType { get; set; }
        /// <summary>
        /// ["<c>fromSymbol</c>"] From symbol
        /// </summary>
        [JsonPropertyName("fromSymbol")]
        public string? FromSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>toSymbol</c>"] To symbol
        /// </summary>
        [JsonPropertyName("toSymbol")]
        public string? ToSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
