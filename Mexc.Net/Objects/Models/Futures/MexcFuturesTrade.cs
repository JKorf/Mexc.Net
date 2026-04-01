using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Trade info
    /// </summary>
    public record MexcFuturesTrade
    {
        /// <summary>
        /// ["<c>p</c>"] Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Quantity
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>T</c>"] Order side
        /// </summary>
        [JsonPropertyName("T")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>M</c>"] Self transaction
        /// </summary>
        [JsonPropertyName("M")]
        public FuturesBool SelfTransaction { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>Id</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("i")]
        public long Id { get; set; }
    }


}
