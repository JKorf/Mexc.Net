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
        [Map("1m", "Min1")]
        OneMinute,
        /// <summary>
        /// Five minutes
        /// </summary>
        [Map("5m", "Min5")]
        FiveMinutes,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("15m", "Min15")]
        FifteenMinutes,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("30m", "Min30")]
        ThirtyMinutes,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("60m", "Min60")]
        OneHour,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("4h", "Hour4")]
        FourHours,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1d", "Day1")]
        OneDay,
        /// <summary>
        /// One week
        /// </summary>
        [Map("1w", "Week1")]
        OneWeek,
        /// <summary>
        /// One month
        /// </summary>
        [Map("1M", "Month1")]
        OneMonth
    }
}
