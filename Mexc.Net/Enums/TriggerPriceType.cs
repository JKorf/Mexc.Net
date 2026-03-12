using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Trigger price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerPriceType>))]
    public enum TriggerPriceType
    {
        /// <summary>
        /// ["<c>1</c>"] Last price
        /// </summary>
        [Map("1")]
        LastPrice,
        /// <summary>
        /// ["<c>2</c>"] Mark price
        /// </summary>
        [Map("2")]
        MarkPrice,
        /// <summary>
        /// ["<c>3</c>"] Index price
        /// </summary>
        [Map("3")]
        IndexPrice
    }
}
