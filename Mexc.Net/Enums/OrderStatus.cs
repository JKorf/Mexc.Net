using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// New
        /// </summary>
        [Map("NEW", "1")]
        New,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("FILLED", "2")]
        Filled,
        /// <summary>
        /// Partially filled
        /// </summary>
        [Map("PARTIALLY_FILLED", "3")]
        PartiallyFilled,
        /// <summary>
        /// Fully canceled
        /// </summary>
        [Map("CANCELED", "4")]
        Canceled,
        /// <summary>
        /// Partially filled, partially canceled
        /// </summary>
        [Map("PARTIALLY_CANCELED", "5")]
        PartiallyCanceled
    }
}
