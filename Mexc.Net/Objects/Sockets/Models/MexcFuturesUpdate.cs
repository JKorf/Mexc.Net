using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Sockets.Models
{
    internal record MexcFuturesUpdate
    {
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }

    internal record MexcFuturesUpdate<T> : MexcFuturesUpdate
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
