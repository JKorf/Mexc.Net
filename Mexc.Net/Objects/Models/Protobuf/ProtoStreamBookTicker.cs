using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoBuf.ProtoContract]
    [LightProto.ProtoContract]
    internal partial record ProtoStreamBookTicker
    {
        [ProtoBuf.ProtoMember(1)]
        [LightProto.ProtoMember(1)]
        public string BidPrice { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(2)]
        [LightProto.ProtoMember(2)]
        public string BidQuantity { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(3)]
        [LightProto.ProtoMember(3)]
        public string AskPrice { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(4)]
        [LightProto.ProtoMember(4)]
        public string AskQuantity { get; set; } = string.Empty;


        public MexcStreamBookTick ToModel()
        {
            return new MexcStreamBookTick
            {
                BestAskPrice = ExchangeHelpers.ParseDecimal(AskPrice) ?? 0,
                BestAskQuantity = ExchangeHelpers.ParseDecimal(AskQuantity) ?? 0,
                BestBidQuantity = ExchangeHelpers.ParseDecimal(BidQuantity) ?? 0,
                BestBidPrice = ExchangeHelpers.ParseDecimal(BidPrice) ?? 0,
            };
        }
    }
}
