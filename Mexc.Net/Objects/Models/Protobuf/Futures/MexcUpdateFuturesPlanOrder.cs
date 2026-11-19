using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesPlanOrder : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string? Id { get; set; }
        [LightProto.ProtoMember(2)]
        public string SymbolOrder { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public int? Leverage { get; set; }
        [LightProto.ProtoMember(4)]
        public int Side { get; set; }
        [LightProto.ProtoMember(5)]
        public string? TriggerPrice { get; set; }
        [LightProto.ProtoMember(6)]
        public string? Price { get; set; }
        [LightProto.ProtoMember(7)]
        public string? Quantity { get; set; }
        [LightProto.ProtoMember(8)]
        public int? OpenType { get; set; }
        [LightProto.ProtoMember(9)]
        public int TriggerType { get; set; }
        [LightProto.ProtoMember(10)]
        public int State { get; set; }
        [LightProto.ProtoMember(11)]
        public int? ExecuteCycle { get; set; }
        [LightProto.ProtoMember(12)]
        public int Trend { get; set; }
        [LightProto.ProtoMember(13)]
        public int OrderType { get; set; }
        [LightProto.ProtoMember(14)]
        public string? OrderId { get; set; }
        [LightProto.ProtoMember(15)]
        public int? ErrorCode { get; set; }
        [LightProto.ProtoMember(16)]
        public int? MarketOrderLevel { get; set; }
        [LightProto.ProtoMember(17)]
        public long CreateTimeOrder { get; set; }
        [LightProto.ProtoMember(18)]
        public long UpdateTime { get; set; }
        [LightProto.ProtoMember(19)]
        public int? PositionMode { get; set; }
        [LightProto.ProtoMember(20)]
        public int? LossTrend { get; set; }
        [LightProto.ProtoMember(21)]
        public int? ProfitTrend { get; set; }
        [LightProto.ProtoMember(22)]
        public string? StopLossPrice { get; set; }
        [LightProto.ProtoMember(23)]
        public string? TakeProfitPrice { get; set; }
        [LightProto.ProtoMember(24)]
        public bool ReduceOnly { get; set; }
        [LightProto.ProtoMember(25)]
        public int? EnsureStopLoss { get; set; }
        [LightProto.ProtoMember(26)]
        public string? ExtraTakerFeeRate { get; set; }

        public MexcPlanOrderUpdate ToModel()
        {
            return new MexcPlanOrderUpdate
            {
                Id = long.TryParse(Id, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id) ? id : 0,
                Symbol = SymbolOrder,
                Leverage = Leverage ?? 0,
                Side = EnumConverter.ParseString<FuturesOrderSide>(Side.ToString())!.Value,
                TriggerPrice = ExchangeHelpers.ParseDecimal(TriggerPrice),
                Price = ExchangeHelpers.ParseDecimal(Price),
                Quantity = ExchangeHelpers.ParseDecimal(Quantity) ?? 0,
                MarginType = OpenType.HasValue ? EnumConverter.ParseString<MarginType>(OpenType.Value.ToString()) : null,
                TriggerType = EnumConverter.ParseString<TriggerType>(TriggerType.ToString())!.Value,
                Status = EnumConverter.ParseString<TpSlStatus>(State.ToString())!.Value,
                ExecuteCycle = ExecuteCycle switch
                {
                    24 => Enums.ExecuteCycle.OneDay,
                    168 => Enums.ExecuteCycle.OneWeek,
                    _ => null
                },
                TriggerPriceType = EnumConverter.ParseString<TriggerPriceType>(Trend.ToString())!.Value,
                OrderType = EnumConverter.ParseString<FuturesOrderType>(OrderType.ToString())!.Value,
                OrderId = long.TryParse(OrderId, NumberStyles.Integer, CultureInfo.InvariantCulture, out var orderId) ? orderId : 0,
                ErrorCode = ErrorCode ?? 0,
                MarketOrderLevel = MarketOrderLevel ?? 0,
                CreateTime = DateTimeConverter.ParseFromDecimal(CreateTimeOrder),
                UpdateTime = DateTimeConverter.ParseFromDecimal(UpdateTime),
                PositionMode = PositionMode.HasValue ? EnumConverter.ParseString<PositionMode>(PositionMode.Value.ToString()) : null,
                StopLossPriceType = LossTrend.HasValue ? EnumConverter.ParseString<TriggerPriceType>(LossTrend.Value.ToString()) : null,
                TakeProfitPriceType = ProfitTrend.HasValue ? EnumConverter.ParseString<TriggerPriceType>(ProfitTrend.Value.ToString()) : null,
                StopLossPrice = ExchangeHelpers.ParseDecimal(StopLossPrice),
                TakeProfitPrice = ExchangeHelpers.ParseDecimal(TakeProfitPrice),
                ReduceOnly = ReduceOnly,
                EnsureStopLoss = EnsureStopLoss switch { 0 => false, 1 => true, _ => null },
                ExtraTakerFeeRate = ExchangeHelpers.ParseDecimal(ExtraTakerFeeRate)
            };
        }
    }
}
