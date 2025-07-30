using ProtoBuf;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoContract]
    internal record SocketEvent
    {
        [ProtoMember(1)]
        public string c { get; set; } = string.Empty;
    }
}
