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
        /// ["<c>1</c>"] Applied for withdrawal
        /// </summary>
        [Map("1")]
        Applied,
        /// <summary>
        /// ["<c>2</c>"] Auditing
        /// </summary>
        [Map("2")]
        Auditing,
        /// <summary>
        /// ["<c>3</c>"] Waiting
        /// </summary>
        [Map("3")]
        Waiting,
        /// <summary>
        /// ["<c>4</c>"] Processing
        /// </summary>
        [Map("4")]
        Processing,
        /// <summary>
        /// ["<c>5</c>"] Wait for packaging
        /// </summary>
        [Map("5")]
        WaitPackaging,
        /// <summary>
        /// ["<c>6</c>"] Wait for confirmations
        /// </summary>
        [Map("6")]
        WaitConfirmations,
        /// <summary>
        /// ["<c>7</c>"] Successful
        /// </summary>
        [Map("7")]
        Success,
        /// <summary>
        /// ["<c>8</c>"] Failed
        /// </summary>
        [Map("8")]
        Failed,
        /// <summary>
        /// ["<c>9</c>"] Canceled
        /// </summary>
        [Map("9")]
        Canceled,
        /// <summary>
        /// ["<c>10</c>"] Manual
        /// </summary>
        [Map("10")]
        Manual
    }
}
