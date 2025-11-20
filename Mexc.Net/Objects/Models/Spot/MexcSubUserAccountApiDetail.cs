namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub user account api details
    /// </summary>
    public record MexcSubUserAccountApiDetails
    {
        /// <summary>
        /// API keys details
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
        /// Note
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;

        /// <summary>
        /// Api key
        /// </summary>
        [JsonPropertyName("apikey")]
        public string ApiKey { get; set; } = string.Empty;
        [JsonInclude, JsonPropertyName("apiKey")]
        internal string ApiKeyInt { set => ApiKey = value; }

        /// <summary>
        /// IP whitelist
        /// </summary>
        [JsonPropertyName("ip")]
        public string IpAddresses { get; set; } = string.Empty;

        /// <summary>
        /// Permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public string Permissions { get; set; } = string.Empty;

        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
    }
}
