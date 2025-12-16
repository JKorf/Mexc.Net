using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]
    internal partial class MexcUpdateKlines : MexcUpdate
    {
        /// <summary>
        /// Start time
        /// </summary>
        [LightProto.ProtoMember(2)]
        [ProtoBuf.ProtoMember(2)]
        public long StartTime { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [LightProto.ProtoMember(9)]
        [ProtoBuf.ProtoMember(9)]
        public long EndTime { get; set; }
        /// <summary>
        /// Volume in quote asset
        /// </summary>
        [LightProto.ProtoMember(8)]
        [ProtoBuf.ProtoMember(8)]
        public string QuoteVolume { get; set; } = string.Empty;
        /// <summary>
        /// Close price
        /// </summary>
        [LightProto.ProtoMember(4)]
        [ProtoBuf.ProtoMember(4)]
        public string ClosePrice { get; set; } = string.Empty;
        /// <summary>
        /// Highest price
        /// </summary>
        [LightProto.ProtoMember(5)]
        [ProtoBuf.ProtoMember(5)]
        public string HighPrice { get; set; } = string.Empty;
        /// <summary>
        /// Interval
        /// </summary>
        [LightProto.ProtoMember(1)]
        [ProtoBuf.ProtoMember(1)]
        public string Interval { get; set; } = string.Empty;
        /// <summary>
        /// Lowest price
        /// </summary>
        [LightProto.ProtoMember(6)]
        [ProtoBuf.ProtoMember(6)]
        public string LowPrice { get; set; } = string.Empty;
        /// <summary>
        /// Open price
        /// </summary>
        [LightProto.ProtoMember(3)]
        [ProtoBuf.ProtoMember(3)]
        public string OpenPrice { get; set; } = string.Empty;
        /// <summary>
        /// Volume
        /// </summary>
        [LightProto.ProtoMember(7)]
        [ProtoBuf.ProtoMember(7)]
        public string Volume { get; set; } = string.Empty;

        public MexcStreamKline ToModel()
        {
            return new MexcStreamKline
            {
                ClosePrice = ExchangeHelpers.ParseDecimal(ClosePrice) ?? 0,
                OpenPrice = ExchangeHelpers.ParseDecimal(OpenPrice) ?? 0,
                HighPrice = ExchangeHelpers.ParseDecimal(HighPrice) ?? 0,
                LowPrice = ExchangeHelpers.ParseDecimal(LowPrice) ?? 0,
                QuoteVolume = ExchangeHelpers.ParseDecimal(QuoteVolume) ?? 0,
                Volume = ExchangeHelpers.ParseDecimal(Volume) ?? 0,
                StartTime = DateTimeConverter.ParseFromDouble(StartTime),
                EndTime = DateTimeConverter.ParseFromDouble(EndTime),
                Interval = EnumConverter.ParseString<KlineInterval>(Interval) ?? default
            };
        }
    }
}
