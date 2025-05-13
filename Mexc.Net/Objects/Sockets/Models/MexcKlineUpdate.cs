using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Sockets.Models
{
    [SerializationModel]
    internal record MexcKlineUpdate : MexcStreamEvent
    {
        [JsonPropertyName("k")]
        public MexcStreamKline Data { get; set; } = default!;
    }
}
