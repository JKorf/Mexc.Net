namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoBuf.ProtoContract]
    [LightProto.ProtoContract]
    internal partial class MexcUpdateStreamBookEntry
    {
        [ProtoBuf.ProtoMember(1)]
        [LightProto.ProtoMember(1)]
        public string Price { get; set; } = string.Empty;
        [ProtoBuf.ProtoMember(2)]
        [LightProto.ProtoMember(2)]
        public string Quantity { get; set; } = string.Empty;
    }
}
