using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountType>))]
    public enum AccountType
    {
        /// <summary>
        /// Spot account
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// Futures account
        /// </summary>
        [Map("FUTURES")]
        Futures
    }
}
