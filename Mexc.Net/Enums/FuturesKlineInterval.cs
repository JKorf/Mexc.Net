using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Kline interval
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesKlineInterval>))]
    public enum FuturesKlineInterval
    {
        /// <summary>
        /// One minute
        /// </summary>
        [Map("Min1")]
        OneMinute = 60,
        /// <summary>
        /// Five minutes
        /// </summary>
        [Map("Min5")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("Min15")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("Min30")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("Min60")]
        OneHour = 60 * 60,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("Hour4")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// Eight hours
        /// </summary>
        [Map("Hour8")]
        EightHours = 60 * 60 * 8,
        /// <summary>
        /// One day
        /// </summary>
        [Map("Day1")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// One week
        /// </summary>
        [Map("Week1")]
        OneWeek = 60 * 60 * 24 * 7,
        /// <summary>
        /// One month
        /// </summary>
        [Map("Month1")]
        OneMonth = 60 * 60 * 24 * 30
    }
}
