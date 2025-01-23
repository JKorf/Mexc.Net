namespace Mexc.Net.Objects.Sockets.Models
{
    internal class MexcUpdate<T>
    {
        [JsonPropertyName("c")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        [JsonPropertyName("d")]
        public T Data { get; set; } = default!;
    }
}
