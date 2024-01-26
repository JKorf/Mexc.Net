using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Transfer id
    /// </summary>
    public class MexcTransferId
    {
        /// <summary>
        /// Transfer id
        /// </summary>
        [JsonProperty("tranId")]
        public string TransferId { get; set; } = string.Empty;
    }
}
