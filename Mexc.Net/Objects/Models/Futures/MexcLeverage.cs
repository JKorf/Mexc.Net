using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Leverage info
    /// </summary>
    public record MexcLeverage
    {
        /// <summary>
        /// ["<c>positionType</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>level</c>"] Level
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }
        /// <summary>
        /// ["<c>imr</c>"] Initial margin rate
        /// </summary>
        [JsonPropertyName("imr")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>mmr</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }
        /// <summary>
        /// ["<c>maxLeverageView</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverageView")]
        public int MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>maxVol</c>"] Max volume
        /// </summary>
        [JsonPropertyName("maxVol")]
        public decimal MaxVolume { get; set; }
        /// <summary>
        /// ["<c>currentMmr</c>"] Current margin maintenance rate
        /// </summary>
        [JsonPropertyName("currentMmr")]
        public decimal CurrentMaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>openType</c>"] Margin type
        /// </summary>
        [JsonPropertyName("openType")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// ["<c>limitBySys</c>"] Limited by system
        /// </summary>
        [JsonPropertyName("limitBySys")]
        public bool LimitBySystem { get; set; }
    }


}
