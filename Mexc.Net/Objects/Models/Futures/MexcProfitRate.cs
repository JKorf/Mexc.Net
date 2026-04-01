using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// Profit rate
/// </summary>
public record MexcProfitRate
{
    /// <summary>
    /// ["<c>ranking</c>"] Ranking
    /// </summary>
    [JsonPropertyName("ranking")]
    public decimal Ranking { get; set; }
    /// <summary>
    /// ["<c>profitRate</c>"] Profit rate
    /// </summary>
    [JsonPropertyName("profitRate")]
    public decimal ProfitRate { get; set; }
    /// <summary>
    /// ["<c>statisticTime</c>"] Statistic time
    /// </summary>
    [JsonPropertyName("statisticTime")]
    public DateTime StatisticTime { get; set; }
}

