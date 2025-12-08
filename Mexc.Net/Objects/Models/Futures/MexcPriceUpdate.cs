namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Price
    /// </summary>
    public record MexcPriceUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }        
    }
}
