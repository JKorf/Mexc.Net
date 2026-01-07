namespace Mexc.Net.Objects.Models.Protobuf
{
    [LightProto.ProtoContract]
    [LightProto.ProtoInclude(314, typeof(MexcUpdateTrades))]
    [LightProto.ProtoInclude(302, typeof(MexcUpdateOrderBookIncrease))]
    [LightProto.ProtoInclude(303, typeof(MexcUpdateOrderBookLimit))]
    [LightProto.ProtoInclude(304, typeof(MexcUpdateOrder))]
    [LightProto.ProtoInclude(306, typeof(MexcUpdateUserTrade))]
    [LightProto.ProtoInclude(313, typeof(MexcUpdateOrderBook))]
    [LightProto.ProtoInclude(307, typeof(MexcUpdateAccount))]
    [LightProto.ProtoInclude(308, typeof(MexcUpdateKlines))]
    [LightProto.ProtoInclude(309, typeof(MexcUpdateMiniTicker))]
    [LightProto.ProtoInclude(310, typeof(MexcUpdateMiniTickers))]
    [LightProto.ProtoInclude(311, typeof(MexcUpdateBookTickers))]
    internal partial class MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        public string Channel { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public string Symbol { get; set; } = string.Empty;
        [LightProto.ProtoMember(4)]
        public string SymbolId { get; set; } = string.Empty;
        [LightProto.ProtoMember(5)]
        public long CreateTime { get; set; }
        [LightProto.ProtoMember(6)]
        public long SendTime { get; set; }
    }
}
