using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// 
/// </summary>
public record MexcLiquidationRiskUpdate
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>positionId</c>"] Position id
    /// </summary>
    [JsonPropertyName("positionId")]
    public long PositionId { get; set; }
    /// <summary>
    /// ["<c>liquidatePrice</c>"] Liquidate price
    /// </summary>
    [JsonPropertyName("liquidatePrice")]
    public decimal LiquidatePrice { get; set; }
    /// <summary>
    /// ["<c>marginRatio</c>"] Margin ratio
    /// </summary>
    [JsonPropertyName("marginRatio")]
    public decimal MarginRatio { get; set; }
    /// <summary>
    /// ["<c>adlLevel</c>"] Adl level
    /// </summary>
    [JsonPropertyName("adlLevel")]
    public int AdlLevel { get; set; }
}

