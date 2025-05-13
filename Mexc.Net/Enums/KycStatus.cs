using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// KYC status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KycStatus>))]
    public enum KycStatus
    {
        /// <summary>
        /// Unverified
        /// </summary>
        [Map("1")]
        Unverified,
        /// <summary>
        /// Primary
        /// </summary>
        [Map("2")]
        Primary,
        /// <summary>
        /// Advanced
        /// </summary>
        [Map("3")]
        Advanced,
        /// <summary>
        /// Institutional
        /// </summary>
        [Map("4")]
        Institutional
    }
}
