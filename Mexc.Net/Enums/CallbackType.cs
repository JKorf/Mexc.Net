using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Mexc.Net.Enums;

/// <summary>
/// Callback type
/// </summary>
[JsonConverter(typeof(EnumConverter<CallbackType>))]
public enum CallbackType
{
    /// <summary>
    /// ["<c>1</c>"] Percentage value
    /// </summary>
    [Map("1")]
    Percentage,
    /// <summary>
    /// ["<c>2</c>"] Absolute value
    /// </summary>
    [Map("2")]
    Absolute,
}
