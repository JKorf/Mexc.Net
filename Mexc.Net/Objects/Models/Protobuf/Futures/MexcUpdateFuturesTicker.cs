using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesTickers : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public MexcUpdateFuturesTicker[] Tickers { get; set; } = [];
    }

    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesTicker
    {
        [LightProto.ProtoMember(1)]
        public string Symbol { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string LastPrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public string PriceChange { get; set; } = string.Empty;
        [LightProto.ProtoMember(4)]
        public string MarkPrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(5)]
        public string IndexPrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(6)]
        public string Volume24h { get; set; } = string.Empty;
        [LightProto.ProtoMember(7)]
        public string QuoteVolume24h { get; set; } = string.Empty;
        [LightProto.ProtoMember(8)]
        public string BestBidPrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(9)]
        public string BestAskPrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(10)]
        public string LowPrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(11)]
        public string HighPrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(12)]
        public long Timestamp { get; set; }

        public MexcFuturesTickerUpdate ToModel()
        {
            return new MexcFuturesTickerUpdate
            {
                Symbol = Symbol,
                LastPrice = ExchangeHelpers.ParseDecimal(LastPrice) ?? 0,
                MaxBidPrice = ExchangeHelpers.ParseDecimal(BestBidPrice) ?? 0,
                MinAskPrice = ExchangeHelpers.ParseDecimal(BestAskPrice) ?? 0,
                Volume24h = ExchangeHelpers.ParseDecimal(Volume24h) ?? 0,
                QuoteVolume24h = ExchangeHelpers.ParseDecimal(QuoteVolume24h) ?? 0,
                LowPrice = ExchangeHelpers.ParseDecimal(LowPrice) ?? 0,
                HighPrice = ExchangeHelpers.ParseDecimal(HighPrice) ?? 0,
                ChangePercentage = (ExchangeHelpers.ParseDecimal(PriceChange) * 100) ?? 0,
                MarkPrice = ExchangeHelpers.ParseDecimal(MarkPrice) ?? 0,
                IndexPrice = ExchangeHelpers.ParseDecimal(IndexPrice) ?? 0,
                Timestamp = DateTimeConverter.ParseFromDouble(Timestamp),                
            };
        }
    }
}
