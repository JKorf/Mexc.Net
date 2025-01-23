namespace Mexc.Net.Objects.Sockets.Models
{
    internal class MexcRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;
        [JsonPropertyName("params")]
        public IEnumerable<string> Parameters { get; set; } = Array.Empty<string>();
    }
}
