using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoContract]
    internal record ProtoStreamBookTickerUpdate
    {
        [ProtoMember(1)]
        public ProtoStreamBookTicker[] Data { get; set; } = [];
    }
}
