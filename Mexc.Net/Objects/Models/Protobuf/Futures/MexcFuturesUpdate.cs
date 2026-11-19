namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    [LightProto.ProtoInclude(2005, typeof(MexcUpdateFuturesTrade))]
    [LightProto.ProtoInclude(2017, typeof(MexcUpdateFuturesDetailTicker))]
    [LightProto.ProtoInclude(2018, typeof(MexcUpdateFuturesTickers))]
    [LightProto.ProtoInclude(2101, typeof(MexcUpdateFuturesKline))]
    [LightProto.ProtoInclude(2015, typeof(MexcUpdateFuturesOrderBook))]
    //[LightProto.ProtoInclude(2016, typeof(MexcUpdateFuturesOrderBookInc))]
    [LightProto.ProtoInclude(2102, typeof(MexcUpdateFuturesFundingRate))]
    [LightProto.ProtoInclude(2001, typeof(MexcUpdateFuturesIndexPrice))]
    [LightProto.ProtoInclude(2013, typeof(MexcUpdateFuturesMarkPrice))]
    [LightProto.ProtoInclude(2004, typeof(MexcUpdateFuturesContract))]
    [LightProto.ProtoInclude(1002, typeof(MexcUpdateFuturesBalance))]    
    [LightProto.ProtoInclude(1001, typeof(MexcUpdateFuturesOrder))]
    [LightProto.ProtoInclude(1011, typeof(MexcUpdateFuturesPosition))]
    [LightProto.ProtoInclude(1004, typeof(MexcUpdateFuturesLeverageMode))]
    [LightProto.ProtoInclude(1005, typeof(MexcUpdateFuturesLiquidation))]
    [LightProto.ProtoInclude(1006, typeof(MexcUpdateFuturesLiquidationRisk))]
    [LightProto.ProtoInclude(1008, typeof(MexcUpdateFuturesUserTrade))]
    [LightProto.ProtoInclude(1009, typeof(MexcUpdateFuturesPlanOrder))]
    [LightProto.ProtoInclude(1010, typeof(MexcUpdateFuturesPositionMode))]
    [LightProto.ProtoInclude(1012, typeof(MexcUpdateFuturesReversePosition))]
    [LightProto.ProtoInclude(1014, typeof(MexcUpdateFuturesTpSlPrice))]
    [LightProto.ProtoInclude(1015, typeof(MexcUpdateFuturesUserRiskLimit))]
    [LightProto.ProtoInclude(1016, typeof(MexcUpdateFuturesTpSlOrder))]
    [LightProto.ProtoInclude(1017, typeof(MexcUpdateFuturesTrailingOrder))]
    [LightProto.ProtoInclude(1018, typeof(MexcUpdateFuturesChaseOrderFailure))]
    internal partial class MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string Symbol { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string Channel { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public long CreateTime { get; set; }
    }
}
