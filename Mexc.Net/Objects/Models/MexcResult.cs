
namespace Mexc.Net.Objects.Models
{
    internal record MexcResult
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
    }

    internal record MexcResult<T> : MexcResult
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }
}
