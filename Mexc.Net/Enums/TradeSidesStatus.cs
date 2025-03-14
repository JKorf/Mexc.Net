using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Trade side enabled status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TradeSidesStatus>))]
    public enum TradeSidesStatus
    {
        /// <summary>
        /// Both buying and selling are enabled
        /// </summary>
        [Map("1")]
        AllEnabled,
        /// <summary>
        /// Only buying is enabled
        /// </summary>
        [Map("2")]
        BuyEnabled,
        /// <summary>
        /// Only selling is enabled
        /// </summary>
        [Map("3")]
        SellEnabled,
        /// <summary>
        /// Not enabled
        /// </summary>
        [Map("4")]
        NoneEnabled
    }
}
