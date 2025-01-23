using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Sockets.Models
{
    internal record MexcKlineUpdate : MexcStreamEvent
    {
        [JsonPropertyName("k")]
        public MexcStreamKline Data { get; set; } = default!;
    }
}
