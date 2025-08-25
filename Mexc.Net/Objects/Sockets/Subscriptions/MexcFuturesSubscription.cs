using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Protobuf;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Queries;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcFuturesSubscription<T> : Subscription<MexcFuturesUpdate<string>, MexcFuturesUpdate<string>>
    {
        private string _topic;
        private string? _symbol;
        private FuturesKlineInterval? _interval;
        private int? _limit;
        private readonly Action<DataEvent<T>> _handler;

        public MexcFuturesSubscription(ILogger logger, string topic, string? symbol, FuturesKlineInterval? interval, int? limit, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _topic = topic;
            _symbol = symbol;
            _interval = interval;
            _limit = limit;
            _handler = handler;

            MessageMatcher = MessageMatcher.Create<MexcFuturesUpdate<T>>("push." + _topic + symbol, DoHandleMessage);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<MexcFuturesUpdate<T>> message)
        {
            _handler.Invoke(message.As(message.Data.Data, message.Data.Channel, message.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            var parameters = new Dictionary<string, object>();
            if (_symbol != null)
                parameters.Add("symbol", _symbol);
            if (_interval != null)
                parameters.Add("interval", EnumConverter.GetString(_interval.Value));
            if (_limit != null)
                parameters.Add("limit", _limit);
            return new MexcFuturesQuery("sub." + _topic, parameters, Authenticated);
        }

        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            var parameters = new Dictionary<string, object>();
            if (_symbol != null)
                parameters.Add("symbol", _symbol);
            if (_interval != null)
                parameters.Add("interval", EnumConverter.GetString(_interval.Value));
            if (_limit != null)
                parameters.Add("limit", _limit);
            return new MexcFuturesQuery("unsub." + _topic, parameters, Authenticated);
        }
    }
}
