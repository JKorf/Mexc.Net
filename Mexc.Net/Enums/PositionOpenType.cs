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
        /// ["<c>1</c>"] Isolated
        /// </summary>
        [Map("1")]
        Isolated,
        /// <summary>
        /// ["<c>2</c>"] Cross
        /// </summary>
        [Map("2")]
        Cross,
        /// <summary>
        /// ["<c>3</c>"] Both
        /// </summary>
        [Map("3")]
        Both
    }
}
