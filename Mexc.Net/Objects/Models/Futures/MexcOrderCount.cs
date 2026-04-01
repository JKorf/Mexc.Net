using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// Order counts
/// </summary>
public record MexcOrderCount
{
    /// <summary>
    /// ["<c>sumCount</c>"] Total count
    /// </summary>
    [JsonPropertyName("sumCount")]
    public int TotalCount { get; set; }
    /// <summary>
    /// ["<c>limitOrderCount</c>"] Limit order count
    /// </summary>
    [JsonPropertyName("limitOrderCount")]
    public int LimitOrderCount { get; set; }
    /// <summary>
    /// ["<c>stopOrderCount</c>"] Stop order count
    /// </summary>
    [JsonPropertyName("stopOrderCount")]
    public int StopOrderCount { get; set; }
    /// <summary>
    /// ["<c>planOrderCount</c>"] Plan order count
    /// </summary>
    [JsonPropertyName("planOrderCount")]
    public int PlanOrderCount { get; set; }
    /// <summary>
    /// ["<c>trackOrderCount</c>"] Track order count
    /// </summary>
    [JsonPropertyName("trackOrderCount")]
    public int TrackOrderCount { get; set; }
}

