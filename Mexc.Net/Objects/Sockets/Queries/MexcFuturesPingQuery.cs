using CryptoExchange.Net.Sockets;
using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Sockets.Queries
{
    internal class MexcFuturesPingQuery : Query<MexcResponse>
    {
        public MexcFuturesPingQuery(int weight = 1) : base(new MexcRequest
        {
            Method = "ping"
        }, false, weight)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageMatcher = MessageMatcher.Create<MexcResponse>("pong");
        }
    }
}
