namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesLiquidationRisk : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string SymbolRisk { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public long? PositionId { get; set; }
        [LightProto.ProtoMember(3)]
        public string? LiquidatePrice { get; set; }
        [LightProto.ProtoMember(4)]
        public string? MarginRatio { get; set; }
        [LightProto.ProtoMember(5)]
        public int AdlLevel { get; set; }

        public MexcLiquidationRiskUpdate ToModel()
        {
            return new MexcLiquidationRiskUpdate
            {
                Symbol = SymbolRisk,
                PositionId = PositionId ?? 0,
                LiquidatePrice = ExchangeHelpers.ParseDecimal(LiquidatePrice) ?? 0,
                MarginRatio = ExchangeHelpers.ParseDecimal(MarginRatio) ?? 0,
                AdlLevel = AdlLevel
            };
        }
    }
}
