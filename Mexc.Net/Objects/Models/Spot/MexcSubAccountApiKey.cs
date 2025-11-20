namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub account API key info
    /// </summary>
    public record MexcSubAccountApiKey
    {
        /// <summary>
        /// Sub account
        /// </summary>
        [JsonPropertyName("subAccount")]
        public string SubAccount { get; set; } = string.Empty;
        /// <summary>
        /// Note
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;
        /// <summary>
        /// API key
        /// </summary>
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>
        /// Secret key
        /// </summary>
        [JsonPropertyName("secretKey")]
        public string SecretKey { get; set; } = string.Empty;
        /// <summary>
        /// Key permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public string Permissions { get; set; } = string.Empty;
        /// <summary>
        /// IP whitelist
        /// </summary>
        [JsonPropertyName("ip")]
        public string Ip { get; set; } = string.Empty;
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
    }
}
