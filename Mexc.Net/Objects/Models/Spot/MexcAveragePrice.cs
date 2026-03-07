namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Average price info
    /// </summary>
    [SerializationModel]
    public record MexcAveragePrice
    {
        /// <summary>
        /// ["<c>mins</c>"] Minutes the average is over
        /// </summary>
        [JsonPropertyName("mins")]
        public int Minutes { get; set; }

        /// <summary>
        /// ["<c>price</c>"] Average price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
