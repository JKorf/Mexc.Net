using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Rebate info
    /// </summary>
    public record MexcRebate
    {
        /// <summary>
        /// ["<c>spot</c>"] Spot
        /// </summary>
        [JsonPropertyName("spot")]
        public decimal Spot { get; set; }
        /// <summary>
        /// ["<c>futures</c>"] Futures
        /// </summary>
        [JsonPropertyName("futures")]
        public decimal Futures { get; set; }
        /// <summary>
        /// ["<c>total</c>"] Total
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// ["<c>uid</c>"] User id
        /// </summary>
        [JsonPropertyName("uid")]
        public string Uid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>account</c>"] Account
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>inviteTime</c>"] Invite time
        /// </summary>
        [JsonPropertyName("inviteTime")]
        public DateTime InviteTime { get; set; }
    }
}
