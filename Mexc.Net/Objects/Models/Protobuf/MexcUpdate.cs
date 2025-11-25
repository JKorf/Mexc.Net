using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]

    [LightProto.ProtoInclude(314, typeof(MexcUpdateTrades))]
    [ProtoBuf.ProtoInclude(314, typeof(MexcUpdateTrades))]
#warning check order book models
    //[LightProto.ProtoInclude(302, typeof(MexcUpdateOrderBook))]
    //[ProtoBuf.ProtoInclude(302, typeof(MexcUpdateOrderBook))]

    //[LightProto.ProtoInclude(303, typeof(MexcUpdateOrderBook))]
    //[ProtoBuf.ProtoInclude(303, typeof(MexcUpdateOrderBook))]

    [LightProto.ProtoInclude(304, typeof(MexcUpdateOrder))]
    [ProtoBuf.ProtoInclude(304, typeof(MexcUpdateOrder))]

    [LightProto.ProtoInclude(306, typeof(MexcUpdateUserTrade))]
    [ProtoBuf.ProtoInclude(306, typeof(MexcUpdateUserTrade))]
    
    //[LightProto.ProtoInclude(312, typeof(MexcUpdateOrderBook))]
    //[ProtoBuf.ProtoInclude(312, typeof(MexcUpdateOrderBook))]

    [LightProto.ProtoInclude(313, typeof(MexcUpdateOrderBook))]
    [ProtoBuf.ProtoInclude(313, typeof(MexcUpdateOrderBook))]

    [LightProto.ProtoInclude(307, typeof(MexcUpdateAccount))]
    [ProtoBuf.ProtoInclude(307, typeof(MexcUpdateAccount))]

    [LightProto.ProtoInclude(308, typeof(MexcUpdateKlines))]
    [ProtoBuf.ProtoInclude(308, typeof(MexcUpdateKlines))]

    [LightProto.ProtoInclude(311, typeof(MexcUpdateBookTicker))]
    [ProtoBuf.ProtoInclude(311, typeof(MexcUpdateBookTicker))]
    internal partial class MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        [ProtoBuf.ProtoMember(1)]
        public string Channel { get; set; }
        [LightProto.ProtoMember(3)]
        [ProtoBuf.ProtoMember(3)]
        public string Symbol { get; set; }
        [LightProto.ProtoMember(4)]
        [ProtoBuf.ProtoMember(4)]
        public string SymbolId { get; set; }
        [LightProto.ProtoMember(5)]
        [ProtoBuf.ProtoMember(5)]
        public long CreateTime { get; set; }
        [LightProto.ProtoMember(6)]
        [ProtoBuf.ProtoMember(6)]
        public long SendTime { get; set; }
    }

    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]
    internal partial class MexcUpdateTrades : MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        [ProtoBuf.ProtoMember(1)]
        public ProtoStreamTrade[] Data { get; set; } = Array.Empty<ProtoStreamTrade>();
        [LightProto.ProtoMember(2)]
        [ProtoBuf.ProtoMember(2)]
        public string EventType { get; set; } = string.Empty;
    }

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

    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]
    internal partial class MexcUpdateBookTicker : MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        [ProtoBuf.ProtoMember(1)]
        public ProtoStreamBookTicker[] Data { get; set; } = [];
    }

    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]
    internal partial class MexcUpdateOrderBook : MexcUpdate
    {
        [ProtoBuf.ProtoMember(1)]
        [LightProto.ProtoMember(1)]
        public ProtoStreamBookEntry[] Asks { get; set; } = Array.Empty<ProtoStreamBookEntry>();
        [ProtoBuf.ProtoMember(2)]
        [LightProto.ProtoMember(2)]
        public ProtoStreamBookEntry[] Bids { get; set; } = Array.Empty<ProtoStreamBookEntry>();

        [ProtoBuf.ProtoMember(4)]
        [LightProto.ProtoMember(4)]
        public string Version { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(5)]
        [LightProto.ProtoMember(5)]
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
                Sequence = Version,
                SequenceEnd = Version2
            };
        }
    }

    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]
    internal partial class MexcUpdateAccount : MexcUpdate
    {
        /// <summary>
        /// Asset
        /// </summary>
        [ProtoBuf.ProtoMember(1)]
        [LightProto.ProtoMember(1)]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [ProtoBuf.ProtoMember(8)]
        [LightProto.ProtoMember(8)]
        public long Timestamp { get; set; }
        /// <summary>
        /// New free quantity
        /// </summary>
        [ProtoBuf.ProtoMember(3)]
        [LightProto.ProtoMember(3)]
        public string Free { get; set; } = string.Empty;
        /// <summary>
        /// Changed free quantity
        /// </summary>
        [ProtoBuf.ProtoMember(4)]
        [LightProto.ProtoMember(4)]
        public string FreeChange { get; set; } = string.Empty;
        /// <summary>
        /// New frozen quantity
        /// </summary>
        [ProtoBuf.ProtoMember(5)]
        [LightProto.ProtoMember(5)]
        public string Frozen { get; set; } = string.Empty;
        /// <summary>
        /// Changed frozen quantity
        /// </summary>
        [ProtoBuf.ProtoMember(6)]
        [LightProto.ProtoMember(6)]
        public string FrozenChange { get; set; } = string.Empty;
        /// <summary>
        /// Trigger update type
        /// </summary>
        [ProtoBuf.ProtoMember(7)]
        [LightProto.ProtoMember(7)]
        public string UpdateType { get; set; } = string.Empty;

        public MexcAccountUpdate ToModel()
        {
            return new MexcAccountUpdate
            {
                Asset = Asset,
                Timestamp = DateTimeConverter.ParseFromDouble(Timestamp),
                Free = ExchangeHelpers.ParseDecimal(Free) ?? 0,
                FreeChange = ExchangeHelpers.ParseDecimal(FreeChange) ?? 0,
                Frozen = ExchangeHelpers.ParseDecimal(Frozen) ?? 0,
                FrozenChange = ExchangeHelpers.ParseDecimal(FrozenChange) ?? 0,
                UpdateType = UpdateType
            };
        }
    }

    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]
    internal partial class MexcUpdateOrder : MexcUpdate
    {
        [ProtoBuf.ProtoMember(1)]
        [LightProto.ProtoMember(1)]
        public string Id { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(2)]
        [LightProto.ProtoMember(2)]
        public string ClientId { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(3)]
        [LightProto.ProtoMember(3)]
        public string Price { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(4)]
        [LightProto.ProtoMember(4)]
        public string Quantity { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(5)]
        [LightProto.ProtoMember(5)]
        public string QuoteQuantity { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(6)]
        [LightProto.ProtoMember(6)]
        public string AveragePrice { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(7)]
        [LightProto.ProtoMember(7)]
        public int OrderType { get; set; }
        [ProtoBuf.ProtoMember(8)]
        [LightProto.ProtoMember(8)]
        public int TradeType { get; set; }
        [ProtoBuf.ProtoMember(9)]
        [LightProto.ProtoMember(9)]
        public bool IsMaker { get; set; }
        [ProtoBuf.ProtoMember(10)]
        [LightProto.ProtoMember(10)]
        public string RemainingQuoteQuantity { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(11)]
        [LightProto.ProtoMember(11)]
        public string RemainingQuantity { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(12)]
        [LightProto.ProtoMember(12)]
        public string? LastTradeQuantity { get; set; }
        [ProtoBuf.ProtoMember(13)]
        [LightProto.ProtoMember(13)]
        public string CumulativeQuantity { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(14)]
        [LightProto.ProtoMember(14)]
        public string CumulativeQuoteQuantity { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(15)]
        [LightProto.ProtoMember(15)]
        public int Status { get; set; }
        [ProtoBuf.ProtoMember(16)]
        [LightProto.ProtoMember(16)]
        public long CreateTimeInt { get; set; }
        [ProtoBuf.ProtoMember(17)]
        [LightProto.ProtoMember(17)]
        public string? SymbolInt { get; set; }
        [ProtoBuf.ProtoMember(18)]
        [LightProto.ProtoMember(18)]
        public int? TriggerType { get; set; }
        [ProtoBuf.ProtoMember(19)]
        [LightProto.ProtoMember(19)]
        public int? TriggerPrice { get; set; }
        [ProtoBuf.ProtoMember(20)]
        [LightProto.ProtoMember(20)]
        public int? State { get; set; }
        [ProtoBuf.ProtoMember(21)]
        [LightProto.ProtoMember(21)]
        public string? OcoId { get; set; }
        [ProtoBuf.ProtoMember(22)]
        [LightProto.ProtoMember(22)]
        public string? RouteFactor { get; set; }
        [ProtoBuf.ProtoMember(23)]
        [LightProto.ProtoMember(23)]
        public string? SymbolIdInt { get; set; }
        [ProtoBuf.ProtoMember(24)]
        [LightProto.ProtoMember(24)]
        public string? MarketId { get; set; }
        [ProtoBuf.ProtoMember(25)]
        [LightProto.ProtoMember(25)]
        public string? MarketCurrencyId { get; set; }
        [ProtoBuf.ProtoMember(26)]
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

    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]
    internal partial class MexcUpdateUserTrade : MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        [ProtoBuf.ProtoMember(1)]
        public string Price { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        [ProtoBuf.ProtoMember(2)]
        public string Quantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        [ProtoBuf.ProtoMember(3)]
        public string QuoteQuantity { get; set; } = string.Empty;

        [LightProto.ProtoMember(4)]
        [ProtoBuf.ProtoMember(4)]
        public int TradeType { get; set; }
        [LightProto.ProtoMember(5)]
        [ProtoBuf.ProtoMember(5)]
        public bool IsMaker { get; set; }
        [LightProto.ProtoMember(6)]
        [ProtoBuf.ProtoMember(6)]
        public bool IsSelfTrade { get; set; }

        [LightProto.ProtoMember(7)]
        [ProtoBuf.ProtoMember(7)]
        public string TradeId { get; set; } = string.Empty;
        [LightProto.ProtoMember(8)]
        [ProtoBuf.ProtoMember(8)]
        public string ClientOrderId { get; set; } = string.Empty;
        [LightProto.ProtoMember(9)]
        [ProtoBuf.ProtoMember(9)]
        public string OrderId { get; set; } = string.Empty;

        [LightProto.ProtoMember(10)]
        [ProtoBuf.ProtoMember(10)]
        public string Fee { get; set; } = string.Empty;
        [LightProto.ProtoMember(11)]
        [ProtoBuf.ProtoMember(11)]
        public string FeeAsset { get; set; } = string.Empty;

        [LightProto.ProtoMember(12)]
        [ProtoBuf.ProtoMember(12)]
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

    //internal class MexcUpdateMiniTicker: MexcUpdate<MexcStreamMiniTick>
    //{
    //    [JsonPropertyName("c")]
    //    public override string Channel { get; set; } = string.Empty;
    //    [JsonPropertyName("s")]
    //    public override string Symbol { get; set; } = string.Empty;
    //    public override long CreateTime { get; set; }
    //    [JsonPropertyName("t")]
    //    public override long SendTime { get; set; }
    //    [JsonPropertyName("d")]
    //    public override MexcStreamMiniTick Data { get; set; } = default!;
    //}

    //internal class MexcUpdateMiniTickers : MexcUpdate<MexcStreamMiniTick[]>
    //{
    //    [JsonPropertyName("c")]
    //    public override string Channel { get; set; } = string.Empty;
    //    [JsonPropertyName("s")]
    //    public override string Symbol { get; set; } = string.Empty;
    //    public override long CreateTime { get; set; }
    //    [JsonPropertyName("t")]
    //    public override long SendTime { get; set; }
    //    [JsonPropertyName("d")]
    //    public override MexcStreamMiniTick[] Data { get; set; } = default!;
    //}
}
