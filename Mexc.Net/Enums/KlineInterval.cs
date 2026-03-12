using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Kline interval
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KlineInterval>))]
    public enum KlineInterval
    {
        /// <summary>
        /// ["<c>1m</c>"] One minute
        /// </summary>
        [Map("1m", "Min1")]
        OneMinute = 60,
        /// <summary>
        /// ["<c>5m</c>"] Five minutes
        /// </summary>
        [Map("5m", "Min5")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// ["<c>15m</c>"] Fifteen minutes
        /// </summary>
        [Map("15m", "Min15")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// ["<c>30m</c>"] Thirty minutes
        /// </summary>
        [Map("30m", "Min30")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// ["<c>60m</c>"] One hour
        /// </summary>
        [Map("60m", "Min60")]
        OneHour = 60 * 60,
        /// <summary>
        /// ["<c>4h</c>"] Four hours
        /// </summary>
        [Map("4h", "Hour4")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// ["<c>1d</c>"] One day
        /// </summary>
        [Map("1d", "Day1")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// ["<c>1W</c>"] One week
        /// </summary>
        [Map("1W", "Week1")]
        OneWeek = 60 * 60 * 24 * 7,
        /// <summary>
        /// ["<c>1M</c>"] One month
        /// </summary>
        [Map("1M", "Month1")]
        OneMonth = 60 * 60 * 24 * 30
    }
}
