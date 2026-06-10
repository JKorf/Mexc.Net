using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Mexc.Net.Objects.Models.Protobuf;
using Mexc.Net.Objects.Sockets.Queries;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcSubscription<T> : Subscription
        where T: MexcUpdate
    {
        private HashSet<string> _topics;
        private readonly Action<DateTime, string?, T> _handler;

        public MexcSubscription(ILogger logger, string[] topics, Action<DateTime, string?, T> handler, bool authenticated) : base(logger, authenticated)
        {
            _topics = new HashSet<string>(topics);
            _handler = handler;

            IndividualSubscriptionCount = topics.Count();

            var topicList = topics.ToList();
            topicList.Add("pb");
            MessageRouter = MessageRouter.CreateForEvent<T>("pb", topics, DoHandleMessage);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, T message)
        {
            if (!_topics.Contains(message.Channel))
                return CallResult.Ok();    

            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.Ok();
        }

        protected override Query? GetSubQuery(SocketConnection connection)
            => new MexcQuery("SUBSCRIPTION", _topics.ToArray(), Authenticated);

        protected override Query? GetUnsubQuery(SocketConnection connection)
            => new MexcQuery("UNSUBSCRIPTION", _topics.ToArray(), Authenticated);
    }
}
