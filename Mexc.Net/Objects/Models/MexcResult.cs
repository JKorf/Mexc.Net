using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models
{
    internal record MexcResult
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string? Message { get; set; }
    }

    internal record MexcResult<T> : MexcResult
    {
        [JsonProperty("data")]
        public T? Data { get; set; }
    }
}
