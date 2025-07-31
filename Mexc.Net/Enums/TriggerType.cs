using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Trigger type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerType>))]
    public enum TriggerType
    {
        /// <summary>
        /// More than or equal
        /// </summary>
        [Map("1")]
        MoreThanOrEqual,
        /// <summary>
        /// Less than or equal
        /// </summary>
        [Map("2")]
        LessThanOrEqual
    }
}
