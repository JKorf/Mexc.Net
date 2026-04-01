using Mexc.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// Zero fee symbols
/// </summary>
public record MexcZeroFeeSymbols
{
    /// <summary>
    /// ["<c>contracts</c>"] Contracts
    /// </summary>
    [JsonPropertyName("contracts")]
    public MexcZeroFeeSymbol[] Contracts { get; set; } = [];
} 

/// <summary>
/// Zero fee rate symbol
/// </summary>
public record MexcZeroFeeSymbol
{
    /// <summary>
    /// ["<c>contractId</c>"] Contract id
    /// </summary>
    [JsonPropertyName("contractId")]
    public long? ContractId { get; set; }
    /// <summary>
    /// ["<c>ifHotTag</c>"] If hot tag
    /// </summary>
    [JsonPropertyName("ifHotTag")]
    public bool IfHotTag { get; set; }
    /// <summary>
    /// ["<c>feeRateType</c>"] Fee rate type
    /// </summary>
    [JsonPropertyName("feeRateType")]
    public string FeeRateType { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>sort</c>"] Sort
    /// </summary>
    [JsonPropertyName("sort")]
    public int? Sort { get; set; }
    /// <summary>
    /// ["<c>tempFeeRateEffectiveStartTime</c>"] Temp fee rate effective start time
    /// </summary>
    [JsonPropertyName("tempFeeRateEffectiveStartTime")]
    public DateTime? TempFeeRateEffectiveStartTime { get; set; }
}

