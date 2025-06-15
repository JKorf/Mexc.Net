using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Objects.Models.Spot;
using ProtoBuf;

namespace Mexc.Net.Objects.Sockets.Models
{
    [SerializationModel]
    [ProtoContract]
    internal record MexcTradeUpdate : MexcStreamEvent
    {
        [JsonPropertyName("deals")]
        [ProtoMember(1)]
        public MexcStreamTrade[] Data { get; set; } = Array.Empty<MexcStreamTrade>();
    }
}
