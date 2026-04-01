using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Mexc.Net.Enums;

/// <summary>
/// Limit order type
/// </summary>
[JsonConverter(typeof(EnumConverter<LimitOrderType>))]
public enum LimitOrderType
{
    /// <summary>
    /// ["<c>0</c>"] Not BBO
    /// </summary>
    [Map("0")]
    NotBbo,
    /// <summary>
    /// ["<c>1</c>"] Opposite 1
    /// </summary>
    [Map("1")]
    Opposite1,
    /// <summary>
    /// ["<c>2</c>"] Opposite 5
    /// </summary>
    [Map("2")]
    Opposite5,
    /// <summary>
    /// ["<c>3</c>"] Same side 1
    /// </summary>
    [Map("3")]
    SameSide1,
    /// <summary>
    /// ["<c>4</c>"] Same side 5
    /// </summary>
    [Map("4")]
    SameSide5,
}
