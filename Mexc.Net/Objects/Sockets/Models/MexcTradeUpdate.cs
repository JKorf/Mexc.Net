using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Sockets.Models
{
    internal record MexcTradeUpdate : MexcStreamEvent
    {
        [JsonPropertyName("deals")]
        public IEnumerable<MexcStreamTrade> Data { get; set; } = Array.Empty<MexcStreamTrade>();
    }
}
