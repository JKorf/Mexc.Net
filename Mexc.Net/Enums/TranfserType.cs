using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    public enum TranfserType
    {
        /// <summary>
        /// Outside transfer
        /// </summary>
        [Map("0")]
        TransferOut,
        /// <summary>
        /// Internal transfer
        /// </summary>
        [Map("1")]
        TransferInternal
    }
}
