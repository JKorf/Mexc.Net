using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Position status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionStatus>))]
    public enum PositionStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Holding
        /// </summary>
        [Map("1")]
        Holding,
        /// <summary>
        /// ["<c>2</c>"] System auto holding
        /// </summary>
        [Map("2")]
        SystemAutoHolding,
        /// <summary>
        /// ["<c>3</c>"] Closed
        /// </summary>
        [Map("3")]
        Closed
    }
}
