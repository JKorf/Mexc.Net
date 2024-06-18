using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Deposit address
    /// </summary>
    public  record MexcDepositAddress
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Tag
        /// </summary>
        [JsonProperty("tag")]
        public string? Tag { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        [JsonProperty("memo")]
        public string? Memo { get; set; }
    }
}
