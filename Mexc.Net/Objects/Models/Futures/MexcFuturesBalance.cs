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
    /// Balance info
    /// </summary>
    public record MexcFuturesBalance
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Position margin
        /// </summary>
        [JsonPropertyName("positionMargin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Cash balance
        /// </summary>
        [JsonPropertyName("cashBalance")]
        public decimal CashBalance { get; set; }
        /// <summary>
        /// Frozen balance
        /// </summary>
        [JsonPropertyName("frozenBalance")]
        public decimal FrozenBalance { get; set; }
        /// <summary>
        /// Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Bonus
        /// </summary>
        [JsonPropertyName("bonus")]
        public decimal Bonus { get; set; }
    }


}
