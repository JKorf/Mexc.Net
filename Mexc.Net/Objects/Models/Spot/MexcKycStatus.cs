using Mexc.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// KYC status
    /// </summary>
    public record MexcKycStatus
    {
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public KycStatus Status { get; set; }
    }
}
