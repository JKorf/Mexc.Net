using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesOrder : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string OrderId { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public long? PositionId { get; set; }
        [LightProto.ProtoMember(3)]
        public int OpenType { get; set; }
        [LightProto.ProtoMember(4)]
        public int Side { get; set; }
        [LightProto.ProtoMember(5)]
        public string? Price { get; set; }
        [LightProto.ProtoMember(6)]
        public string Quantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(7)]
        public string RemainingQuantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(8)]
        public string? AvgPrice { get; set; }
        [LightProto.ProtoMember(9)]
        public string FilledQuantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(10)]
        public int State { get; set; }
        [LightProto.ProtoMember(11)]
        public long? Version { get; set; }
        [LightProto.ProtoMember(12)]
        public int Category { get; set; }
        [LightProto.ProtoMember(13)]
        public long OrderType { get; set; }
        [LightProto.ProtoMember(14)]
        public string SymbolOrder { get; set; } = string.Empty;
        [LightProto.ProtoMember(15)]
        public string? OrderMargin { get; set; }
        [LightProto.ProtoMember(16)]
        public string? TakerFee { get; set; }
        [LightProto.ProtoMember(17)]
        public string? MakerFee { get; set; }
        [LightProto.ProtoMember(18)]
        public string? Profit { get; set; }
        [LightProto.ProtoMember(19)]
        public string FeeAsset { get; set; } = string.Empty;
        [LightProto.ProtoMember(20)]
        public string? UsedMargin { get; set; }
        [LightProto.ProtoMember(21)]
        public int? Leverage { get; set; }
        [LightProto.ProtoMember(22)]
        public long ErrorCode { get; set; }
        [LightProto.ProtoMember(23)]
        public string? ClientOrderId { get; set; }
        [LightProto.ProtoMember(24)]
        public int? MarketOrderLevel { get; set; }
        [LightProto.ProtoMember(25)]
        public int PositionMode { get; set; }
        [LightProto.ProtoMember(26)]
        public bool? ReduceOnly { get; set; }
        [LightProto.ProtoMember(27)]
        public long? CreateTimeOrder { get; set; }
        [LightProto.ProtoMember(28)]
        public long? UpdateTime { get; set; }
        [LightProto.ProtoMember(29)]
        public int BboTypeNum { get; set; }
        [LightProto.ProtoMember(30)]
        public string MakerFeeRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(31)]
        public string TakerFeeRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(32)]
        public int? BizSource { get; set; }
        [LightProto.ProtoMember(33)]
        public string? SideType { get; set; }
        [LightProto.ProtoMember(34)]
        public string? PositionType { get; set; }


        public MexcFuturesOrder ToModel()
        {
            return new MexcFuturesOrder
            {
                AveragePrice = ExchangeHelpers.ParseDecimal(AvgPrice),
                Category = EnumConverter.ParseString<OrderCategory>(Category.ToString())!.Value,
                CreateTime = CreateTimeOrder == null ? default : DateTimeConverter.ParseFromDecimal(CreateTimeOrder.Value),
                ClientOrderId = ClientOrderId,
                ErrorCode = (int)ErrorCode,
                FeeAsset = FeeAsset,
                Leverage = Leverage ?? 0,
                LimitOrderType = EnumConverter.ParseString<LimitOrderType>(BboTypeNum.ToString())!.Value,
                MakerFee = ExchangeHelpers.ParseDecimal(MakerFee) ?? 0,
                MarginType = EnumConverter.ParseString<MarginType>(OpenType.ToString())!.Value,
                OrderId = long.Parse(OrderId),
                OrderMargin = ExchangeHelpers.ParseDecimal(OrderMargin) ?? 0,
                OrderSide = EnumConverter.ParseString<FuturesOrderSide>(Side.ToString())!.Value,
                OrderType = EnumConverter.ParseString<FuturesOrderType>(OrderType.ToString())!.Value,
                PositionId = PositionId ?? 0,
                PositionMode = EnumConverter.ParseString<PositionMode>(PositionMode.ToString())!.Value,
                Price = ExchangeHelpers.ParseDecimal(Price),
                Profit = ExchangeHelpers.ParseDecimal(Profit) ?? 0,
                Quantity = ExchangeHelpers.ParseDecimal(Quantity) ?? 0,
                QuantityFilled = ExchangeHelpers.ParseDecimal(FilledQuantity) ?? 0,
                ReduceOnly = ReduceOnly ?? false,
                Status = EnumConverter.ParseString<FuturesOrderStatus>(State.ToString())!.Value,
                Symbol = SymbolOrder,
                TakerFee = ExchangeHelpers.ParseDecimal(TakerFee) ?? 0,
                TotalFee = ExchangeHelpers.ParseDecimal(TakerFee) + ExchangeHelpers.ParseDecimal(MakerFee),
                UpdateTime = UpdateTime == null ? default : DateTimeConverter.ParseFromDecimal(UpdateTime.Value),
                UsedMargin = ExchangeHelpers.ParseDecimal(UsedMargin) ?? 0,
                Version = Version
            };
        }
    }
}
