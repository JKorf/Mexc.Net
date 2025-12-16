using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcPongSubscription : SystemSubscription
    {
        public MexcPongSubscription(ILogger logger) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<MexcResponse>("pong");
            MessageRouter = MessageRouter.CreateWithoutHandler<MexcResponse>("pong");
        }
    }
}
