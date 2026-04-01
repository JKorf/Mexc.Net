using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Leverage mode update
    /// </summary>
    public record MexcLeverageModeUpdate
    {
        /// <summary>
        /// ["<c>lm</c>"] Leverage mode
        /// </summary>
        [JsonPropertyName("lm")]
        public int PositionMode { get; set; }
    }
}
