using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Mexc.Net.Enums;

/// <summary>
/// Take profit/stop loss order type
/// </summary>
[JsonConverter(typeof(EnumConverter<TpSlType>))]
public enum TpSlType
{
    /// <summary>
    /// ["<c>0</c>"] Market order
    /// </summary>
    [Map("0")]
    Market,
    /// <summary>
    /// ["<c>1</c>"] Limit order
    /// </summary>
    [Map("1")]
    Limit,
}
