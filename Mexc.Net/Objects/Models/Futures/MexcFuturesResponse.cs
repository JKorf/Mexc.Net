using CryptoExchange.Net.Converters.SystemTextJson;

namespace Mexc.Net.Objects.Models.Futures
{
    internal record MexcFuturesResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }

    internal record MexcFuturesResponse<T> : MexcFuturesResponse
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
