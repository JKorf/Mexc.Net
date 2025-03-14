using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Sockets.Models
{
    [SerializationModel]
    internal record MexcTradeUpdate : MexcStreamEvent
    {
        [JsonPropertyName("deals")]
        public MexcStreamTrade[] Data { get; set; } = Array.Empty<MexcStreamTrade>();
    }
}
