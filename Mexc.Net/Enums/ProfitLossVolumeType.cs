using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Mexc.Net.Enums;

/// <summary>
/// Profit loss volume type
/// </summary>
[JsonConverter(typeof(EnumConverter<ProfitLossVolumeType>))]
public enum ProfitLossVolumeType
{
    /// <summary>
    /// ["<c>SAME</c>"] Same quantities
    /// </summary>
    [Map("SAME")]
    Same,
    /// <summary>
    /// ["<c>SEPARATE</c>"] Separate quantities
    /// </summary>
    [Map("SEPARATE")]
    Separate,
}
