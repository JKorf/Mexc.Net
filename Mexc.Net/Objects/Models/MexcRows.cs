using Newtonsoft.Json;

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
        [JsonProperty("total")]
        public int Total { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonProperty("rows")]
        public T Data { get; set; } = default!;
    }
}
