using System;
using System.Text.Json.Serialization;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models;

/// <summary>
/// Take profit/stop loss order
/// </summary>
public record MexcTpSlOrder
{
    /// <summary>
    /// ["<c>id</c>"] Tp/Sl order id
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public long OrderId { get; set; }
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>positionId</c>"] Position id
    /// </summary>
    [JsonPropertyName("positionId")]
    public long PositionId { get; set; }
    /// <summary>
    /// ["<c>lossTrend</c>"] Stop loss trigger price type
    /// </summary>
    [JsonPropertyName("lossTrend")]
    public TriggerPriceType StopLossPriceType { get; set; }
    /// <summary>
    /// ["<c>profitTrend</c>"] Take profit trigger price type
    /// </summary>
    [JsonPropertyName("profitTrend")]
    public TriggerPriceType TakeProfitPriceType { get; set; }
    /// <summary>
    /// ["<c>stopLossPrice</c>"] Stop loss price
    /// </summary>
    [JsonPropertyName("stopLossPrice")]
    public decimal? StopLossPrice { get; set; }
    /// <summary>
    /// ["<c>takeProfitPrice</c>"] Take profit price
    /// </summary>
    [JsonPropertyName("takeProfitPrice")]
    public decimal? TakeProfitPrice { get; set; }
    /// <summary>
    /// ["<c>state</c>"] Status
    /// </summary>
    [JsonPropertyName("state")]
    public TpSlStatus Status { get; set; }
    /// <summary>
    /// ["<c>triggerSide</c>"] Trigger side
    /// </summary>
    [JsonPropertyName("triggerSide")]
    public TriggerSide TriggerSide { get; set; }
    /// <summary>
    /// ["<c>positionType</c>"] Position side
    /// </summary>
    [JsonPropertyName("positionType")]
    public PositionSide PositionSide { get; set; }
    /// <summary>
    /// ["<c>vol</c>"] Quantity
    /// </summary>
    [JsonPropertyName("vol")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>realityVol</c>"] Quantity placed
    /// </summary>
    [JsonPropertyName("realityVol")]
    public decimal? QuantityPlaced { get; set; }
    /// <summary>
    /// ["<c>placeOrderId</c>"] Placed order id
    /// </summary>
    [JsonPropertyName("placeOrderId")]
    public long PlacedOrderId { get; set; }
    /// <summary>
    /// ["<c>errorCode</c>"] Error code
    /// </summary>
    [JsonPropertyName("errorCode")]
    public int ErrorCode { get; set; }
    /// <summary>
    /// ["<c>version</c>"] Version
    /// </summary>
    [JsonPropertyName("version")]
    public long Version { get; set; }
    /// <summary>
    /// ["<c>isFinished</c>"] Is finished
    /// </summary>
    [JsonPropertyName("isFinished")]
    public bool IsFinished { get; set; }
    /// <summary>
    /// ["<c>priceProtect</c>"] Price protect
    /// </summary>
    [JsonPropertyName("priceProtect")]
    public bool PriceProtect { get; set; }
    /// <summary>
    /// ["<c>profitLossVolType</c>"] Profit loss vol type
    /// </summary>
    [JsonPropertyName("profitLossVolType")]
    public ProfitLossVolumeType? ProfitLossVolType { get; set; }
    /// <summary>
    /// ["<c>takeProfitVol</c>"] Take profit quantity
    /// </summary>
    [JsonPropertyName("takeProfitVol")]
    public decimal? TakeProfitQuantity { get; set; }
    /// <summary>
    /// ["<c>stopLossVol</c>"] Stop loss quantity
    /// </summary>
    [JsonPropertyName("stopLossVol")]
    public decimal? StopLossQuantity { get; set; }
    /// <summary>
    /// ["<c>createTime</c>"] Create time
    /// </summary>
    [JsonPropertyName("createTime")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// ["<c>updateTime</c>"] Update time
    /// </summary>
    [JsonPropertyName("updateTime")]
    public DateTime? UpdateTime { get; set; }
    /// <summary>
    /// ["<c>volType</c>"] Volume type
    /// </summary>
    [JsonPropertyName("volType")]
    public VolumeType? VolumeType { get; set; }
    /// <summary>
    /// ["<c>takeProfitReverse</c>"] Take profit reverse
    /// </summary>
    [JsonPropertyName("takeProfitReverse")]
    public int TakeProfitReverse { get; set; }
    /// <summary>
    /// ["<c>stopLossReverse</c>"] Stop loss reverse
    /// </summary>
    [JsonPropertyName("stopLossReverse")]
    public int StopLossReverse { get; set; }
    /// <summary>
    /// ["<c>closeTryTimes</c>"] Close try times
    /// </summary>
    [JsonPropertyName("closeTryTimes")]
    public int? CloseTryTimes { get; set; }
    /// <summary>
    /// ["<c>reverseTryTimes</c>"] Reverse try times
    /// </summary>
    [JsonPropertyName("reverseTryTimes")]
    public int? ReverseTryTimes { get; set; }
    /// <summary>
    /// ["<c>reverseErrorCode</c>"] Reverse error code
    /// </summary>
    [JsonPropertyName("reverseErrorCode")]
    public int ReverseErrorCode { get; set; }
    /// <summary>
    /// ["<c>takeProfitType</c>"] Take profit type
    /// </summary>
    [JsonPropertyName("takeProfitType")]
    public TpSlType? TakeProfitType { get; set; }
    /// <summary>
    /// ["<c>stopLossType</c>"] Stop loss type
    /// </summary>
    [JsonPropertyName("stopLossType")]
    public TpSlType? StopLossType { get; set; }
    /// <summary>
    /// ["<c>stopLossOrderPrice</c>"] Stop loss order price
    /// </summary>
    [JsonPropertyName("stopLossOrderPrice")]
    public decimal? StopLossOrderPrice { get; set; }
}

