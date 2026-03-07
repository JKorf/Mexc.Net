namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Deposit address
    /// </summary>
    [SerializationModel]
    public  record MexcDepositAddress
    {
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tag</c>"] Tag
        /// </summary>
        [JsonPropertyName("tag")]
        public string? Tag { get; set; }
        /// <summary>
        /// ["<c>memo</c>"] Memo
        /// </summary>
        [JsonPropertyName("memo")]
        public string? Memo { get; set; }
    }
}
