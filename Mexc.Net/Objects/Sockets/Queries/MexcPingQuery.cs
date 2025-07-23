using CryptoExchange.Net.Sockets;
using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Sockets.Queries
{
    internal class MexcPingQuery : Query<MexcResponse>
    {
        public MexcPingQuery(int weight = 1) : base(new MexcRequest
        {
            Method = "PING"
        }, false, weight)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageMatcher = MessageMatcher.Create<MexcResponse>("PONG");
        }
    }
}
