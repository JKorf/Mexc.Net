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
        /// ["<c>1</c>"] Open
        /// </summary>
        [Map("1", "2")]
        Open,
        /// <summary>
        /// ["<c>3</c>"] Filled
        /// </summary>
        [Map("3")]
        Filled,
        /// <summary>
        /// ["<c>4</c>"] Fully canceled
        /// </summary>
        [Map("4")]
        Canceled,
        /// <summary>
        /// ["<c>5</c>"] Invalid
        /// </summary>
        [Map("5")]
        Invalid
    }
}
