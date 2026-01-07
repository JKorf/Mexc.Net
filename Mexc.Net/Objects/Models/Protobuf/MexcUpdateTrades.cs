namespace Mexc.Net.Objects.Models.Protobuf
{

    [LightProto.ProtoContract]
    internal partial class MexcUpdateTrades : MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        public MexcUpdateStreamTrade[] Data { get; set; } = Array.Empty<MexcUpdateStreamTrade>();
        [LightProto.ProtoMember(2)]
        public string EventType { get; set; } = string.Empty;
    }

}
