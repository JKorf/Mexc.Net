using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models
{
    /// <summary>
    /// Paginated result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MexcPaginated<T>
    {
        /// <summary>
        /// Total records
        /// </summary>
        [JsonProperty("totalRecords")]
        public int Total { get; set; }
        /// <summary>
        /// Page
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; set; }
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonProperty("totalPageNum")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; } = default!;
    }
}
