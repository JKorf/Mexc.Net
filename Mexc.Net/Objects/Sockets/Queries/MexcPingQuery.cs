using CryptoExchange.Net.Sockets;
using Mexc.Net.Objects.Sockets.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Sockets.Queries
{
    internal class MexcPingQuery : Query<MexcResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }
        public MexcPingQuery(int weight = 1) : base(new MexcRequest
        {
            Method = "PING"
        }, false, weight)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            ListenerIdentifiers = new HashSet<string>() { "PONG" };
        }
    }
}
