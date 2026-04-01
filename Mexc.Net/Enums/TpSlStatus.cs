using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Mexc.Net.Enums;

/// <summary>
/// Take profit/stop loss status
/// </summary>
[JsonConverter(typeof(EnumConverter<TpSlStatus>))]
public enum TpSlStatus
{
    /// <summary>
    /// ["<c>1</c>"] Untriggered
    /// </summary>
    [Map("1")]
    Untriggered,
    /// <summary>
    /// ["<c>2</c>"] Canceled
    /// </summary>
    [Map("2")]
    Canceled,
    /// <summary>
    /// ["<c>3</c>"] Executed
    /// </summary>
    [Map("3")]
    Executed,
    /// <summary>
    /// ["<c>4</c>"] Invalidated
    /// </summary>
    [Map("4")]
    Invalidated,
    /// <summary>
    /// ["<c>5</c>"] Execution failed
    /// </summary>
    [Map("5")]
    Failed,
}
