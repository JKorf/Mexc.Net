using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesTpSlOrder : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public long Id { get; set; }
        [LightProto.ProtoMember(2)]
        public string OrderId { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public string SymbolOrder { get; set; } = string.Empty;
        [LightProto.ProtoMember(4)]
        public string? PositionId { get; set; }
        [LightProto.ProtoMember(5)]
        public int? LossTrend { get; set; }
        [LightProto.ProtoMember(6)]
        public int? ProfitTrend { get; set; }
        [LightProto.ProtoMember(7)]
        public string? StopLossPrice { get; set; }
        [LightProto.ProtoMember(8)]
        public string? TakeProfitPrice { get; set; }
        [LightProto.ProtoMember(9)]
        public int State { get; set; }
        [LightProto.ProtoMember(10)]
        public int TriggerSide { get; set; }
        [LightProto.ProtoMember(11)]
        public int PositionType { get; set; }
        [LightProto.ProtoMember(12)]
        public string? Quantity { get; set; }
        [LightProto.ProtoMember(13)]
        public string? QuantityPlaced { get; set; }
        [LightProto.ProtoMember(14)]
        public string? PlacedOrderId { get; set; }
        [LightProto.ProtoMember(15)]
        public int? ErrorCode { get; set; }
        [LightProto.ProtoMember(16)]
        public int? Version { get; set; }
        [LightProto.ProtoMember(17)]
        public int? IsFinished { get; set; }
        [LightProto.ProtoMember(18)]
        public string? ProfitLossVolType { get; set; }
        [LightProto.ProtoMember(19)]
        public string? TakeProfitQuantity { get; set; }
        [LightProto.ProtoMember(20)]
        public string? StopLossQuantity { get; set; }
        [LightProto.ProtoMember(21)]
        public long? CreateTimeOrder { get; set; }
        [LightProto.ProtoMember(22)]
        public long? UpdateTime { get; set; }
        [LightProto.ProtoMember(23)]
        public int? VolumeType { get; set; }
        [LightProto.ProtoMember(24)]
        public int? TakeProfitReverse { get; set; }
        [LightProto.ProtoMember(25)]
        public int? StopLossReverse { get; set; }
        [LightProto.ProtoMember(26)]
        public int? CloseTryTimes { get; set; }
        [LightProto.ProtoMember(27)]
        public int? ReverseTryTimes { get; set; }
        [LightProto.ProtoMember(28)]
        public int? ReverseErrorCode { get; set; }
        [LightProto.ProtoMember(29)]
        public int? StopLossType { get; set; }
        [LightProto.ProtoMember(30)]
        public string? StopLossOrderPrice { get; set; }
        [LightProto.ProtoMember(31)]
        public int? EnsureStopLoss { get; set; }
        [LightProto.ProtoMember(32)]
        public string? ExtraTakerFeeRate { get; set; }

        public MexcTpSlOrder ToModel()
        {
            return new MexcTpSlOrder
            {
                Id = Id,
                OrderId = long.TryParse(OrderId, NumberStyles.Integer, CultureInfo.InvariantCulture, out var orderId) ? orderId : 0,
                Symbol = SymbolOrder,
                PositionId = long.TryParse(PositionId, NumberStyles.Integer, CultureInfo.InvariantCulture, out var positionId) ? positionId : 0,
                StopLossPriceType = LossTrend.HasValue ? EnumConverter.ParseString<TriggerPriceType>(LossTrend.Value.ToString())!.Value : default,
                TakeProfitPriceType = ProfitTrend.HasValue ? EnumConverter.ParseString<TriggerPriceType>(ProfitTrend.Value.ToString())!.Value : default,
                StopLossPrice = ExchangeHelpers.ParseDecimal(StopLossPrice),
                TakeProfitPrice = ExchangeHelpers.ParseDecimal(TakeProfitPrice),
                Status = EnumConverter.ParseString<TpSlStatus>(State.ToString())!.Value,
                TriggerSide = EnumConverter.ParseString<TriggerSide>(TriggerSide.ToString())!.Value,
                PositionSide = EnumConverter.ParseString<PositionSide>(PositionType.ToString())!.Value,
                Quantity = ExchangeHelpers.ParseDecimal(Quantity) ?? 0,
                QuantityPlaced = ExchangeHelpers.ParseDecimal(QuantityPlaced),
                PlacedOrderId = long.TryParse(PlacedOrderId, NumberStyles.Integer, CultureInfo.InvariantCulture, out var placedOrderId) ? placedOrderId : 0,
                ErrorCode = ErrorCode ?? 0,
                Version = Version ?? 0,
                IsFinished = IsFinished == 1,
                ProfitLossVolType = ProfitLossVolType == null ? null : EnumConverter.ParseString<ProfitLossVolumeType>(ProfitLossVolType),
                TakeProfitQuantity = ExchangeHelpers.ParseDecimal(TakeProfitQuantity),
                StopLossQuantity = ExchangeHelpers.ParseDecimal(StopLossQuantity),
                CreateTime = CreateTimeOrder.HasValue ? DateTimeConverter.ParseFromDecimal(CreateTimeOrder.Value) : default,
                UpdateTime = UpdateTime.HasValue ? DateTimeConverter.ParseFromDecimal(UpdateTime.Value) : null,
                VolumeType = VolumeType.HasValue ? EnumConverter.ParseString<VolumeType>(VolumeType.Value.ToString()) : null,
                TakeProfitReverse = TakeProfitReverse ?? 0,
                StopLossReverse = StopLossReverse ?? 0,
                CloseTryTimes = CloseTryTimes,
                ReverseTryTimes = ReverseTryTimes,
                ReverseErrorCode = ReverseErrorCode ?? 0,
                StopLossType = StopLossType.HasValue ? EnumConverter.ParseString<TpSlType>(StopLossType.Value.ToString()) : null,
                StopLossOrderPrice = ExchangeHelpers.ParseDecimal(StopLossOrderPrice)
            };
        }
    }
}
