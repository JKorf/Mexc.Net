using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models
{
    /// <summary>
    /// Paginated result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [SerializationModel]
    public record MexcPaginated<T>
    {
        /// <summary>
        /// Total records
        /// </summary>
        [JsonPropertyName("totalRecords")]
        public int Total { get; set; }
        /// <summary>
        /// Page
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonPropertyName("totalPageNum")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
