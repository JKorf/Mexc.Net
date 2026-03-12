using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Margin type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginType>))]
    public enum MarginType
    {
        /// <summary>
        /// ["<c>1</c>"] Isolated margin
        /// </summary>
        [Map("1")]
        Isolated,
        /// <summary>
        /// ["<c>2</c>"] Cross margin
        /// </summary>
        [Map("2")]
        Cross
    }
}
