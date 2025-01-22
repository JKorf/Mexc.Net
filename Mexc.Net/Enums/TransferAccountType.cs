using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Transfer account type
    /// </summary>
    public enum TransferAccountType
    {
        /// <summary>
        /// Email
        /// </summary>
        [Map("EMAIL")]
        Email,
        /// <summary>
        /// User id
        /// </summary>
        [Map("UID")]
        UID,
        /// <summary>
        /// Mobile number
        /// </summary>
        [Map("MOBILE")]
        Mobile
    }
}
