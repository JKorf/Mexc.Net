using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]
    internal partial class MexcUpdateOrderBookLimit : MexcUpdate
    {
        [ProtoBuf.ProtoMember(1)]
        [LightProto.ProtoMember(1)]
        public MexcUpdateStreamBookEntry[] Asks { get; set; } = Array.Empty<MexcUpdateStreamBookEntry>();
        [ProtoBuf.ProtoMember(2)]
        [LightProto.ProtoMember(2)]
        public MexcUpdateStreamBookEntry[] Bids { get; set; } = Array.Empty<MexcUpdateStreamBookEntry>();

        [ProtoBuf.ProtoMember(4)]
        [LightProto.ProtoMember(4)]
        public string Version { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(5)]
        [LightProto.ProtoMember(5)]
        public string? Version2 { get; set; } = string.Empty;

        public MexcStreamOrderBook ToModel()
        {
            return new MexcStreamOrderBook
            {
                Asks = Asks.Select(x => new MexcStreamOrderBookEntry
                {
                    Price = ExchangeHelpers.ParseDecimal(x.Price) ?? 0,
                    Quantity = ExchangeHelpers.ParseDecimal(x.Quantity) ?? 0
                }).ToArray(),
                Bids = Bids.Select(x => new MexcStreamOrderBookEntry
                {
                    Price = ExchangeHelpers.ParseDecimal(x.Price) ?? 0,
                    Quantity = ExchangeHelpers.ParseDecimal(x.Quantity) ?? 0
                }).ToArray(),
                Sequence = Version,
                SequenceEnd = Version2
            };
        }
    }
}
