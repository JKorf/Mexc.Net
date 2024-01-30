using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// Partially filled
        /// </summary>
        [Map("PARTIALLY_FILLED")]
        PartiallyFilled,
        /// <summary>
        /// Fully canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// Partially filled, partially canceled
        /// </summary>
        [Map("PARTIALLY_CANCELED")]
        PartiallyCanceled
    }
}
