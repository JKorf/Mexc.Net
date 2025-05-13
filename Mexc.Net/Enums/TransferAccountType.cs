using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Transfer account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferAccountType>))]
    public enum TransferAccountType
    {
        /// <summary>
        /// Email
        /// </summary>
        [Map("EMAIL")]
        Email,
        /// <summary>
        /// User id
        /// </summary>
        [Map("UID")]
        UID,
        /// <summary>
        /// Mobile number
        /// </summary>
        [Map("MOBILE")]
        Mobile
    }
}
