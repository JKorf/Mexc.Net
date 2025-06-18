using Mexc.Net.Objects.Models.Spot;
using ProtoBuf;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoContract]
    internal abstract class MexcUpdate<T>
    {
        [JsonPropertyName("c")]
        public abstract string Channel { get; set; }
        [JsonPropertyName("s")]
        public abstract string Symbol { get; set; }
        public abstract long CreateTime { get; set; }
        [JsonPropertyName("t")]
        public abstract long SendTime { get; set; }
        public abstract T Data { get; set; }
    }

    [ProtoContract]
    internal class MexcUpdateTrades : MexcUpdate<ProtoTradeUpdate>
    {
        [ProtoMember(1)]
        public override string Channel { get; set; } = string.Empty;
        [ProtoMember(3)]
        public override string Symbol { get; set; } = string.Empty;
        [ProtoMember(5)]
        public override long CreateTime { get; set; }
        [ProtoMember(6)]
        public override long SendTime { get; set; }
        [ProtoMember(314)]
        public override ProtoTradeUpdate Data { get; set; } = default!;
    }

    [ProtoContract]
    internal class MexcUpdateKlines : MexcUpdate<ProtoStreamKline>
    {
        [ProtoMember(1)]
        public override string Channel { get; set; } = string.Empty;
        [ProtoMember(3)]
        public override string Symbol { get; set; } = string.Empty;
        [ProtoMember(5)]
        public override long CreateTime { get; set; }
        [ProtoMember(6)]
        public override long SendTime { get; set; }
        [ProtoMember(308)]
        public override ProtoStreamKline Data { get; set; } = default!;
    }

    [ProtoContract]
    internal class MexcUpdateBookTicker : MexcUpdate<ProtoStreamBookTickerUpdate>
    {
        [ProtoMember(1)]
        public override string Channel { get; set; } = string.Empty;
        [ProtoMember(3)]
        public override string Symbol { get; set; } = string.Empty;
        [ProtoMember(5)]
        public override long CreateTime { get; set; }
        [ProtoMember(6)]
        public override long SendTime { get; set; }
        [ProtoMember(311)]
        public override ProtoStreamBookTickerUpdate Data { get; set; } = default!;
    }

    [ProtoContract]
    internal class MexcUpdateOrderBook : MexcUpdate<ProtoOrderBookUpdate>
    {
        [ProtoMember(1)]
        public override string Channel { get; set; } = string.Empty;
        [ProtoMember(3)]
        public override string Symbol { get; set; } = string.Empty;
        [ProtoMember(5)]
        public override long CreateTime { get; set; }
        [ProtoMember(6)]
        public override long SendTime { get; set; }
        [ProtoMember(313)]
        public override ProtoOrderBookUpdate Data { get; set; } = default!;
    }

    [ProtoContract]
    internal class MexcUpdateAggOrderBook : MexcUpdate<ProtoAggOrderBookUpdate>
    {
        [ProtoMember(1)]
        public override string Channel { get; set; } = string.Empty;
        [ProtoMember(3)]
        public override string Symbol { get; set; } = string.Empty;
        [ProtoMember(5)]
        public override long CreateTime { get; set; }
        [ProtoMember(6)]
        public override long SendTime { get; set; }
        [ProtoMember(312)]
        public override ProtoAggOrderBookUpdate Data { get; set; } = default!;
    }

    [ProtoContract]
    internal class MexcUpdatePartialOrderBook : MexcUpdate<ProtoOrderBookUpdate>
    {
        [ProtoMember(1)]
        public override string Channel { get; set; } = string.Empty;
        [ProtoMember(3)]
        public override string Symbol { get; set; } = string.Empty;
        [ProtoMember(5)]
        public override long CreateTime { get; set; }
        [ProtoMember(6)]
        public override long SendTime { get; set; }
        [ProtoMember(303)]
        public override ProtoOrderBookUpdate Data { get; set; } = default!;
    }

    [ProtoContract]
    internal class MexcUpdateAccount : MexcUpdate<ProtoAccountUpdate>
    {
        [ProtoMember(1)]
        public override string Channel { get; set; } = string.Empty;
        [ProtoMember(3)]
        public override string Symbol { get; set; } = string.Empty;
        [ProtoMember(5)]
        public override long CreateTime { get; set; }
        [ProtoMember(6)]
        public override long SendTime { get; set; }
        [ProtoMember(307)]
        public override ProtoAccountUpdate Data { get; set; } = default!;
    }

    [ProtoContract]
    internal class MexcUpdateOrder : MexcUpdate<ProtoOrderUpdate>
    {
        [ProtoMember(1)]
        public override string Channel { get; set; } = string.Empty;
        [ProtoMember(3)]
        public override string Symbol { get; set; } = string.Empty;
        [ProtoMember(5)]
        public override long CreateTime { get; set; }
        [ProtoMember(6)]
        public override long SendTime { get; set; }
        [ProtoMember(304)]
        public override ProtoOrderUpdate Data { get; set; } = default!;
    }

    [ProtoContract]
    internal class MexcUpdateUserTrade : MexcUpdate<ProtoUserTradeUpdate>
    {
        [ProtoMember(1)]
        public override string Channel { get; set; } = string.Empty;
        [ProtoMember(3)]
        public override string Symbol { get; set; } = string.Empty;
        [ProtoMember(5)]
        public override long CreateTime { get; set; }
        [ProtoMember(6)]
        public override long SendTime { get; set; }
        [ProtoMember(306)]
        public override ProtoUserTradeUpdate Data { get; set; } = default!;
    }

    internal class MexcUpdateMiniTicker: MexcUpdate<MexcStreamMiniTick>
    {
        [JsonPropertyName("c")]
        public override string Channel { get; set; } = string.Empty;
        [JsonPropertyName("s")]
        public override string Symbol { get; set; } = string.Empty;
        public override long CreateTime { get; set; }
        [JsonPropertyName("t")]
        public override long SendTime { get; set; }
        [JsonPropertyName("d")]
        public override MexcStreamMiniTick Data { get; set; } = default!;
    }

    internal class MexcUpdateMiniTickers : MexcUpdate<MexcStreamMiniTick[]>
    {
        [JsonPropertyName("c")]
        public override string Channel { get; set; } = string.Empty;
        [JsonPropertyName("s")]
        public override string Symbol { get; set; } = string.Empty;
        public override long CreateTime { get; set; }
        [JsonPropertyName("t")]
        public override long SendTime { get; set; }
        [JsonPropertyName("d")]
        public override MexcStreamMiniTick[] Data { get; set; } = default!;
    }
}
