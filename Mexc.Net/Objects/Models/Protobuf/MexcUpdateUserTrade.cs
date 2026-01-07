using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateUserTrade : MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        public string Price { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string Quantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public string QuoteQuantity { get; set; } = string.Empty;

        [LightProto.ProtoMember(4)]
        public int TradeType { get; set; }
        [LightProto.ProtoMember(5)]
        public bool IsMaker { get; set; }
        [LightProto.ProtoMember(6)]
        public bool IsSelfTrade { get; set; }

        [LightProto.ProtoMember(7)]
        public string TradeId { get; set; } = string.Empty;
        [LightProto.ProtoMember(8)]
        public string ClientOrderId { get; set; } = string.Empty;
        [LightProto.ProtoMember(9)]
        public string OrderId { get; set; } = string.Empty;

        [LightProto.ProtoMember(10)]
        public string Fee { get; set; } = string.Empty;
        [LightProto.ProtoMember(11)]
        public string FeeAsset { get; set; } = string.Empty;

        [LightProto.ProtoMember(12)]
        public long Timestamp { get; set; }

        public MexcUserTradeUpdate ToModel()
        {
            return new MexcUserTradeUpdate
            {
                ClientOrderId = ClientOrderId,
                Fee = ExchangeHelpers.ParseDecimal(Fee) ?? 0,
                FeeAsset = FeeAsset,
                IsMaker = IsMaker,
                IsSelfTrade = IsSelfTrade,
                OrderId = OrderId,
                Price = ExchangeHelpers.ParseDecimal(Price) ?? 0,
                Quantity = ExchangeHelpers.ParseDecimal(Quantity) ?? 0,
                QuoteQuantity = ExchangeHelpers.ParseDecimal(QuoteQuantity) ?? 0,
                TradeId = TradeId,
                TradeSide = TradeType == 1 ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                TradeTime = DateTimeConverter.ParseFromDouble(Timestamp)
            };
        }
    }
}
