using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesOrderType>))]
    public enum FuturesOrderType
    {
        /// <summary>
        /// ["<c>1</c>"] Limit
        /// </summary>
        [Map("1")]
        Limit,
        /// <summary>
        /// ["<c>5</c>"] Market
        /// </summary>
        [Map("5")]
        Market,
        /// <summary>
        /// ["<c>2</c>"] Limit maker
        /// </summary>
        [Map("2")]
        LimitMaker,
        /// <summary>
        /// ["<c>3</c>"] Immediate or cancel
        /// </summary>
        [Map("3")]
        ImmediateOrCancel,
        /// <summary>
        /// ["<c>4</c>"] Fill or kill
        /// </summary>
        [Map("4")]
        FillOrKill
    }
}
