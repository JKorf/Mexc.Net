using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// KYC status
    /// </summary>
    [SerializationModel]
    public record MexcKycStatus
    {
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public KycStatus Status { get; set; }
    }
}
