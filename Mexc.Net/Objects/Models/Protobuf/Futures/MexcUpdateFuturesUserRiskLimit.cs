using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesUserRiskLimit : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public int? Level { get; set; }
        [LightProto.ProtoMember(2)]
        public string? MaxQuantity { get; set; }
        [LightProto.ProtoMember(3)]
        public string? MaintenanceMarginRate { get; set; }
        [LightProto.ProtoMember(4)]
        public string? InitialMarginRate { get; set; }
        [LightProto.ProtoMember(5)]
        public int? MaxLeverage { get; set; }
        [LightProto.ProtoMember(6)]
        public string SymbolRiskLimit { get; set; } = string.Empty;
        [LightProto.ProtoMember(7)]
        public int PositionType { get; set; }
        [LightProto.ProtoMember(8)]
        public int RiskSource { get; set; }
        [LightProto.ProtoMember(9)]
        public int? Leverage { get; set; }
        [LightProto.ProtoMember(10)]
        public int OpenType { get; set; }
        [LightProto.ProtoMember(11)]
        public bool? LimitBySystem { get; set; }
        [LightProto.ProtoMember(12)]
        public string? MaxQuantityView { get; set; }

        public MexcRiskLimit ToModel()
        {
            return new MexcRiskLimit
            {
                Level = Level ?? 0,
                MaxQuantity = ExchangeHelpers.ParseDecimal(MaxQuantity) ?? 0,
                MaintenanceMarginRate = ExchangeHelpers.ParseDecimal(MaintenanceMarginRate) ?? 0,
                InitialMarginRate = ExchangeHelpers.ParseDecimal(InitialMarginRate) ?? 0,
                MaxLeverage = MaxLeverage ?? 0,
                Symbol = SymbolRiskLimit,
                PositionSide = EnumConverter.ParseString<PositionSide>(PositionType.ToString())!.Value,
                RiskSource = RiskSource,
                Leverage = Leverage ?? 0,
                MarginType = EnumConverter.ParseString<MarginType>(OpenType.ToString())!.Value,
                LimitBySystem = LimitBySystem ?? false
            };
        }
    }
}
