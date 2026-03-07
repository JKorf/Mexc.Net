namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Exchange info
    /// </summary>
    [SerializationModel]
    public record MexcExchangeInfo
    {
        /// <summary>
        /// ["<c>timezone</c>"] The timezone the server uses
        /// </summary>
        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>serverTime</c>"] The current server time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// ["<c>symbols</c>"] All symbols supported
        /// </summary>
        [JsonPropertyName("symbols")]
        public MexcSymbol[] Symbols { get; set; } = Array.Empty<MexcSymbol>();
    }
}
