using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesLiquidation : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public int Type { get; set; }
        [LightProto.ProtoMember(2)]
        public MexcFuturesLiquidationData? Param { get; set; }

        public MexcLiquidationUpdate ToModel()
        {
            return new MexcLiquidationUpdate
            {
                Type = Type,
                Param = Param!.ToModel()
            };
        }
    }

    [LightProto.ProtoContract]
    internal partial class MexcFuturesLiquidationData
    {
        [LightProto.ProtoMember(1)]
        public int NotifyType { get; set; }
        [LightProto.ProtoMember(2)]
        public int OpenType { get; set; }
        [LightProto.ProtoMember(3)]
        public string? SymbolDisplayName { get; set; }
        [LightProto.ProtoMember(4)]
        public string? SymbolDisplayNameEnglish { get; set; }
        [LightProto.ProtoMember(5)]
        public bool MultiAssets { get; set; }
        [LightProto.ProtoMember(6)]
        public string? MarginRate { get; set; }

        public MexcLiquidationUpdateData ToModel()
        {
            return new MexcLiquidationUpdateData
            {
                NotifyType = EnumConverter.ParseString<LiquidationNotifyType>(NotifyType.ToString())!.Value,
                MarginType = EnumConverter.ParseString<MarginType>(OpenType.ToString())!.Value,
                SymbolDisplayName = SymbolDisplayName ?? string.Empty,
                SymbolDisplayNameEnglish = SymbolDisplayNameEnglish ?? string.Empty,
                MultiAssets = MultiAssets,
                MarginRate = ExchangeHelpers.ParseDecimal(MarginRate) ?? 0
            };
        }
    }
}
