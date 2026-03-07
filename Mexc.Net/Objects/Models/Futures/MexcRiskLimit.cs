using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Risk limit info
    /// </summary>
    public record MexcRiskLimit
    {
        /// <summary>
        /// ["<c>level</c>"] Level
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }
        /// <summary>
        /// ["<c>maxVol</c>"] Max quantity
        /// </summary>
        [JsonPropertyName("maxVol")]
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// ["<c>maxLeverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public int MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>mmr</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>imr</c>"] Initial margin rate
        /// </summary>
        [JsonPropertyName("imr")]
        public decimal InitialMarginRate { get; set; }
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
    }


}
