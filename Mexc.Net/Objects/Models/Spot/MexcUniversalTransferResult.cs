namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Universal transfer result
    /// </summary>
    public record MexcUniversalTransferResult
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public string TransactionId { get; set; } = String.Empty;
    }
}
