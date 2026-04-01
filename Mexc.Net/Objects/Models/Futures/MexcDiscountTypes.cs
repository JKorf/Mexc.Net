using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// 
/// </summary>
public record MexcDiscountTypes
{
    /// <summary>
    /// ["<c>useFeeDiscount</c>"] Use fee discount
    /// </summary>
    [JsonPropertyName("useFeeDiscount")]
    public bool UseFeeDiscount { get; set; }
    /// <summary>
    /// ["<c>useFeeDeduct</c>"] Use fee deduct
    /// </summary>
    [JsonPropertyName("useFeeDeduct")]
    public bool UseFeeDeduct { get; set; }
}

