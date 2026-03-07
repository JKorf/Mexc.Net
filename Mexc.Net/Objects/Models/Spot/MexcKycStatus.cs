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
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public KycStatus Status { get; set; }
    }
}
