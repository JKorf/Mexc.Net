using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Deposit status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DepositStatus>))]
    public enum DepositStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Small delay
        /// </summary>
        [Map("1")]
        DelayedSmall,
        /// <summary>
        /// ["<c>2</c>"] Delayed
        /// </summary>
        [Map("2")]
        Delayed,
        /// <summary>
        /// ["<c>3</c>"] Large delay
        /// </summary>
        [Map("3")]
        DelayedLarge,
        /// <summary>
        /// ["<c>4</c>"] Pending
        /// </summary>
        [Map("4")]
        Pending,
        /// <summary>
        /// ["<c>5</c>"] Success
        /// </summary>
        [Map("5")]
        Success,
        /// <summary>
        /// ["<c>6</c>"] Auditing
        /// </summary>
        [Map("6")]
        Auditing,
        /// <summary>
        /// ["<c>7</c>"] Rejected
        /// </summary>
        [Map("7")]
        Rejected
    }
}
