using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesBalance : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string Asset { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string AvailableBalance { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public string FrozenBalance { get; set; } = string.Empty;
        [LightProto.ProtoMember(4)]
        public string PositionMargin { get; set; } = string.Empty;
        [LightProto.ProtoMember(5)]
        public string Bonus { get; set; } = string.Empty;

        public MexcFuturesBalanceUpdate ToModel()
        {
            return new MexcFuturesBalanceUpdate
            {
                Asset = Asset,
                AvailableBalance = ExchangeHelpers.ParseDecimal(AvailableBalance) ?? 0,
                FrozenBalance = ExchangeHelpers.ParseDecimal(FrozenBalance) ?? 0,
                PositionMargin = ExchangeHelpers.ParseDecimal(PositionMargin) ?? 0,
                Bonus = ExchangeHelpers.ParseDecimal(Bonus) ?? 0
            };
        }
    }
}
