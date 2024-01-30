using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Mexc MX deduct status
    /// </summary>
    public class MexcDeductStatus
    {
        /// <summary>
        /// Is deduction enabled
        /// </summary>
        [JsonProperty("mxDeductEnable")]
        public bool MxDeductionEnable { get; set; }
    }
}
