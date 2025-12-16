namespace Mexc.Net.Objects.Models.Protobuf
{

    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]
    internal partial class MexcUpdateTrades : MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        [ProtoBuf.ProtoMember(1)]
        public MexcUpdateStreamTrade[] Data { get; set; } = Array.Empty<MexcUpdateStreamTrade>();
        [LightProto.ProtoMember(2)]
        [ProtoBuf.ProtoMember(2)]
        public string EventType { get; set; } = string.Empty;
    }

}
