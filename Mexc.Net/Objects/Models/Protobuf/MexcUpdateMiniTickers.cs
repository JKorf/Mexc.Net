using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateMiniTickers : MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        public MexcUpdateMiniTickersModel[] Tickers { get; set; } = [];
    }

    [LightProto.ProtoContract]
    internal partial class MexcUpdateMiniTickersModel
    {
        [LightProto.ProtoMember(1)]
        public string SymbolInt { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string Price { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public string Rate { get; set; } = string.Empty;
        [LightProto.ProtoMember(4)]
        public string ZonedRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(5)]
        public string High { get; set; } = string.Empty;
        [LightProto.ProtoMember(6)]
        public string Low { get; set; } = string.Empty;
        [LightProto.ProtoMember(7)]
        public string Volume { get; set; } = string.Empty;
        [LightProto.ProtoMember(8)]
        public string Quantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(9)]
        public string LastCloseRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(10)]
        public string LastCloseZonedRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(11)]
        public string LastCloseHigh { get; set; } = string.Empty;
        [LightProto.ProtoMember(12)]
        public string LastCloseLow { get; set; } = string.Empty;

        public MexcMiniTickUpdate ToModel()
        {
            return new MexcMiniTickUpdate
            {
                LastPrice = ExchangeHelpers.ParseDecimal(Price) ?? 0,
                PriceChangePercentage = (ExchangeHelpers.ParseDecimal(ZonedRate) * 100) ?? 0,
                PriceChangePercentageUtc8 = (ExchangeHelpers.ParseDecimal(Rate) * 100) ?? 0,
                HighPrice = ExchangeHelpers.ParseDecimal(High) ?? 0,
                LowPrice = ExchangeHelpers.ParseDecimal(Low) ?? 0,
                QuoteVolume = ExchangeHelpers.ParseDecimal(Volume) ?? 0,
                Volume = ExchangeHelpers.ParseDecimal(Quantity) ?? 0,
                Symbol = SymbolInt
            };
        }
    }
}
