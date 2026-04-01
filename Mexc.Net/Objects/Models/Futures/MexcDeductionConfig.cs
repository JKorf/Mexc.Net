using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// Deduction config
/// </summary>
public record MexcDeductionConfig
{
    /// <summary>
    /// ["<c>deductCoin</c>"] Deduct asset
    /// </summary>
    [JsonPropertyName("deductCoin")]
    public string DeductAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>settleCoin</c>"] Settle asset
    /// </summary>
    [JsonPropertyName("settleCoin")]
    public string SettleAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>discountRatio</c>"] Discount ratio
    /// </summary>
    [JsonPropertyName("discountRatio")]
    public decimal DiscountRatio { get; set; }
}

