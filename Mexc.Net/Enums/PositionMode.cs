using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionMode>))]
    public enum PositionMode
    {
        /// <summary>
        /// ["<c>1</c>"] Hedge mode
        /// </summary>
        [Map("1")]
        Hedge,
        /// <summary>
        /// ["<c>2</c>"] One way mode
        /// </summary>
        [Map("2")]
        OneWay
    }
}
