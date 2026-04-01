using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// User trade update
/// </summary>
public record MexcFuturesUserTradeUpdate
{
    /// <summary>
    /// ["<c>id</c>"] Trade id
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
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
    /// ["<c>price</c>"] Price
    /// </summary>
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    /// <summary>
    /// ["<c>feeCurrency</c>"] Fee asset
    /// </summary>
    [JsonPropertyName("feeCurrency")]
    public string FeeAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>fee</c>"] Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }
    /// <summary>
    /// ["<c>timestamp</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// ["<c>profit</c>"] Profit
    /// </summary>
    [JsonPropertyName("profit")]
    public decimal Profit { get; set; }
    /// <summary>
    /// ["<c>isTaker</c>"] Is taker
    /// </summary>
    [JsonPropertyName("isTaker")]
    public bool IsTaker { get; set; }
    [JsonInclude, JsonPropertyName("taker")]
    internal bool TakerInt { set => IsTaker = value; }

    /// <summary>
    /// ["<c>category</c>"] Order category
    /// </summary>
    [JsonPropertyName("category")]
    public OrderCategory Category { get; set; }
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public long OrderId { get; set; }
    /// <summary>
    /// ["<c>isSelf</c>"] Is self
    /// </summary>
    [JsonPropertyName("isSelf")]
    public bool IsSelf { get; set; }
    /// <summary>
    /// ["<c>externalOid</c>"] Client order id
    /// </summary>
    [JsonPropertyName("externalOid")]
    public string? ClientOrderId { get; set; }
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
    /// ["<c>opponentUid</c>"] Opponent uid
    /// </summary>
    [JsonPropertyName("opponentUid")]
    public long? OpponentUid { get; set; }
}

