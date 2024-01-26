using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Dust transfer result
    /// </summary>
    public class MexcDustResult
    {
        /// <summary>
        /// Successfully converted
        /// </summary>
        [JsonProperty("successList")]
        public IEnumerable<string> Successful { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Failed to convert
        /// </summary>
        [JsonProperty("failedList")]
        public IEnumerable<MexcFailedDust> Failed { get; set; } = Array.Empty<MexcFailedDust>();
        /// <summary>
        /// Total converted
        /// </summary>
        [JsonProperty("totalConvert")]
        public decimal TotalConverted { get; set; }
        /// <summary>
        /// Convert fee
        /// </summary>
        [JsonProperty("convertFee")]
        public decimal ConvertFee { get; set; }
    }

    /// <summary>
    /// Failed dust asset
    /// </summary>
    public class MexcFailedDust
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Code
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
