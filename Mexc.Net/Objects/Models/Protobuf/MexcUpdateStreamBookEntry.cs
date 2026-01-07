namespace Mexc.Net.Objects.Models.Protobuf
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateStreamBookEntry
    {
        [LightProto.ProtoMember(1)]
        public string Price { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string Quantity { get; set; } = string.Empty;
    }
}
