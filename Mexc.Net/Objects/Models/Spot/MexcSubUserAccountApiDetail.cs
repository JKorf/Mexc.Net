namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub user account api details
    /// </summary>
    public record MexcSubUserAccountApiDetails
    {
        /// <summary>
        /// ["<c>subAccount</c>"] API keys details
        /// </summary>
        [JsonPropertyName("subAccount")]
        public MexcSubUserAccountApiDetail[] Keys { get; set; } = [];
    }

    /// <summary>
    /// Sub user account api detail
    /// </summary>
    public record MexcSubUserAccountApiDetail
    {
        /// <summary>
        /// ["<c>note</c>"] Note
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>apikey</c>"] Api key
        /// </summary>
        [JsonPropertyName("apikey")]
        public string ApiKey { get; set; } = string.Empty;
        [JsonInclude, JsonPropertyName("apiKey")]
        internal string ApiKeyInt { set => ApiKey = value; }

        /// <summary>
        /// ["<c>ip</c>"] IP whitelist
        /// </summary>
        [JsonPropertyName("ip")]
        public string IpAddresses { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>permissions</c>"] Permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public string Permissions { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>createTime</c>"] Creation time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
    }
}
