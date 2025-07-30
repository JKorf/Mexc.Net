using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoContract]
    internal class ProtoStreamBookEntry
    {
        [ProtoMember(1)]
        public string Price { get; set; } = string.Empty;
        [ProtoMember(2)]
        public string Quantity { get; set; } = string.Empty;
    }
}
