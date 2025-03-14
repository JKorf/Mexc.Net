using CryptoExchange.Net.Converters.SystemTextJson;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Account update info
    /// </summary>
    [SerializationModel]
    public record MexcAccountUpdate
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("c")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// New free quantity
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Free { get; set; }
        /// <summary>
        /// Changed free quantity
        /// </summary>
        [JsonPropertyName("fd")]
        public decimal FreeChange { get; set; }
        /// <summary>
        /// New frozen quantity
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// Changed frozen quantity
        /// </summary>
        [JsonPropertyName("ld")]
        public decimal FrozenChange { get; set; }
        /// <summary>
        /// Trigger update type
        /// </summary>
        [JsonPropertyName("o")]
        public string UpdateType { get; set; } = string.Empty;
    }
}
