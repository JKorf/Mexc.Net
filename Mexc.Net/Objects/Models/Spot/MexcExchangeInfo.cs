using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public record MexcExchangeInfo
    {
        /// <summary>
        /// The timezone the server uses
        /// </summary>
        public string TimeZone { get; set; } = string.Empty;
        /// <summary>
        /// The current server time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// All symbols supported
        /// </summary>
        public IEnumerable<MexcSymbol> Symbols { get; set; } = Array.Empty<MexcSymbol>();
    }
}
