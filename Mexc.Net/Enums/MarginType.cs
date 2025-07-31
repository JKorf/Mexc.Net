using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Margin type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginType>))]
    public enum MarginType
    {
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("1")]
        Isolated,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("2")]
        Cross
    }
}
