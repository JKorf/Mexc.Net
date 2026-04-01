using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// 
/// </summary>
public record MexcLiquidationUpdate
{
    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; set; }
    /// <summary>
    /// ["<c>param</c>"] Param
    /// </summary>
    [JsonPropertyName("param")]
    public MexcLiquidationUpdateData Param { get; set; } = null!;
} 

/// <summary>
/// Liquidation update data
/// </summary>
public record MexcLiquidationUpdateData
{
    /// <summary>
    /// ["<c>notifyType</c>"] Notify type
    /// </summary>
    [JsonPropertyName("notifyType")]
    public LiquidationNotifyType NotifyType { get; set; }
    /// <summary>
    /// ["<c>openType</c>"] Margin type
    /// </summary>
    [JsonPropertyName("openType")]
    public MarginType MarginType { get; set; }
    /// <summary>
    /// ["<c>dn</c>"] Display name of symbol
    /// </summary>
    [JsonPropertyName("dn")]
    public string SymbolDisplayName { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>dne</c>"] Display name of symbol in English
    /// </summary>
    [JsonPropertyName("dne")]
    public string SymbolDisplayNameEnglish { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>multiAssets</c>"] Multi asset mode enabled
    /// </summary>
    [JsonPropertyName("multiAssets")]
    public bool MultiAssets { get; set; }
    /// <summary>
    /// ["<c>marginRate</c>"] Margin rate
    /// </summary>
    [JsonPropertyName("marginRate")]
    public decimal MarginRate { get; set; }
}

