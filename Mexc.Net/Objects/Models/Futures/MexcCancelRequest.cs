namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Cancel request
    /// </summary>
    public record MexcCancelRequest
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("orderId")]
        public long? OrderId { get; set; }
        /// <summary>
        /// ["<c>externalOid</c>"] Client order id
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("externalOid")]
        public string? ClientOrderId { get; set; }
    }
}
