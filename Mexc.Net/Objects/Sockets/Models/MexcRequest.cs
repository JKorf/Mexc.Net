using ProtoBuf;

namespace Mexc.Net.Objects.Sockets.Models
{
    [ProtoContract]
    internal class MexcRequest
    {
        [JsonPropertyName("method")]
        [ProtoMember(1)]
        public string Method { get; set; } = string.Empty;
        [JsonPropertyName("params"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [ProtoMember(2)]
        public string[]? Parameters { get; set; }
        [JsonPropertyName("id")]
        [ProtoMember(3), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
    }
}
