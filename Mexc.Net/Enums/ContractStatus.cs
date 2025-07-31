using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Contract status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractStatus>))]
    public enum ContractStatus
    {
        /// <summary>
        /// Trading is enabled
        /// </summary>
        [Map("0")]
        Enabled,
        /// <summary>
        /// Delivering
        /// </summary>
        [Map("1")]
        Delivering,
        /// <summary>
        /// Completed
        /// </summary>
        [Map("2")]
        Completed,
        /// <summary>
        /// Symbol is offline
        /// </summary>
        [Map("3")]
        Offline,
        /// <summary>
        /// Trading is paused
        /// </summary>
        [Map("4")]
        Paused
    }
}
