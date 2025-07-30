using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Objects.Sockets.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoContract]
    internal record ProtoAggOrderBookUpdate : MexcStreamEvent
    {
        [ProtoMember(1)]
        public ProtoOrderBookUpdate[] Data { get; set; } = [];
    }
}
