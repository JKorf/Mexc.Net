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
        /// By volume
        /// </summary>
        [Map("BY_VOLUME")]
        ByVolume,
        /// <summary>
        /// By value
        /// </summary>
        [Map("BY_VALUE")]
        ByValue
    }
}
