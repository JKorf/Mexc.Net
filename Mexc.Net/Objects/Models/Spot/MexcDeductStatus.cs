using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Mexc MX deduct status
    /// </summary>
    public record MexcDeductStatus
    {
        /// <summary>
        /// Is deduction enabled
        /// </summary>
        [JsonProperty("mxDeductEnable")]
        public bool MxDeductionEnable { get; set; }
    }
}
