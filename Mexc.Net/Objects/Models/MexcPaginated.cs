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
        /// ["<c>totalRecords</c>"] Total records
        /// </summary>
        [JsonPropertyName("totalRecords")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>page</c>"] Page
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }
        /// <summary>
        /// ["<c>totalPageNum</c>"] Total pages
        /// </summary>
        [JsonPropertyName("totalPageNum")]
        public int TotalPages { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
