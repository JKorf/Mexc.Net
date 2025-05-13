using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Withdrawal status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawStatus>))]
    public enum WithdrawStatus
    {
        /// <summary>
        /// Applied for withdrawal
        /// </summary>
        [Map("1")]
        Applied,
        /// <summary>
        /// Auditing
        /// </summary>
        [Map("2")]
        Auditing,
        /// <summary>
        /// Waiting
        /// </summary>
        [Map("3")]
        Waiting,
        /// <summary>
        /// Processing
        /// </summary>
        [Map("4")]
        Processing,
        /// <summary>
        /// Wait for packaging
        /// </summary>
        [Map("5")]
        WaitPackaging,
        /// <summary>
        /// Wait for confirmations
        /// </summary>
        [Map("6")]
        WaitConfirmations,
        /// <summary>
        /// Successful
        /// </summary>
        [Map("7")]
        Success,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("8")]
        Failed,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("9")]
        Canceled,
        /// <summary>
        /// Manual
        /// </summary>
        [Map("10")]
        Manual
    }
}
