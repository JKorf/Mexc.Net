using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Objects.Sockets.Models;

namespace Mexc.Net.Objects.Sockets.Queries
{
    internal class MexcQuery : Query<MexcResponse>
    {
        private readonly IEnumerable<string> _expectedTopics;

        public MexcQuery(string method, string[] parameters, bool authenticated, int weight = 1) : base(new MexcRequest
        {
            Id = ExchangeHelpers.NextId(),
            Method = method,
            Parameters = parameters
        }, authenticated, weight)
        {
            _expectedTopics = parameters;

            MessageMatcher = MessageMatcher.Create<MexcResponse>(((MexcRequest)Request).Id.ToString(), HandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<MexcResponse>(((MexcRequest)Request).Id.ToString(), HandleMessage);
        }

        public CallResult<MexcResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcResponse message)
        {
            var topics = message.Message.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (!topics.All(t => _expectedTopics.Contains(t)))
                return new CallResult<MexcResponse>(new ServerError(ErrorInfo.Unknown with { Message = message.Message }));

            return new CallResult<MexcResponse>(message, originalData, null);
        }
    }
}
