using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models
{
    internal class MexcResult<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string? Message { get; set; }
        [JsonProperty("data")]
        public T? Data { get; set; }
    }
}
