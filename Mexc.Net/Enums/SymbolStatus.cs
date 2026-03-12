using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Symbol status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolStatus>))]
    public enum SymbolStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Trading is enabled
        /// </summary>
        [Map("1")]
        Enabled,
        /// <summary>
        /// ["<c>2</c>"] Trading is paused
        /// </summary>
        [Map("2")]
        Paused,
        /// <summary>
        /// ["<c>3</c>"] Symbol is offline
        /// </summary>
        [Map("3")]
        Offline
    }
}
