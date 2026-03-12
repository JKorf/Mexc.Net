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
        /// ["<c>LIMIT</c>"] Limit
        /// </summary>
        [Map("LIMIT", "1")]
        Limit,
        /// <summary>
        /// ["<c>MARKET</c>"] Market
        /// </summary>
        [Map("MARKET", "5")]
        Market,
        /// <summary>
        /// ["<c>LIMIT_MAKER</c>"] Limit maker
        /// </summary>
        [Map("LIMIT_MAKER", "2")]
        LimitMaker,
        /// <summary>
        /// ["<c>IMMEDIATE_OR_CANCEL</c>"] Immediate or cancel
        /// </summary>
        [Map("IMMEDIATE_OR_CANCEL", "3")]
        ImmediateOrCancel,
        /// <summary>
        /// ["<c>FILL_OR_KILL</c>"] Fill or kill
        /// </summary>
        [Map("FILL_OR_KILL", "4")]
        FillOrKill,
        /// <summary>
        /// Take profit / Stop loss order
        /// </summary>
        TpSlOrder
    }
}
