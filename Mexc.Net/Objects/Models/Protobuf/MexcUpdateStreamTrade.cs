using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [LightProto.ProtoContract]
    internal partial record MexcUpdateStreamTrade
    {
        [LightProto.ProtoMember(3)]
        public int Side { get; set; }
        [LightProto.ProtoMember(1)]
        public string Price { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string Quantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(4)]
        public long Timestamp { get; set; }

        public MexcStreamTrade ToModel()
        {
            return new MexcStreamTrade
            {
                Price = decimal.Parse(Price, NumberStyles.Float, CultureInfo.InvariantCulture),
                Quantity = decimal.Parse(Quantity, NumberStyles.Float, CultureInfo.InvariantCulture),
                Side = Side == 1 ? OrderSide.Buy : OrderSide.Sell,
                Timestamp = DateTimeConverter.ParseFromDouble(Timestamp)
            };
        }
    }
}
