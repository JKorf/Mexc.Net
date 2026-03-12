using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Transfer direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferDirection>))]
    public enum TransferDirection
    {
        /// <summary>
        /// ["<c>IN</c>"] In
        /// </summary>
        [Map("IN")]
        In,
        /// <summary>
        /// ["<c>OUT</c>"] Out
        /// </summary>
        [Map("OUT")]
        Out
    }
}
