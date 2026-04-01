using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// Futures order result
/// </summary>
public record MexcFuturesBatchOrderResult
{
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public long OrderId { get; set; }
    /// <summary>
    /// ["<c>errorCode</c>"] Error code
    /// </summary>
    [JsonPropertyName("errorCode")]
    public int ErrorCode { get; set; }
}

