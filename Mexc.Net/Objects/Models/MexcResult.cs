using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models
{
    internal class MexcResult
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string? Message { get; set; }
    }

    internal class MexcResult<T> : MexcResult
    {
        [JsonProperty("data")]
        public T? Data { get; set; }
    }
}
