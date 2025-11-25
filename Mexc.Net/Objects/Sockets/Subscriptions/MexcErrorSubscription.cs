using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcErrorSubscription : SystemSubscription
    {
        public MexcErrorSubscription(ILogger logger) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<MexcResponse>("0", HandleMessage);
        }

        public CallResult HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcResponse message)
        {
            _logger.LogError("Server Error: {Error}", message.Message);
            return CallResult.SuccessResult;
        }
    }
}
