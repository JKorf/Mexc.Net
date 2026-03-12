using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesOrderSide>))]
    public enum FuturesOrderSide
    {
        /// <summary>
        /// ["<c>1</c>"] Open long
        /// </summary>
        [Map("1")]
        OpenLong,
        /// <summary>
        /// ["<c>2</c>"] Close short
        /// </summary>
        [Map("2")]
        CloseShort,
        /// <summary>
        /// ["<c>3</c>"] Open short
        /// </summary>
        [Map("3")]
        OpenShort,
        /// <summary>
        /// ["<c>4</c>"] Close long
        /// </summary>
        [Map("4")]
        CloseLong
    }
}
