using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models.Spot
{
    internal class MexcListenKey
    {
        [JsonProperty("listenKey")]
        public string ListenKey { get; set; } = string.Empty;
    }
}
