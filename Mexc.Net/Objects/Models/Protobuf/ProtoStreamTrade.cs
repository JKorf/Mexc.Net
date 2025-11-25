using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoBuf.ProtoContract]
    [LightProto.ProtoContract]
    internal partial record ProtoStreamTrade
    {
        [ProtoBuf.ProtoMember(3)]
        [LightProto.ProtoMember(3)]
        public int Side { get; set; }
        [ProtoBuf.ProtoMember(1)]
        [LightProto.ProtoMember(1)]
        public string Price { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(2)]
        [LightProto.ProtoMember(2)]
        public string Quantity { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(4)]
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
