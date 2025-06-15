using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Queries;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcSubscription<T, U> : Subscription<MexcResponse, MexcResponse>
        where T: MexcUpdate<U>
    {
        private string[] _topics;
        private readonly Action<DataEvent<U>> _handler;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public MexcSubscription(ILogger logger, IEnumerable<string> topics, Action<DataEvent<U>> handler, bool authenticated) : base(logger, authenticated)
        {
            _topics = topics.ToArray();
            _handler = handler;
            ListenerIdentifiers = new HashSet<string>(_topics);
        }

        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var data = (T)message.Data;
            _handler.Invoke(message.As(data.Data, data.Channel, data.Symbol, SocketUpdateType.Update)/*.WithDataTimestamp(data.Timestamp)*/);
            return CallResult.SuccessResult;
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(T);

        public override Query? GetSubQuery(SocketConnection connection)
            => new MexcQuery("SUBSCRIPTION", _topics, Authenticated);

        public override Query? GetUnsubQuery()
            => new MexcQuery("UNSUBSCRIPTION", _topics, Authenticated);
    }
}
