using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Position status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionStatus>))]
    public enum PositionStatus
    {
        /// <summary>
        /// Holding
        /// </summary>
        [Map("1")]
        Holding,
        /// <summary>
        /// System auto holding
        /// </summary>
        [Map("2")]
        SystemAutoHolding,
        /// <summary>
        /// Closed
        /// </summary>
        [Map("3")]
        Closed
    }
}
