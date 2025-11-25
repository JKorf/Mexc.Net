using Mexc.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoBuf.ProtoContract]
    [LightProto.ProtoContract]
    internal partial class MexcUpdateMiniTicker : MexcUpdate
    {
        [ProtoBuf.ProtoMember(1)]
        [LightProto.ProtoMember(1)]
        public string SymbolInt { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(2)]
        [LightProto.ProtoMember(2)]
        public string Price { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(3)]
        [LightProto.ProtoMember(3)]
        public string Rate { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(4)]
        [LightProto.ProtoMember(4)]
        public string ZonedRate { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(5)]
        [LightProto.ProtoMember(5)]
        public string High { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(6)]
        [LightProto.ProtoMember(6)]
        public string Low { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(7)]
        [LightProto.ProtoMember(7)]
        public string Volume { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(8)]
        [LightProto.ProtoMember(8)]
        public string Quantity { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(9)]
        [LightProto.ProtoMember(9)]
        public string LastCloseRate { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(10)]
        [LightProto.ProtoMember(10)]
        public string LastCloseZonedRate { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(11)]
        [LightProto.ProtoMember(11)]
        public string LastCloseHigh { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(12)]
        [LightProto.ProtoMember(12)]
        public string LastCloseLow { get; set; } = string.Empty;

        public MexcMiniTickUpdate ToModel()
        {
            return new MexcMiniTickUpdate
            {
                LastPrice = ExchangeHelpers.ParseDecimal(Price) ?? 0,
                PriceChangePercentage = ExchangeHelpers.ParseDecimal(ZonedRate) ?? 0,
                PriceChangePercentageUtc8 = ExchangeHelpers.ParseDecimal(Rate) ?? 0,
                HighPrice = ExchangeHelpers.ParseDecimal(High) ?? 0,
                LowPrice = ExchangeHelpers.ParseDecimal(Low) ?? 0,
                QuoteVolume = ExchangeHelpers.ParseDecimal(Volume) ?? 0,
                Volume = ExchangeHelpers.ParseDecimal(Quantity) ?? 0,
                Symbol = SymbolInt
            };
        }
    }
}
