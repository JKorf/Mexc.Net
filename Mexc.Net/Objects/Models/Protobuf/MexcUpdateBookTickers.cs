namespace Mexc.Net.Objects.Models.Protobuf
{

    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]
    internal partial class MexcUpdateBookTickers : MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        [ProtoBuf.ProtoMember(1)]
        public MexcUpdateBookTicker[] Data { get; set; } = [];
    }

}
