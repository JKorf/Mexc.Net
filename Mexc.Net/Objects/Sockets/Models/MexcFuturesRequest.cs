using ProtoBuf;

namespace Mexc.Net.Objects.Sockets.Models
{
    internal class MexcFuturesRequest
    {
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;
        [JsonPropertyName("param")]
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}
