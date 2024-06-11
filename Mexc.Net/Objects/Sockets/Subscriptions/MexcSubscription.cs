using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcSubscription<T> : Subscription<MexcResponse, MexcResponse>
    {
        private IEnumerable<string> _topics;
        private readonly Action<DataEvent<T>> _handler;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public MexcSubscription(ILogger logger, IEnumerable<string> topics, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _topics = topics;
            _handler = handler;
            ListenerIdentifiers = new HashSet<string>(_topics);
        }

        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var data = (MexcUpdate<T>)message.Data;
            _handler.Invoke(message.As(data.Data, data.Channel, data.Symbol, SocketUpdateType.Update));
            return new CallResult(null);
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(MexcUpdate<T>);

        public override Query? GetSubQuery(SocketConnection connection)
            => new MexcQuery("SUBSCRIPTION", _topics, Authenticated);

        public override Query? GetUnsubQuery()
            => new MexcQuery("UNSUBSCRIPTION", _topics, Authenticated);
    }
}
