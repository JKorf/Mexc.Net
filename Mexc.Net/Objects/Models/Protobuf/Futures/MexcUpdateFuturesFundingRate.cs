using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesFundingRate : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string SymbolRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string FundingRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public long NextSettleTime { get; set; }
        [LightProto.ProtoMember(4)]
        public long FundingType { get; set; }

        public MexcFundingRateUpdate ToModel()
        {
            return new MexcFundingRateUpdate
            {
                Symbol = SymbolRate,
                FundingRate = ExchangeHelpers.ParseDecimal(FundingRate) ?? 0,
                NextSettleTime = DateTimeConverter.ParseFromDouble(NextSettleTime),
            };
        }
    }
}
