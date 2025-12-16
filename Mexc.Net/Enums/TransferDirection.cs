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
        /// In
        /// </summary>
        [Map("IN")]
        In,
        /// <summary>
        /// Out
        /// </summary>
        [Map("OUT")]
        Out
    }
}
