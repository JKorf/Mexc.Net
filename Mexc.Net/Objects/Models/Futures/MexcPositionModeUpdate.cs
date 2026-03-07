using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Position mode update
    /// </summary>
    public record MexcPositionModeUpdate
    {
        /// <summary>
        /// ["<c>positionMode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("positionMode")]
        public PositionMode PositionMode { get; set; }
    }
}
