using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Change type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ChangeType>))]
    public enum ChangeType
    {
        /// <summary>
        /// ["<c>ADD</c>"] Increase
        /// </summary>
        [Map("ADD")]
        Increase,
        /// <summary>
        /// ["<c>SUB</c>"] Decrease
        /// </summary>
        [Map("SUB")]
        Decrease
    }
}
