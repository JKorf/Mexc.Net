namespace Mexc.Net.Objects.Models
{
    /// <summary>
    /// Rows result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [SerializationModel]
    public record MexcRows<T>
    {
        /// <summary>
        /// ["<c>total</c>"] Total records
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>rows</c>"] Data
        /// </summary>
        [JsonPropertyName("rows")]
        public T Data { get; set; } = default!;
    }
}
