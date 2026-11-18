using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesKline : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string SymbolKline { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string Interval { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public long OpenTime { get; set; }
        [LightProto.ProtoMember(4)]
        public string OpenPrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(5)]
        public string ClosePrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(6)]
        public string HighPrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(7)]
        public string LowPrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(8)]
        public string QuoteVolume { get; set; } = string.Empty;
        [LightProto.ProtoMember(9)]
        public string ContractQuantity { get; set; } = string.Empty;

        public MexcFuturesStreamKline ToModel()
        {
            return new MexcFuturesStreamKline
            {
                Symbol = Symbol,
                OpenPrice = ExchangeHelpers.ParseDecimal(OpenPrice) ?? 0,
                LowPrice = ExchangeHelpers.ParseDecimal(LowPrice) ?? 0,
                HighPrice = ExchangeHelpers.ParseDecimal(HighPrice) ?? 0,
                ClosePrice = ExchangeHelpers.ParseDecimal(ClosePrice) ?? 0,
                Volume = ExchangeHelpers.ParseDecimal(ContractQuantity) ?? 0,
                QuoteVolume = ExchangeHelpers.ParseDecimal(QuoteVolume) ?? 0,
                OpenTime = DateTimeConverter.ParseFromDouble(OpenTime),
                Interval = EnumConverter.ParseString<FuturesKlineInterval>(Interval) ?? default
            };
        }
    }
}
