using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Kline interval
    /// </summary>
    public enum KlineInterval
    {
        /// <summary>
        /// One minute
        /// </summary>
        [Map("1m")]
        OneMinute,
        /// <summary>
        /// Five minutes
        /// </summary>
        [Map("5m")]
        FiveMinutes,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("15m")]
        FifteenMinutes,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("30m")]
        ThirtyMinutes,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("60m")]
        OneHour,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("4h")]
        FourHours,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1d")]
        OneDay,
        /// <summary>
        /// One week
        /// </summary>
        [Map("1w")]
        OneWeek,
        /// <summary>
        /// One month
        /// </summary>
        [Map("1M")]
        OneMonth
    }
}
