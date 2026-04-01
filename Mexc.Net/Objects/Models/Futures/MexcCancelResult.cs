using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// Cancel result
/// </summary>
public record MexcCancelResult
{
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public long? OrderId { get; set; }
    /// <summary>
    /// ["<c>errorCode</c>"] Error code
    /// </summary>
    [JsonPropertyName("errorCode")]
    public decimal ErrorCode { get; set; }
    /// <summary>
    /// ["<c>errorMsg</c>"] Error msg
    /// </summary>
    [JsonPropertyName("errorMsg")]
    public string ErrorMsg { get; set; } = string.Empty;
}

