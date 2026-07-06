using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Trade type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TradeType>))]
    public enum TradeType
    {
        /// <summary>
        /// ["<c>ASK</c>"] Ask
        /// </summary>
        [Map("ASK")]
        Ask,
        /// <summary>
        /// ["<c>BID</c>"] Bid
        /// </summary>
        [Map("BID")]
        Bid
    }
}
