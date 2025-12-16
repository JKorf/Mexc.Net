using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Position mode update
    /// </summary>
    public record MexcPositionModeUpdate
    {
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonPropertyName("positionMode")]
        public PositionMode PositionMode { get; set; }
    }
}
