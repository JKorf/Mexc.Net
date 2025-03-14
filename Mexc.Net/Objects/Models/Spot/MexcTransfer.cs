using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Transfer id
        /// </summary>
        [JsonPropertyName("tranId")]
        public string TransferId { get; set; } = string.Empty;
        /// <summary>
        /// Client transfer id
        /// </summary>
        [JsonPropertyName("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;
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
        /// From account type
        /// </summary>
        [JsonPropertyName("fromAccountType")]
        public AccountType FromAccountType { get; set; }
        /// <summary>
        /// To account type
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public AccountType ToAccountType { get; set; }
        /// <summary>
        /// From symbol
        /// </summary>
        [JsonPropertyName("fromSymbol")]
        public string? FromSymbol { get; set; } = string.Empty;
        /// <summary>
        /// To symbol
        /// </summary>
        [JsonPropertyName("toSymbol")]
        public string? ToSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
