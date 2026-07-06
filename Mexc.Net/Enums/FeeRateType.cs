using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Fee rate type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FeeRateType>))]
    public enum FeeRateType
    {
        /// <summary>
        /// Base fee rate
        /// </summary>
        [Map("BASE")]
        Base,
        /// <summary>
        /// Temporary fee rate
        /// </summary>
        [Map("TEMP")]
        Temporary
    }
}
