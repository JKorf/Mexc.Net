using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoContract]
    internal record ProtoOrderUpdate
    {
        [ProtoMember(1)]
        public string Id { get; set; } = string.Empty;
        [ProtoMember(2)]
        public string ClientId { get; set; } = string.Empty;
        [ProtoMember(3)]
        public string Price { get; set; } = string.Empty;
        [ProtoMember(4)]
        public string Quantity { get; set; } = string.Empty;
        [ProtoMember(5)]
        public string QuoteQuantity { get; set; } = string.Empty;
        [ProtoMember(6)]
        public string AveragePrice { get; set; } = string.Empty;
        [ProtoMember(7)]
        public int OrderType { get; set; }
        [ProtoMember(8)]
        public int TradeType { get; set; }
        [ProtoMember(9)]
        public bool IsMaker { get; set; }
        [ProtoMember(10)]
        public string RemainingQuoteQuantity { get; set; } = string.Empty;
        [ProtoMember(11)]
        public string RemainingQuantity { get; set; } = string.Empty;
        [ProtoMember(12)]
        public string? LastTradeQuantity { get; set; }
        [ProtoMember(13)]
        public string CumulativeQuantity { get; set; } = string.Empty;
        [ProtoMember(14)]
        public string CumulativeQuoteQuantity { get; set; } = string.Empty;
        [ProtoMember(15)]
        public int Status { get; set; }
        [ProtoMember(16)]
        public long CreateTime { get; set; }
        [ProtoMember(17)]
        public string? Symbol { get; set; }
        [ProtoMember(18)]
        public int? TriggerType { get; set; }
        [ProtoMember(19)]
        public int? TriggerPrice { get; set; }
        [ProtoMember(20)]
        public int? State { get; set; }
        [ProtoMember(21)]
        public string? OcoId { get; set; }
        [ProtoMember(22)]
        public string? RouteFactor { get; set; }
        [ProtoMember(23)]
        public string? SymbolId { get; set; }
        [ProtoMember(24)]
        public string? MarketId { get; set; }
        [ProtoMember(25)]
        public string? MarketCurrencyId { get; set; }
        [ProtoMember(26)]
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
                Timestamp = DateTimeConverter.ParseFromDouble(CreateTime),
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
