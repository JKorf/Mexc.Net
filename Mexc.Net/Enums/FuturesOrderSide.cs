using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesOrderSide>))]
    public enum FuturesOrderSide
    {
        /// <summary>
        /// Open long
        /// </summary>
        [Map("1")]
        OpenLong,
        /// <summary>
        /// Close short
        /// </summary>
        [Map("2")]
        CloseShort,
        /// <summary>
        /// Open short
        /// </summary>
        [Map("3")]
        OpenShort,
        /// <summary>
        /// Close long
        /// </summary>
        [Map("4")]
        CloseLong
    }
}
