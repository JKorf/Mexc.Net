namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Account update info
    /// </summary>
    [SerializationModel]
    public record MexcAccountUpdate
    {
        /// <summary>
        /// ["<c>a</c>"] Asset
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>c</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("c")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>f</c>"] New free quantity
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Free { get; set; }
        /// <summary>
        /// ["<c>fd</c>"] Changed free quantity
        /// </summary>
        [JsonPropertyName("fd")]
        public decimal FreeChange { get; set; }
        /// <summary>
        /// ["<c>l</c>"] New frozen quantity
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>ld</c>"] Changed frozen quantity
        /// </summary>
        [JsonPropertyName("ld")]
        public decimal FrozenChange { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Trigger update type
        /// </summary>
        [JsonPropertyName("o")]
        public string UpdateType { get; set; } = string.Empty;
    }
}
