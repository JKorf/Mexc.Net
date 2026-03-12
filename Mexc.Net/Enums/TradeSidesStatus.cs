using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Trade side enabled status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TradeSidesStatus>))]
    public enum TradeSidesStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Both buying and selling are enabled
        /// </summary>
        [Map("1")]
        AllEnabled,
        /// <summary>
        /// ["<c>2</c>"] Only buying is enabled
        /// </summary>
        [Map("2")]
        BuyEnabled,
        /// <summary>
        /// ["<c>3</c>"] Only selling is enabled
        /// </summary>
        [Map("3")]
        SellEnabled,
        /// <summary>
        /// ["<c>4</c>"] Not enabled
        /// </summary>
        [Map("4")]
        NoneEnabled
    }
}
