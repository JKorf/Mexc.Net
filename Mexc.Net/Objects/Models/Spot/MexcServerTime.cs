namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Server time
    /// </summary>
    public record MexcServerTime
    {
        /// <summary>
        /// Current server time
        /// </summary>
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
    }
}
