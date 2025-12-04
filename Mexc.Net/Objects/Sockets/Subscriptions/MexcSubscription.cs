using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Objects.Models.Protobuf;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Queries;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcSubscription<T> : Subscription
        where T: MexcUpdate
    {
        private HashSet<string> _topics;
        private readonly Action<DateTime, string?, T> _handler;

        public MexcSubscription(ILogger logger, IEnumerable<string> topics, Action<DateTime, string?, T> handler, bool authenticated) : base(logger, authenticated)
        {
            _topics = new HashSet<string>(topics);
            _handler = handler;

            var topicList = topics.ToList();
            topicList.Add("pb");
            MessageMatcher = MessageMatcher.Create<T>(topicList, DoHandleMessage);
            MessageRouter = MessageRouter.CreateWithTopicFilters<T>("pb", topics, DoHandleMessage);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, T message)
        {
            if (!_topics.Contains(message.Channel))
                return CallResult.SuccessResult;    

            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }

        protected override Query? GetSubQuery(SocketConnection connection)
            => new MexcQuery("SUBSCRIPTION", _topics.ToArray(), Authenticated);

        protected override Query? GetUnsubQuery(SocketConnection connection)
            => new MexcQuery("UNSUBSCRIPTION", _topics.ToArray(), Authenticated);
    }
}
