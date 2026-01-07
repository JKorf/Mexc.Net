using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [LightProto.ProtoContract]
    internal partial record MexcUpdateBookTicker
    {
        [LightProto.ProtoMember(1)]
        public string BidPrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string BidQuantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public string AskPrice { get; set; } = string.Empty;
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
