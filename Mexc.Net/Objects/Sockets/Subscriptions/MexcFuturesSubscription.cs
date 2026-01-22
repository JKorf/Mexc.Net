using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Queries;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcFuturesSubscription<T> : Subscription
    {
        private string _topic;
        private string? _symbol;
        private FuturesKlineInterval? _interval;
        private int? _limit;
        private readonly Action<DateTime, string?, MexcFuturesUpdate<T>> _handler;

        public MexcFuturesSubscription(ILogger logger, string topic, string? symbol, FuturesKlineInterval? interval, int? limit, Action<DateTime, string?, MexcFuturesUpdate<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _topic = topic;
            _symbol = symbol;
            _interval = interval;
            _limit = limit;
            _handler = handler;

            MessageRouter = MessageRouter.CreateWithOptionalTopicFilter<MexcFuturesUpdate<T>>("push." + _topic, symbol, DoHandleMessage);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<T> message)
        {
            _handler.Invoke(receiveTime, originalData, message);
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
