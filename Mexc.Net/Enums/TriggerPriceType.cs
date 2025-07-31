using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Trigger price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerPriceType>))]
    public enum TriggerPriceType
    {
        /// <summary>
        /// Last price
        /// </summary>
        [Map("1")]
        LastPrice,
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("2")]
        MarkPrice,
        /// <summary>
        /// Index price
        /// </summary>
        [Map("3")]
        IndexPrice
    }
}
