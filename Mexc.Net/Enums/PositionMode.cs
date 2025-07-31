using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionMode>))]
    public enum PositionMode
    {
        /// <summary>
        /// Hedge mode
        /// </summary>
        [Map("1")]
        Hedge,
        /// <summary>
        /// One way mode
        /// </summary>
        [Map("2")]
        OneWay
    }
}
