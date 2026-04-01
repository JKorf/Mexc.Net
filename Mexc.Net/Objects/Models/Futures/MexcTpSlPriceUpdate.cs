using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// Tp sl price update
/// </summary>
public record MexcTpSlPriceUpdate
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public long OrderId { get; set; }
    /// <summary>
    /// ["<c>lossTrend</c>"] Stop loss price trigger type
    /// </summary>
    [JsonPropertyName("lossTrend")]
    public TriggerPriceType StopLossPriceType { get; set; }
    /// <summary>
    /// ["<c>profitTrend</c>"] Take profit price trigger type
    /// </summary>
    [JsonPropertyName("profitTrend")]
    public TriggerPriceType TakeProfitPriceType { get; set; }
    /// <summary>
    /// ["<c>stopLossPrice</c>"] Stop loss price
    /// </summary>
    [JsonPropertyName("stopLossPrice")]
    public decimal StopLossPrice { get; set; }
    /// <summary>
    /// ["<c>takeProfitPrice</c>"] Take profit price
    /// </summary>
    [JsonPropertyName("takeProfitPrice")]
    public decimal TakeProfitPrice { get; set; }
}

