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
        /// ["<c>0</c>"] Trading is enabled
        /// </summary>
        [Map("0")]
        Enabled,
        /// <summary>
        /// ["<c>1</c>"] Delivering
        /// </summary>
        [Map("1")]
        Delivering,
        /// <summary>
        /// ["<c>2</c>"] Completed
        /// </summary>
        [Map("2")]
        Completed,
        /// <summary>
        /// ["<c>3</c>"] Symbol is offline
        /// </summary>
        [Map("3")]
        Offline,
        /// <summary>
        /// ["<c>4</c>"] Trading is paused
        /// </summary>
        [Map("4")]
        Paused
    }
}
