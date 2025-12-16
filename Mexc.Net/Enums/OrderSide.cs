using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// Buy
        /// </summary>
        [Map("BUY", "1")]
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
        [Map("SELL", "2")]
        Sell
    }
}
