using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Position open type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionOpenType>))]
    public enum PositionOpenType
    {
        /// <summary>
        /// Isolated
        /// </summary>
        [Map("1")]
        Isolated,
        /// <summary>
        /// Cross
        /// </summary>
        [Map("2")]
        Cross,
        /// <summary>
        /// Both
        /// </summary>
        [Map("3")]
        Both
    }
}
