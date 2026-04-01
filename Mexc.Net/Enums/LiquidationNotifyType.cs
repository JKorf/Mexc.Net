using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Mexc.Net.Enums;

/// <summary>
/// Liquidation notify type
/// </summary>
[JsonConverter(typeof(EnumConverter<LiquidationNotifyType>))]
public enum LiquidationNotifyType
{
    /// <summary>
    /// ["<c>1</c>"] Liquidation
    /// </summary>
    [Map("1")]
    Liquidation,
    /// <summary>
    /// ["<c>2</c>"] Warning for liquidation
    /// </summary>
    [Map("2")]
    LiquidationWarning,
}
