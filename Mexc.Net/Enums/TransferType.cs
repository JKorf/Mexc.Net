using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferType>))]
    public enum TransferType
    {
        /// <summary>
        /// Outside transfer
        /// </summary>
        [Map("0")]
        TransferOut,
        /// <summary>
        /// Internal transfer
        /// </summary>
        [Map("1")]
        TransferInternal
    }
}
