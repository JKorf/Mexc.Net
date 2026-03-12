using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Trigger type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerType>))]
    public enum TriggerType
    {
        /// <summary>
        /// ["<c>1</c>"] More than or equal
        /// </summary>
        [Map("1")]
        MoreThanOrEqual,
        /// <summary>
        /// ["<c>2</c>"] Less than or equal
        /// </summary>
        [Map("2")]
        LessThanOrEqual
    }
}
