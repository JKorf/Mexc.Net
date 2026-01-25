using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateOrder : MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        public string Id { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string ClientId { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public string Price { get; set; } = string.Empty;
        [LightProto.ProtoMember(4)]
        public string Quantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(5)]
        public string QuoteQuantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(6)]
        public string AveragePrice { get; set; } = string.Empty;
        [LightProto.ProtoMember(7)]
        public int OrderType { get; set; }
        [LightProto.ProtoMember(8)]
        public int TradeType { get; set; }
        [LightProto.ProtoMember(9)]
        public bool IsMaker { get; set; }
        [LightProto.ProtoMember(10)]
        public string RemainingQuoteQuantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(11)]
        public string RemainingQuantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(12)]
        public string? LastTradeQuantity { get; set; }
        [LightProto.ProtoMember(13)]
        public string CumulativeQuantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(14)]
        public string CumulativeQuoteQuantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(15)]
        public int Status { get; set; }
        [LightProto.ProtoMember(16)]
        public long CreateTimeInt { get; set; }
        [LightProto.ProtoMember(17)]
        public string? SymbolInt { get; set; }
        [LightProto.ProtoMember(18)]
        public int? TriggerType { get; set; }
        [LightProto.ProtoMember(19)]
        public int? TriggerPrice { get; set; }
        [LightProto.ProtoMember(20)]
        public int? State { get; set; }
        [LightProto.ProtoMember(21)]
        public string? OcoId { get; set; }
        [LightProto.ProtoMember(22)]
        public string? RouteFactor { get; set; }
        [LightProto.ProtoMember(23)]
        public string? SymbolIdInt { get; set; }
        [LightProto.ProtoMember(24)]
        public string? MarketId { get; set; }
        [LightProto.ProtoMember(25)]
        public string? MarketCurrencyId { get; set; }
        [LightProto.ProtoMember(26)]
        public string? CurrencyId { get; set; }

        public MexcUserOrderUpdate ToModel()
        {
            return new MexcUserOrderUpdate
            {
                AveragePrice = ExchangeHelpers.ParseDecimal(AveragePrice),
                ClientOrderId = ClientId,
                CumulativeQuantity = ExchangeHelpers.ParseDecimal(CumulativeQuantity),
                CumulativeQuoteQuantity = ExchangeHelpers.ParseDecimal(CumulativeQuoteQuantity),
                IsMaker = IsMaker,
                OrderId = Id,
                OrderType = ToOrderType(),
                Price = ExchangeHelpers.ParseDecimal(Price) ?? 0,
                Quantity = ExchangeHelpers.ParseDecimal(Quantity) ?? 0,
                QuantityRemaining = ExchangeHelpers.ParseDecimal(RemainingQuantity) ?? 0,
                QuoteQuantity = ExchangeHelpers.ParseDecimal(QuoteQuantity) ?? 0,
                QuoteQuantityRemaining = ExchangeHelpers.ParseDecimal(RemainingQuoteQuantity) ?? 0,
                Side = TradeType == 1 ? OrderSide.Buy : OrderSide.Sell,
                Timestamp = DateTimeConverter.ParseFromDouble(CreateTimeInt),
                Status = ToOrderStatus()
            };
        }

        private OrderStatus ToOrderStatus()
        {
            if (Status == 1) return OrderStatus.New;
            if (Status == 2) return OrderStatus.Filled;
            if (Status == 3) return OrderStatus.PartiallyFilled;
            if (Status == 4) return OrderStatus.Canceled;
            if (Status == 5) return OrderStatus.PartiallyCanceled;

            throw new Exception("Unknown order status: " + Status);
        }

        private OrderType ToOrderType()
        {
            if (OrderType == 1) return Enums.OrderType.Limit;
            if (OrderType == 2) return Enums.OrderType.LimitMaker;
            if (OrderType == 3) return Enums.OrderType.ImmediateOrCancel;
            if (OrderType == 4) return Enums.OrderType.FillOrKill;
            if (OrderType == 5) return Enums.OrderType.Market;
            if (OrderType == 100) return Enums.OrderType.TpSlOrder;

            throw new Exception("Unknown order type: " + OrderType);
        }
    }
}
