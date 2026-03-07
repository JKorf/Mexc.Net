using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Paged universal transfer results
    /// </summary>
    public record MexcUniversalTransferPaged
    {
        /// <summary>
        /// ["<c>totalCount</c>"] Total pages
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int TotalPages { get; set; }

        /// <summary>
        /// ["<c>result</c>"] Transfers data
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
        /// ["<c>tranId</c>"] Transfer id
        /// </summary>
        [JsonPropertyName("tranId")]
        public string TransferId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>fromAccount</c>"] Name of the origin account
        /// </summary>
        [JsonPropertyName("fromAccount")]
        public string FromAccount { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>toAccount</c>"] Name of the destination account
        /// </summary>
        [JsonPropertyName("toAccount")]
        public string ToAccount { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>clientTranId</c>"] Client transfer id
        /// </summary>
        [JsonPropertyName("clientTranId")]
        public string? ClientTransferId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>asset</c>"] Asset that was transferred
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>amount</c>"] Amount transferred
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// ["<c>fromAccountType</c>"] Type of the origin account
        /// </summary>
        [JsonPropertyName("fromAccountType")]
        public AccountType FromAccountType { get; set; }

        /// <summary>
        /// ["<c>toAccountType</c>"] Type of the destination account
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public AccountType ToAccountType { get; set; }

        /// <summary>
        /// ["<c>status</c>"] Status of the transfer
        /// </summary>
        [JsonPropertyName("status")]
        public TransferStatus Status { get; set; }

        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp of the transfer
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
