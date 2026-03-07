namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub accounts info
    /// </summary>
    internal record MexcSubUserAccounts
    {
        /// <summary>
        /// ["<c>subAccounts</c>"] Sub accounts
        /// </summary>
        [JsonPropertyName("subAccounts")]
        public MexcSubUserAccount[] SubAccounts { get; set; } = [];
    }

    /// <summary>
    /// Sub user account info
    /// </summary>
    public record MexcSubUserAccount
    {
        /// <summary>
        /// ["<c>subAccount</c>"] Sub account name
        /// </summary>
        [JsonPropertyName("subAccount")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>isFreeze</c>"] Is frozen
        /// </summary>
        [JsonPropertyName("isFreeze")]
        public bool IsFreeze { get; set; }

        /// <summary>
        /// ["<c>createTime</c>"] Creation time
        /// </summary>
        [JsonPropertyName("createTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ["<c>uid</c>"] Account id
        /// </summary>
        [JsonPropertyName("uid")]
        public long AccountId { get; set; }
    }
}
