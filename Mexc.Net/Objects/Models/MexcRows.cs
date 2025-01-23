﻿
namespace Mexc.Net.Objects.Models
{
    /// <summary>
    /// Rows result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record MexcRows<T>
    {
        /// <summary>
        /// Total records
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("rows")]
        public T Data { get; set; } = default!;
    }
}
