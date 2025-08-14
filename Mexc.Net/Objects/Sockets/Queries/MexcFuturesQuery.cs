using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Sockets.Queries
{
    internal class MexcFuturesQuery : Query<MexcFuturesUpdate<string>>
    {
        public MexcFuturesQuery(string method, Dictionary<string, object> parameters, bool authenticated, int weight = 1) : base(new MexcFuturesRequest
        {
            Method = method,
            Parameters = parameters
        }, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<MexcFuturesUpdate<string>>(["rs." + method, "rs.error"], HandleMessage);
        }

        public CallResult<MexcFuturesUpdate<string>> HandleMessage(SocketConnection connection, DataEvent<MexcFuturesUpdate<string>> message)
        {
            if (message.Data.Channel.Equals("rs.error", StringComparison.Ordinal))
                return message.ToCallResult<MexcFuturesUpdate<string>>(new ServerError(ErrorInfo.Unknown with { Message = message.Data.Data }));

            return message.ToCallResult();
        }
    }
}
