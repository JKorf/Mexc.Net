namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Server time
    /// </summary>
    [SerializationModel]
    public record MexcServerTime
    {
        /// <summary>
        /// ["<c>serverTime</c>"] Current server time
        /// </summary>
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
    }
}
