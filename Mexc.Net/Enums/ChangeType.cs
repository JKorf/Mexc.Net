using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Change type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ChangeType>))]
    public enum ChangeType
    {
        /// <summary>
        /// Increase
        /// </summary>
        [Map("ADD")]
        Increase,
        /// <summary>
        /// Decrease
        /// </summary>
        [Map("SUB")]
        Decrease
    }
}
