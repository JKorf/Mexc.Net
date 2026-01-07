namespace Mexc.Net.Objects.Models.Protobuf
{

    [LightProto.ProtoContract]
    internal partial class MexcUpdateBookTickers : MexcUpdate
    {
        [LightProto.ProtoMember(1)]
        public MexcUpdateBookTicker[] Data { get; set; } = [];
    }

}
