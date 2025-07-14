using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Objects.Sockets.Models;
using ProtoBuf;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoContract]
    internal record ProtoStreamTrade
    {
        [ProtoMember(3)]
        public int Side { get; set; }
        [ProtoMember(1)]
        public string Price { get; set; } = string.Empty;
        [ProtoMember(2)]
        public string Quantity { get; set; } = string.Empty;
        [ProtoMember(4)]
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
