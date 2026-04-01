using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Mexc.Net.Enums;

/// <summary>
/// Execute cycle
/// </summary>
[JsonConverter(typeof(EnumConverter<ExecuteCycle>))]
public enum ExecuteCycle
{
    /// <summary>
    /// ["<c>1</c>"] One day
    /// </summary>
    [Map("1")]
    OneDay,
    /// <summary>
    /// ["<c>2</c>"] One week
    /// </summary>
    [Map("2")]
    OneWeek,
}
