namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Price info
    /// </summary>
    [SerializationModel]
    public record MexcPrice
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Last price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
