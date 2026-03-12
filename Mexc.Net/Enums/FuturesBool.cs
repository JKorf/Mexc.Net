using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Bool
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesBool>))]
    public enum FuturesBool
    {
        /// <summary>
        /// ["<c>1</c>"] Yes
        /// </summary>
        [Map("1")]
        Yes,
        /// <summary>
        /// ["<c>2</c>"] No
        /// </summary>
        [Map("2")]
        No
    }
}
