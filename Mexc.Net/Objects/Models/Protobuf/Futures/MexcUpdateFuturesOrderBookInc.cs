using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesOrderBookInc : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public MexcUpdateFuturesOrderBookItem[] Asks { get; set; } = [];
        [LightProto.ProtoMember(2)]
        public MexcUpdateFuturesOrderBookItem[] Bids { get; set; } = [];

        [LightProto.ProtoMember(3)]
        public long BeginVersion { get; set; }
        [LightProto.ProtoMember(4)]
        public long EndVersion { get; set; }
        [LightProto.ProtoMember(5)]
        public long Timestamp { get; set; }

        public MexcFuturesOrderBook ToModel()
        {
            return new MexcFuturesOrderBook
            {
                Asks = Asks.Select(a => new MexcOrderBookFuturesEntry
                {
                    Price = ExchangeHelpers.ParseDecimal(a.Price) ?? 0,
                    Quantity = ExchangeHelpers.ParseDecimal(a.Volume) ?? 0,
                    Count = a.Count
                }).ToArray(),
                Bids = Bids.Select(b => new MexcOrderBookFuturesEntry
                {
                    Price = ExchangeHelpers.ParseDecimal(b.Price) ?? 0,
                    Quantity = ExchangeHelpers.ParseDecimal(b.Volume) ?? 0,
                    Count = b.Count
                }).ToArray(),
                Timestamp = DateTimeConverter.ParseFromDecimal(Timestamp),
                SequenceStart = BeginVersion,
                SequenceEnd = EndVersion,
                Version = EndVersion
            };
        }
    }
}
