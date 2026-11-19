using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesTpSlPrice : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string SymbolOrder { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string OrderId { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public int? LossTrend { get; set; }
        [LightProto.ProtoMember(4)]
        public int? ProfitTrend { get; set; }
        [LightProto.ProtoMember(5)]
        public string? StopLossPrice { get; set; }
        [LightProto.ProtoMember(6)]
        public string? TakeProfitPrice { get; set; }
        [LightProto.ProtoMember(7)]
        public int? EnsureStopLoss { get; set; }
        [LightProto.ProtoMember(8)]
        public string? ExtraTakerFeeRate { get; set; }

        public MexcTpSlPriceUpdate ToModel()
        {
            return new MexcTpSlPriceUpdate
            {
                Symbol = SymbolOrder,
                OrderId = long.TryParse(OrderId, NumberStyles.Integer, CultureInfo.InvariantCulture, out var orderId) ? orderId : 0,
                StopLossPriceType = LossTrend.HasValue ? EnumConverter.ParseString<TriggerPriceType>(LossTrend.Value.ToString())!.Value : default,
                TakeProfitPriceType = ProfitTrend.HasValue ? EnumConverter.ParseString<TriggerPriceType>(ProfitTrend.Value.ToString())!.Value : default,
                StopLossPrice = ExchangeHelpers.ParseDecimal(StopLossPrice) ?? 0,
                TakeProfitPrice = ExchangeHelpers.ParseDecimal(TakeProfitPrice) ?? 0,
                EnsureStopLoss = EnsureStopLoss switch { 0 => false, 1 => true, _ => null },
                ExtraTakerFeeRate = ExchangeHelpers.ParseDecimal(ExtraTakerFeeRate)
            };
        }
    }
}
