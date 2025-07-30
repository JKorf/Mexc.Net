using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Bool
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesBool>))]
    public enum FuturesBool
    {
        /// <summary>
        /// Yes
        /// </summary>
        [Map("1")]
        Yes,
        /// <summary>
        /// No
        /// </summary>
        [Map("2")]
        No
    }
}
