namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Auto deleverage update
    /// </summary>
    public record MexcAdlUpdate
    {
        /// <summary>
        /// Adl level
        /// </summary>
        [JsonPropertyName("adlLevel")]
        public int AdlLevel { get; set; }
        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public long PositionId { get; set; }
    }
}
