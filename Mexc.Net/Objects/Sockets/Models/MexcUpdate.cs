using ProtoBuf;

namespace Mexc.Net.Objects.Sockets.Models
{
    [ProtoContract]
    [ProtoInclude(301, typeof(MexcUpdateTrades))]
    internal abstract class MexcUpdate<T>
    {
        [ProtoMember(1)]
        [JsonPropertyName("c")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("s")]
        [ProtoMember(3)]
        public string Symbol { get; set; } = string.Empty;
        [JsonPropertyName("t")]
        [ProtoMember(5)]
        public long CreateTime { get; set; }
        [ProtoMember(6)]
        public long SendTime { get; set; }

        public abstract T Data { get; set; }
    }

    [ProtoContract]
    internal class MexcUpdateTrades : MexcUpdate<MexcTradeUpdate>
    {
        [ProtoMember(301)]
        public override MexcTradeUpdate Data { get; set; }
    }
}
