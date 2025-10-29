using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Paged universal transfer results
    /// </summary>
    public class MexcUniversalTransferPaged
    {
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Transfers data
        /// </summary>
        [JsonPropertyName("result")]
        public MexcUniversalTransfer[] Data { get; set; } = [];
    }

    /// <summary>
    /// Universal transfer info
    /// </summary>
    public record MexcUniversalTransfer
    {
        /// <summary>
        /// Transfer id
        /// </summary>
        [JsonPropertyName("tranId")]
        public string TransferId { get; set; } = string.Empty;

        /// <summary>
        /// Name of the origin account
        /// </summary>
        [JsonPropertyName("fromAccount")]
        public string FromAccount { get; set; } = string.Empty;

        /// <summary>
        /// Name of the destination account
        /// </summary>
        [JsonPropertyName("toAccount")]
        public string ToAccount { get; set; } = string.Empty;

        /// <summary>
        /// Client transfer id
        /// </summary>
        [JsonPropertyName("clientTranId")]
        public string? ClientTransferId { get; set; } = string.Empty;

        /// <summary>
        /// Asset that was transferred
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Amount transferred
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Type of the origin account
        /// </summary>
        [JsonPropertyName("fromAccountType")]
        public AccountType FromAccountType { get; set; }

        /// <summary>
        /// Type of the destination account
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public AccountType ToAccountType { get; set; }

        /// <summary>
        /// Status of the transfer
        /// </summary>
        [JsonPropertyName("status")]
        public TransferStatus Status { get; set; }

        /// <summary>
        /// Timestamp of the transfer
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
