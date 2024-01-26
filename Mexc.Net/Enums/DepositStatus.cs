using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Deposit status
    /// </summary>
    public enum DepositStatus
    {
        /// <summary>
        /// Small delay
        /// </summary>
        [Map("1")]
        DelayedSmall,
        /// <summary>
        /// Delayed
        /// </summary>
        [Map("2")]
        Delayed,
        /// <summary>
        /// Large delay
        /// </summary>
        [Map("3")]
        DelayedLarge,
        /// <summary>
        /// Pending
        /// </summary>
        [Map("4")]
        Pending,
        /// <summary>
        /// Success
        /// </summary>
        [Map("5")]
        Success,
        /// <summary>
        /// Auditing
        /// </summary>
        [Map("6")]
        Auditing,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("7")]
        Rejected
    }
}
