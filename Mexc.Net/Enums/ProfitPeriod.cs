using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Mexc.Net.Enums;

/// <summary>
/// Profit period
/// </summary>
[JsonConverter(typeof(EnumConverter<ProfitPeriod>))]
public enum ProfitPeriod
{
    /// <summary>
    /// ["<c>1</c>"] Day
    /// </summary>
    [Map("1")]
    Day,
    /// <summary>
    /// ["<c>2</c>"] Week
    /// </summary>
    [Map("2")]
    Week,
}
