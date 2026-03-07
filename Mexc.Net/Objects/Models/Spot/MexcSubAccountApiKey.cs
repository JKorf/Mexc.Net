namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub account API key info
    /// </summary>
    public record MexcSubAccountApiKey
    {
        /// <summary>
        /// ["<c>subAccount</c>"] Sub account
        /// </summary>
        [JsonPropertyName("subAccount")]
        public string SubAccount { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>note</c>"] Note
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>apiKey</c>"] API key
        /// </summary>
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>secretKey</c>"] Secret key
        /// </summary>
        [JsonPropertyName("secretKey")]
        public string SecretKey { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>permissions</c>"] Key permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public string Permissions { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ip</c>"] IP whitelist
        /// </summary>
        [JsonPropertyName("ip")]
        public string Ip { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>createTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
    }
}
