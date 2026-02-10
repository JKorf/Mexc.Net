using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Sockets.Queries
{
    internal class MexcFuturesQuery : Query<MexcFuturesUpdate<string>>
    {
        public MexcFuturesQuery(string method, Dictionary<string, object> parameters, bool authenticated, bool expectsResponse = true, int weight = 1) : base(new MexcFuturesRequest
        {
            Method = method,
            Parameters = parameters
        }, authenticated, weight)
        {
            ExpectsResponse = expectsResponse;

            MessageRouter = MessageRouter.CreateWithoutTopicFilter<MexcFuturesUpdate<string>>(["rs." + method, "rs.error"], HandleMessage);
        }

        public CallResult<MexcFuturesUpdate<string>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<string> message)
        {
            if (message.Channel.Equals("rs.error", StringComparison.Ordinal))
                return new CallResult<MexcFuturesUpdate<string>>(new ServerError(ErrorInfo.Unknown with { Message = message.Data }), originalData);

            return new CallResult<MexcFuturesUpdate<string>>(message, originalData, null);
        }
    }
}
