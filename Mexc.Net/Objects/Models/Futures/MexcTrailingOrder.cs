using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// Trailing order
/// </summary>
public record MexcTrailingOrder
{
    /// <summary>
    /// ["<c>id</c>"] Id
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>leverage</c>"] Leverage
    /// </summary>
    [JsonPropertyName("leverage")]
    public decimal Leverage { get; set; }
    /// <summary>
    /// ["<c>side</c>"] Side
    /// </summary>
    [JsonPropertyName("side")]
    public FuturesOrderSide Side { get; set; }
    /// <summary>
    /// ["<c>vol</c>"] Quantity
    /// </summary>
    [JsonPropertyName("vol")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>openType</c>"] Open type
    /// </summary>
    [JsonPropertyName("openType")]
    public MarginType OpenType { get; set; }
    /// <summary>
    /// ["<c>trend</c>"] Trigger price type
    /// </summary>
    [JsonPropertyName("trend")]
    public TriggerPriceType TriggerPriceType { get; set; }
    /// <summary>
    /// ["<c>activePrice</c>"] Activation price
    /// </summary>
    [JsonPropertyName("activePrice")]
    public decimal? ActivationPrice { get; set; }
    /// <summary>
    /// ["<c>markPrice</c>"] Mark price
    /// </summary>
    [JsonPropertyName("markPrice")]
    public decimal MarkPrice { get; set; }
    /// <summary>
    /// ["<c>backType</c>"] Callback type
    /// </summary>
    [JsonPropertyName("backType")]
    public CallbackType CallbackType { get; set; }
    /// <summary>
    /// ["<c>backValue</c>"] Callback value
    /// </summary>
    [JsonPropertyName("backValue")]
    public decimal CallbackValue { get; set; }
    /// <summary>
    /// ["<c>triggerPrice</c>"] Trigger price
    /// </summary>
    [JsonPropertyName("triggerPrice")]
    public decimal TriggerPrice { get; set; }
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public long OrderId { get; set; }
    /// <summary>
    /// ["<c>errorCode</c>"] Error code
    /// </summary>
    [JsonPropertyName("errorCode")]
    public int? ErrorCode { get; set; }
    /// <summary>
    /// ["<c>state</c>"] Status
    /// </summary>
    [JsonPropertyName("state")]
    public TpSlStatus Status { get; set; }
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
    /// ["<c>positionMode</c>"] Position mode
    /// </summary>
    [JsonPropertyName("positionMode")]
    public PositionMode PositionMode { get; set; }
    /// <summary>
    /// ["<c>reduceOnly</c>"] Reduce only
    /// </summary>
    [JsonPropertyName("reduceOnly")]
    public bool ReduceOnly { get; set; }
    /// <summary>
    /// ["<c>triggerType</c>"] Trigger type
    /// </summary>
    [JsonPropertyName("triggerType")]
    public TriggerType TriggerType { get; set; }
}

