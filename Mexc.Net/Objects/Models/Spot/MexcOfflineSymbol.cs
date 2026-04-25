using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Offline symbol
    /// </summary>
    public record MexcOfflineSymbol
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>status</c>"] Status of the symbol, paused or offline
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }

        /// <summary>
        /// ["<c>offlineTime</c>"] The time when the symbol went offline
        /// </summary>
        [JsonPropertyName("offlineTime")]
        public DateTime? OfflineTime { get; set; }
    }
}
