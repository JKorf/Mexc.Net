using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Objects.Sockets.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoContract]
    internal record ProtoOrderBookUpdate : MexcStreamEvent
    {
        [ProtoMember(1)]
        public ProtoStreamBookEntry[] Asks { get; set; } = Array.Empty<ProtoStreamBookEntry>();
        [ProtoMember(2)]
        public ProtoStreamBookEntry[] Bids { get; set; } = Array.Empty<ProtoStreamBookEntry>();

        [ProtoMember(4)]
        public string Version { get; set; } = string.Empty;
        [ProtoMember(5)]
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
                Event = Event,
                Sequence = Version,
                SequenceEnd = Version2
            };
        }
    }
}
