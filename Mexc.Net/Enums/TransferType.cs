using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferType>))]
    public enum TransferType
    {
        /// <summary>
        /// ["<c>0</c>"] Outside transfer
        /// </summary>
        [Map("0")]
        TransferOut,
        /// <summary>
        /// ["<c>1</c>"] Internal transfer
        /// </summary>
        [Map("1")]
        TransferInternal
    }
}
