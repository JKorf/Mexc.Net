using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Risk limit type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RiskLimitType>))]
    public enum RiskLimitType
    {
        /// <summary>
        /// ["<c>BY_VOLUME</c>"] By volume
        /// </summary>
        [Map("BY_VOLUME")]
        ByVolume,
        /// <summary>
        /// ["<c>BY_VALUE</c>"] By value
        /// </summary>
        [Map("BY_VALUE")]
        ByValue
    }
}
