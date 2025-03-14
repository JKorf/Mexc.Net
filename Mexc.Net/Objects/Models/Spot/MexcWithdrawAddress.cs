using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Withdraw address
    /// </summary>
    [SerializationModel]
    public record MexcWithdrawAddress
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Tag
        /// </summary>
        [JsonPropertyName("addressTag")]
        public string? Tag { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        [JsonPropertyName("memo")]
        public string? Memo { get; set; }
    }
}
