using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesTrailingOrder : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string Id { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string SymbolOrder { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public int? Leverage { get; set; }
        [LightProto.ProtoMember(4)]
        public int Side { get; set; }
        [LightProto.ProtoMember(5)]
        public string? Quantity { get; set; }
        [LightProto.ProtoMember(6)]
        public int? OpenType { get; set; }
        [LightProto.ProtoMember(7)]
        public int Trend { get; set; }
        [LightProto.ProtoMember(8)]
        public string? ActivationPrice { get; set; }
        [LightProto.ProtoMember(9)]
        public string? MarkPrice { get; set; }
        [LightProto.ProtoMember(10)]
        public int BackType { get; set; }
        [LightProto.ProtoMember(11)]
        public string? CallbackValue { get; set; }
        [LightProto.ProtoMember(12)]
        public string? TriggerPrice { get; set; }
        [LightProto.ProtoMember(13)]
        public int TriggerType { get; set; }
        [LightProto.ProtoMember(14)]
        public long? OrderId { get; set; }
        [LightProto.ProtoMember(15)]
        public int? ErrorCode { get; set; }
        [LightProto.ProtoMember(16)]
        public int State { get; set; }
        [LightProto.ProtoMember(17)]
        public long? CreateTimeOrder { get; set; }
        [LightProto.ProtoMember(18)]
        public long? UpdateTime { get; set; }
        [LightProto.ProtoMember(19)]
        public int? PositionMode { get; set; }
        [LightProto.ProtoMember(20)]
        public bool ReduceOnly { get; set; }

        public MexcTrailingOrder ToModel()
        {
            return new MexcTrailingOrder
            {
                Id = long.TryParse(Id, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id) ? id : 0,
                Symbol = SymbolOrder,
                Leverage = Leverage ?? 0,
                Side = EnumConverter.ParseString<FuturesOrderSide>(Side.ToString())!.Value,
                Quantity = ExchangeHelpers.ParseDecimal(Quantity) ?? 0,
                OpenType = OpenType.HasValue ? EnumConverter.ParseString<MarginType>(OpenType.Value.ToString())!.Value : default,
                TriggerPriceType = EnumConverter.ParseString<TriggerPriceType>(Trend.ToString())!.Value,
                ActivationPrice = ExchangeHelpers.ParseDecimal(ActivationPrice),
                MarkPrice = ExchangeHelpers.ParseDecimal(MarkPrice) ?? 0,
                CallbackType = EnumConverter.ParseString<CallbackType>(BackType.ToString())!.Value,
                CallbackValue = ExchangeHelpers.ParseDecimal(CallbackValue) ?? 0,
                TriggerPrice = ExchangeHelpers.ParseDecimal(TriggerPrice) ?? 0,
                TriggerType = EnumConverter.ParseString<TriggerType>(TriggerType.ToString())!.Value,
                OrderId = OrderId ?? 0,
                ErrorCode = ErrorCode,
                // TrackOrderState uses different codes from TriggerOrderState.
                Status = State switch
                {
                    0 or 1 => TpSlStatus.Untriggered,
                    2 => TpSlStatus.Executed,
                    3 => TpSlStatus.Failed,
                    4 => TpSlStatus.Canceled,
                    _ => (TpSlStatus)State,
                },
                CreateTime = CreateTimeOrder.HasValue ? DateTimeConverter.ParseFromDecimal(CreateTimeOrder.Value) : default,
                UpdateTime = UpdateTime.HasValue ? DateTimeConverter.ParseFromDecimal(UpdateTime.Value) : null,
                PositionMode = PositionMode.HasValue ? EnumConverter.ParseString<PositionMode>(PositionMode.Value.ToString())!.Value : default,
                ReduceOnly = ReduceOnly
            };
        }
    }
}
