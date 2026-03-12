using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// KYC status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KycStatus>))]
    public enum KycStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Unverified
        /// </summary>
        [Map("1")]
        Unverified,
        /// <summary>
        /// ["<c>2</c>"] Primary
        /// </summary>
        [Map("2")]
        Primary,
        /// <summary>
        /// ["<c>3</c>"] Advanced
        /// </summary>
        [Map("3")]
        Advanced,
        /// <summary>
        /// ["<c>4</c>"] Institutional
        /// </summary>
        [Map("4")]
        Institutional
    }
}
