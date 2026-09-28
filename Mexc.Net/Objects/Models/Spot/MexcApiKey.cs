using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Api key info
    /// </summary>
    public record MexcApiKey
    {
        /// <summary>
        /// ["<c>note</c>"] Note
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>accessKey</c>"] API key
        /// </summary>
        [JsonPropertyName("accessKey")]
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>permissions</c>"] Permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        [JsonConverter(typeof(CommaSplitStringConverter))]
        public string[] Permissions { get; set; } = [];
        /// <summary>
        /// ["<c>ipWhiteList</c>"] IP whitelist
        /// </summary>
        [JsonPropertyName("ipWhiteList")]
        public string IpWhiteList { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>createTime</c>"] Create timestamp
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>remainingValidity</c>"] Remaining valid days
        /// </summary>
        [JsonPropertyName("remainingValidity")]
        public int RemaingingValidity { get; set; }
    }
}
