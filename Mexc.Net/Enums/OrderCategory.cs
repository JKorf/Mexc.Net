using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Order category
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderCategory>))]
    public enum OrderCategory
    {
        /// <summary>
        /// Limit order
        /// </summary>
        [Map("1")]
        Limit,
        /// <summary>
        /// System takeover delegate
        /// </summary>
        [Map("2")]
        SystemDelegate,
        /// <summary>
        /// Close delegate
        /// </summary>
        [Map("3")]
        CloseDelegate,
        /// <summary>
        /// Auto Deleverage reduction
        /// </summary>
        [Map("4")]
        ADLReduction
    }
}
