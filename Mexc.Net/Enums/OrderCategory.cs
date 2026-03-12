using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Order category
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderCategory>))]
    public enum OrderCategory
    {
        /// <summary>
        /// ["<c>1</c>"] Limit order
        /// </summary>
        [Map("1")]
        Limit,
        /// <summary>
        /// ["<c>2</c>"] System takeover delegate
        /// </summary>
        [Map("2")]
        SystemDelegate,
        /// <summary>
        /// ["<c>3</c>"] Close delegate
        /// </summary>
        [Map("3")]
        CloseDelegate,
        /// <summary>
        /// ["<c>4</c>"] Auto Deleverage reduction
        /// </summary>
        [Map("4")]
        ADLReduction
    }
}
