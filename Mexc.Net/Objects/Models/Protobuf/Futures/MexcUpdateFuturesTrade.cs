using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesTrade : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string Price { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string Quantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public int Side { get; set; }
        [LightProto.ProtoMember(5)]
        public int SelfTrade { get; set; }
        [LightProto.ProtoMember(6)]
        public long Timestamp { get; set; }

        public MexcFuturesTrade ToModel()
        {
            return new MexcFuturesTrade
            {
                Price = decimal.Parse(Price, NumberStyles.Float, CultureInfo.InvariantCulture),
                Quantity = decimal.Parse(Quantity, NumberStyles.Float, CultureInfo.InvariantCulture),
                Side = Side == 1 ? OrderSide.Buy : OrderSide.Sell,
                Timestamp = DateTimeConverter.ParseFromDouble(Timestamp),
                SelfTransaction = SelfTrade == 1 ? FuturesBool.Yes : FuturesBool.No,
                
            };
        }
    }
}
