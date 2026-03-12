using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Transfer account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferAccountType>))]
    public enum TransferAccountType
    {
        /// <summary>
        /// ["<c>EMAIL</c>"] Email
        /// </summary>
        [Map("EMAIL")]
        Email,
        /// <summary>
        /// ["<c>UID</c>"] User id
        /// </summary>
        [Map("UID")]
        UID,
        /// <summary>
        /// ["<c>MOBILE</c>"] Mobile number
        /// </summary>
        [Map("MOBILE")]
        Mobile
    }
}
