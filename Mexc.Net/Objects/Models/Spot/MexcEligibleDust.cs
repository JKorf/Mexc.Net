namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Eligible dust asset
    /// </summary>
    [SerializationModel]
    public record MexcEligibleDust
    {
        /// <summary>
        /// ["<c>convertMx</c>"] Resulting Mx
        /// </summary>
        [JsonPropertyName("convertMx")]
        public decimal ConvertMx { get; set; }
        /// <summary>
        /// ["<c>convertUsdt</c>"] Dust worth
        /// </summary>
        [JsonPropertyName("convertUsdt")]
        public decimal ConvertUsdt { get; set; }
        /// <summary>
        /// ["<c>balance</c>"] Current balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] Asset name
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>code</c>"] Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>message</c>"] Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
