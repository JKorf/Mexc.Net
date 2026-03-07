namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order result
    /// </summary>
    public record MexcOrderResult
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string? OrderId { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }

        /// <summary>
        /// ["<c>msg</c>"] Error message
        /// </summary>
        [JsonInclude, JsonPropertyName("msg")]
        internal string? ErrorMessage { get; set; }
        /// <summary>
        /// ["<c>code</c>"] Error code
        /// </summary>
        [JsonInclude, JsonPropertyName("code")]
        internal int? ErrorCode { get; set; }
    }
}
