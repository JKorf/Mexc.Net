using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Mexc.Net.Enums;

/// <summary>
/// Self trade prevention mode
/// </summary>
[JsonConverter(typeof(EnumConverter<StpMode>))]
public enum StpMode
{
    /// <summary>
    /// ["<c>0</c>"] No action
    /// </summary>
    [Map("0")]
    NoAction,
    /// <summary>
    /// ["<c>1</c>"] Cancel both orders
    /// </summary>
    [Map("1")]
    CancelBoth,
    /// <summary>
    /// ["<c>2</c>"] Cancel the maker order
    /// </summary>
    [Map("2")]
    CancelMaker,
    /// <summary>
    /// ["<c>3</c>"] Cancel the taker order
    /// </summary>
    [Map("3")]
    CancelTaker,
}
