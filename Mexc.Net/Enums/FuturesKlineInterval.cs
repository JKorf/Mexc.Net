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
        /// ["<c>Min1</c>"] One minute
        /// </summary>
        [Map("Min1")]
        OneMinute = 60,
        /// <summary>
        /// ["<c>Min5</c>"] Five minutes
        /// </summary>
        [Map("Min5")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// ["<c>Min15</c>"] Fifteen minutes
        /// </summary>
        [Map("Min15")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// ["<c>Min30</c>"] Thirty minutes
        /// </summary>
        [Map("Min30")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// ["<c>Min60</c>"] One hour
        /// </summary>
        [Map("Min60")]
        OneHour = 60 * 60,
        /// <summary>
        /// ["<c>Hour4</c>"] Four hours
        /// </summary>
        [Map("Hour4")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// ["<c>Hour8</c>"] Eight hours
        /// </summary>
        [Map("Hour8")]
        EightHours = 60 * 60 * 8,
        /// <summary>
        /// ["<c>Day1</c>"] One day
        /// </summary>
        [Map("Day1")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// ["<c>Week1</c>"] One week
        /// </summary>
        [Map("Week1")]
        OneWeek = 60 * 60 * 24 * 7,
        /// <summary>
        /// ["<c>Month1</c>"] One month
        /// </summary>
        [Map("Month1")]
        OneMonth = 60 * 60 * 24 * 30
    }
}
