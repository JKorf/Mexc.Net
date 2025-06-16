using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// Limit
        /// </summary>
        [Map("LIMIT", "1")]
        Limit,
        /// <summary>
        /// Market
        /// </summary>
        [Map("MARKET", "5")]
        Market,
        /// <summary>
        /// Limit maker
        /// </summary>
        [Map("LIMIT_MAKER", "2")]
        LimitMaker,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("IMMEDIATE_OR_CANCEL", "3")]
        ImmediateOrCancel,
        /// <summary>
        /// Fill or kill
        /// </summary>
        [Map("FILL_OR_KILL", "4")]
        FillOrKill,
        /// <summary>
        /// Take profit / Stop loss order
        /// </summary>
        TpSlOrder
    }
}
