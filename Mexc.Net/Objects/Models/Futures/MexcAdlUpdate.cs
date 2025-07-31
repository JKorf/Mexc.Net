using CryptoExchange.Net.Converters;
using Mexc.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
