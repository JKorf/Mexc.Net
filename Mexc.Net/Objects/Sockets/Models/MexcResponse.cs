namespace Mexc.Net.Objects.Sockets.Models
{
    internal class MexcResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
