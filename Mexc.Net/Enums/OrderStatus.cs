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
        /// ["<c>NEW</c>"] New
        /// </summary>
        [Map("NEW", "1")]
        New,
        /// <summary>
        /// ["<c>FILLED</c>"] Filled
        /// </summary>
        [Map("FILLED", "2")]
        Filled,
        /// <summary>
        /// ["<c>PARTIALLY_FILLED</c>"] Partially filled
        /// </summary>
        [Map("PARTIALLY_FILLED", "3")]
        PartiallyFilled,
        /// <summary>
        /// ["<c>CANCELED</c>"] Fully canceled
        /// </summary>
        [Map("CANCELED", "4")]
        Canceled,
        /// <summary>
        /// ["<c>PARTIALLY_CANCELED</c>"] Partially filled, partially canceled
        /// </summary>
        [Map("PARTIALLY_CANCELED", "5")]
        PartiallyCanceled
    }
}
