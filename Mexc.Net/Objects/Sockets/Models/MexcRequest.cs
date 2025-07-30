using ProtoBuf;

namespace Mexc.Net.Objects.Sockets.Models
{
    [ProtoContract]
    internal class MexcRequest
    {
        [JsonPropertyName("method")]
        [ProtoMember(1)]
        public string Method { get; set; } = string.Empty;
        [JsonPropertyName("params")]
        [ProtoMember(2)]
        public string[] Parameters { get; set; } = Array.Empty<string>();
        [JsonPropertyName("id")]
        [ProtoMember(3)]
        public int Id { get; set; }
    }
}
