using Mexc.Net.Objects.Sockets.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [SerializationModel]
    [ProtoContract]
    internal record ProtoTradeUpdate : MexcStreamEvent
    {
        [ProtoMember(1)]
        public ProtoStreamTrade[] Data { get; set; } = Array.Empty<ProtoStreamTrade>();
    }
}
