using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesUserTrade : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string? Id { get; set; }
        [LightProto.ProtoMember(2)]
        public string SymbolTrade { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public int Side { get; set; }
        [LightProto.ProtoMember(4)]
        public string? Quantity { get; set; }
        [LightProto.ProtoMember(5)]
        public string? Price { get; set; }
        [LightProto.ProtoMember(6)]
        public string? FeeAsset { get; set; }
        [LightProto.ProtoMember(7)]
        public string? Fee { get; set; }
        [LightProto.ProtoMember(8)]
        public long? Timestamp { get; set; }
        [LightProto.ProtoMember(9)]
        public string? Profit { get; set; }
        [LightProto.ProtoMember(10)]
        public bool IsTaker { get; set; }
        [LightProto.ProtoMember(11)]
        public int Category { get; set; }
        [LightProto.ProtoMember(12)]
        public string OrderId { get; set; } = string.Empty;
        [LightProto.ProtoMember(13)]
        public bool? IsSelf { get; set; }
        [LightProto.ProtoMember(14)]
        public string? ClientOrderId { get; set; }
        [LightProto.ProtoMember(15)]
        public int PositionMode { get; set; }
        [LightProto.ProtoMember(16)]
        public bool? ReduceOnly { get; set; }
        [LightProto.ProtoMember(17)]
        public long? OpponentUid { get; set; }

        public MexcFuturesUserTradeUpdate ToModel()
        {
            return new MexcFuturesUserTradeUpdate
            {
                Id = long.TryParse(Id, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id) ? id : 0,
                Symbol = SymbolTrade,
                Side = EnumConverter.ParseString<FuturesOrderSide>(Side.ToString())!.Value,
                Quantity = ExchangeHelpers.ParseDecimal(Quantity) ?? 0,
                Price = ExchangeHelpers.ParseDecimal(Price) ?? 0,
                FeeAsset = FeeAsset ?? string.Empty,
                Fee = ExchangeHelpers.ParseDecimal(Fee) ?? 0,
                Timestamp = Timestamp.HasValue ? DateTimeConverter.ParseFromDecimal(Timestamp.Value) : default,
                Profit = ExchangeHelpers.ParseDecimal(Profit) ?? 0,
                IsTaker = IsTaker,
                Category = EnumConverter.ParseString<OrderCategory>(Category.ToString())!.Value,
                OrderId = long.TryParse(OrderId, NumberStyles.Integer, CultureInfo.InvariantCulture, out var orderId) ? orderId : 0,
                IsSelf = IsSelf ?? false,
                ClientOrderId = ClientOrderId,
                PositionMode = EnumConverter.ParseString<PositionMode>(PositionMode.ToString())!.Value,
                ReduceOnly = ReduceOnly ?? false,
                OpponentUid = OpponentUid
            };
        }
    }
}
