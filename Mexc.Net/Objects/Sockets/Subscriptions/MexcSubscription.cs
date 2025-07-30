using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Objects.Models.Protobuf;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Queries;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcSubscription<T, U> : Subscription<MexcResponse, MexcResponse>
        where T: MexcUpdate<U>
    {
        private string[] _topics;
        private readonly Action<DataEvent<U>> _handler;

        public MexcSubscription(ILogger logger, IEnumerable<string> topics, Action<DataEvent<U>> handler, bool authenticated) : base(logger, authenticated)
        {
            _topics = topics.ToArray();
            _handler = handler;

            MessageMatcher = MessageMatcher.Create<T>(_topics, DoHandleMessage);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<T> message)
        {
            var time = message.Data.SendTime != 0 ? message.Data.SendTime : message.Data.CreateTime;
            _handler.Invoke(message.As(message.Data.Data, message.Data.Channel, message.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(time == 0 ? null : DateTimeConverter.ParseFromDouble(time)));
            return CallResult.SuccessResult;
        }

        public override Query? GetSubQuery(SocketConnection connection)
            => new MexcQuery("SUBSCRIPTION", _topics, Authenticated);

        public override Query? GetUnsubQuery()
            => new MexcQuery("UNSUBSCRIPTION", _topics, Authenticated);
    }
}
