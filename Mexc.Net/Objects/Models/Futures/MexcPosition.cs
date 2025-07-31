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
    /// 
    /// </summary>
    public record MexcPosition
    {
        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public long PositionId { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("openType")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("state")]
        public PositionStatus Status { get; set; }
        /// <summary>
        /// Holding volume
        /// </summary>
        [JsonPropertyName("holdVol")]
        public decimal PositionSize { get; set; }
        /// <summary>
        /// Frozen volume
        /// </summary>
        [JsonPropertyName("frozenVol")]
        public decimal FrozenVolume { get; set; }
        /// <summary>
        /// Close volume
        /// </summary>
        [JsonPropertyName("closeVol")]
        public decimal CloseVolume { get; set; }
        /// <summary>
        /// Hold average price
        /// </summary>
        [JsonPropertyName("holdAvgPrice")]
        public decimal HoldAveragePrice { get; set; }
        /// <summary>
        /// Open average price
        /// </summary>
        [JsonPropertyName("openAvgPrice")]
        public decimal OpenAveragePrice { get; set; }
        /// <summary>
        /// Close average price
        /// </summary>
        [JsonPropertyName("closeAvgPrice")]
        public decimal CloseAveragePrice { get; set; }
        /// <summary>
        /// Liquidate price
        /// </summary>
        [JsonPropertyName("liquidatePrice")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Original initial margin
        /// </summary>
        [JsonPropertyName("oim")]
        public decimal OriginalInitialMargin { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("im")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Hold fee
        /// </summary>
        [JsonPropertyName("holdFee")]
        public decimal HoldFee { get; set; }
        /// <summary>
        /// Realised profit and loss
        /// </summary>
        [JsonPropertyName("realised")]
        public decimal RealisedPnl { get; set; }
        /// <summary>
        /// Auto deleverage level
        /// </summary>
        [JsonPropertyName("adlLevel")]
        public int AdlLevel { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Auto margin enabled
        /// </summary>
        [JsonPropertyName("autoAddIm")]
        public bool AutoMargin { get; set; }
    }


}
