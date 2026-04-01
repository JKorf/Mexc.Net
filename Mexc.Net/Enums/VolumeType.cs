using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Mexc.Net.Enums;

/// <summary>
/// Volume type
/// </summary>
[JsonConverter(typeof(EnumConverter<VolumeType>))]
public enum VolumeType
{
    /// <summary>
    /// ["<c>1</c>"] Partial take profit/stop loss
    /// </summary>
    [Map("1")]
    PartialTpSl,
    /// <summary>
    /// ["<c>2</c>"] Position take profit/stop loss
    /// </summary>
    [Map("2")]
    PositionTpSl,
}
