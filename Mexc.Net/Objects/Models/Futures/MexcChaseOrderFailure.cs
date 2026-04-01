using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Chase order failure
    /// </summary>
    public record MexcChaseOrderFailure
    {
        /// <summary>
        /// ["<c>ec</c>"] Error code
        /// </summary>
        [JsonPropertyName("ec")]
        public int ErrorCode { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
    }
}
