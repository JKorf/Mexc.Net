﻿namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public record MexcExchangeInfo
    {
        /// <summary>
        /// The timezone the server uses
        /// </summary>
        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; } = string.Empty;
        /// <summary>
        /// The current server time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// All symbols supported
        /// </summary>
        [JsonPropertyName("symbols")]
        public IEnumerable<MexcSymbol> Symbols { get; set; } = Array.Empty<MexcSymbol>();
    }
}
