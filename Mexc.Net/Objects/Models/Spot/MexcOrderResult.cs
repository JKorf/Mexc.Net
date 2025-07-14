using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order result
    /// </summary>
    public record MexcOrderResult
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string? OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [JsonInclude, JsonPropertyName("msg")]
        internal string? ErrorMessage { get; set; }
        /// <summary>
        /// Error code
        /// </summary>
        [JsonInclude, JsonPropertyName("code")]
        internal int? ErrorCode { get; set; }
    }
}
