namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Price
    /// </summary>
    public record MexcPriceUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }        
    }
}
