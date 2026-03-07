using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// 
    /// </summary>
    public record MexcPosition
    {
        /// <summary>
        /// ["<c>positionId</c>"] Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public long PositionId { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionType</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>openType</c>"] Margin type
        /// </summary>
        [JsonPropertyName("openType")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public PositionStatus Status { get; set; }
        /// <summary>
        /// ["<c>holdVol</c>"] Holding volume
        /// </summary>
        [JsonPropertyName("holdVol")]
        public decimal PositionSize { get; set; }
        /// <summary>
        /// ["<c>frozenVol</c>"] Frozen volume
        /// </summary>
        [JsonPropertyName("frozenVol")]
        public decimal FrozenVolume { get; set; }
        /// <summary>
        /// ["<c>closeVol</c>"] Close volume
        /// </summary>
        [JsonPropertyName("closeVol")]
        public decimal CloseVolume { get; set; }
        /// <summary>
        /// ["<c>holdAvgPrice</c>"] Hold average price
        /// </summary>
        [JsonPropertyName("holdAvgPrice")]
        public decimal HoldAveragePrice { get; set; }
        /// <summary>
        /// ["<c>openAvgPrice</c>"] Open average price
        /// </summary>
        [JsonPropertyName("openAvgPrice")]
        public decimal OpenAveragePrice { get; set; }
        /// <summary>
        /// ["<c>closeAvgPrice</c>"] Close average price
        /// </summary>
        [JsonPropertyName("closeAvgPrice")]
        public decimal CloseAveragePrice { get; set; }
        /// <summary>
        /// ["<c>liquidatePrice</c>"] Liquidate price
        /// </summary>
        [JsonPropertyName("liquidatePrice")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>oim</c>"] Original initial margin
        /// </summary>
        [JsonPropertyName("oim")]
        public decimal OriginalInitialMargin { get; set; }
        /// <summary>
        /// ["<c>im</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("im")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>holdFee</c>"] Hold fee
        /// </summary>
        [JsonPropertyName("holdFee")]
        public decimal HoldFee { get; set; }
        /// <summary>
        /// ["<c>realised</c>"] Realised profit and loss
        /// </summary>
        [JsonPropertyName("realised")]
        public decimal RealisedPnl { get; set; }
        /// <summary>
        /// ["<c>adlLevel</c>"] Auto deleverage level
        /// </summary>
        [JsonPropertyName("adlLevel")]
        public int AdlLevel { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>autoAddIm</c>"] Auto margin enabled
        /// </summary>
        [JsonPropertyName("autoAddIm")]
        public bool AutoMargin { get; set; }
    }


}
