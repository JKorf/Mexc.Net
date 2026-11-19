using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesDetailTicker : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string SymbolTicker { get; set; } = string.Empty;
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
        [LightProto.ProtoMember(13)]
        public string Bid1Price { get; set; } = string.Empty;
        [LightProto.ProtoMember(14)]
        public string Ask1Price { get; set; } = string.Empty;
        [LightProto.ProtoMember(15)]
        public string OpenInterest { get; set; } = string.Empty;
        [LightProto.ProtoMember(16)]
        public string ChangeValue { get; set; } = string.Empty;
        [LightProto.ProtoMember(17)]
        public string FundingRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(18)]
        public string TimeZone { get; set; } = string.Empty;
        [LightProto.ProtoMember(19)]
        public string[] ChangePercentages { get; set; } = [];
        [LightProto.ProtoMember(20)]
        public string[] ChangePercentagesTimeZones { get; set; } = [];

        public MexcFuturesDetailTickerUpdate ToModel()
        {
            return new MexcFuturesDetailTickerUpdate
            {
                Symbol = SymbolTicker,
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
                BestAskPrice = ExchangeHelpers.ParseDecimal(Ask1Price) ?? 0,
                BestBidPrice = ExchangeHelpers.ParseDecimal(Bid1Price) ?? 0,
                ChangeValue = ExchangeHelpers.ParseDecimal(ChangeValue) ?? 0,
                FundingRate = ExchangeHelpers.ParseDecimal(FundingRate) ?? 0,
                OpenInterest = ExchangeHelpers.ParseDecimal(OpenInterest) ?? 0,
                TimeZone = TimeZone,
                ChangeRates = ChangePercentages.Select(x => ExchangeHelpers.ParseDecimal(x)).ToArray(),
                ChangeRatesTimeZones = ChangePercentagesTimeZones
            };
        }
    }
}
