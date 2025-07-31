using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesOrderStatus>))]
    public enum FuturesOrderStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        [Map("1", "2")]
        Open,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("3")]
        Filled,
        /// <summary>
        /// Fully canceled
        /// </summary>
        [Map("4")]
        Canceled,
        /// <summary>
        /// Invalid
        /// </summary>
        [Map("5")]
        Invalid
    }
}
