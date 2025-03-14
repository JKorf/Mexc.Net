using CryptoExchange.Net.Converters.SystemTextJson;

namespace Mexc.Net.Objects.Models
{
    [SerializationModel]
    internal record MexcResult
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
    }

    [SerializationModel]
    internal record MexcResult<T> : MexcResult
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }
}
