using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Objects.Models.Protobuf;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Queries;
using System.Linq;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcFuturesUserSubscription : Subscription<MexcFuturesUpdate<string>, MexcFuturesUpdate<string>>
    {
        private readonly Action<DataEvent<MexcFuturesBalanceUpdate>>? _balanceHandler;
        private readonly Action<DataEvent<MexcFuturesOrder>>? _orderHandler;
        private readonly Action<DataEvent<MexcPosition>>? _positionHandler;
        private readonly Action<DataEvent<MexcRiskLimit>>? _riskLimitHandler;
        private readonly Action<DataEvent<MexcAdlUpdate>>? _adlHandler;
        private readonly Action<DataEvent<MexcPositionModeUpdate>>? _positionModeHandler;

        public MexcFuturesUserSubscription(ILogger logger, 
            Action<DataEvent<MexcFuturesBalanceUpdate>>? balanceHandler,
            Action<DataEvent<MexcFuturesOrder>>? orderHandler,
            Action<DataEvent<MexcPosition>>? positionHandler,
            Action<DataEvent<MexcRiskLimit>>? riskLimitHandler,
            Action<DataEvent<MexcAdlUpdate>>? adlHandler,
            Action<DataEvent<MexcPositionModeUpdate>>? positionModeHandler
            ) : base(logger, true)
        {
            _balanceHandler = balanceHandler;
            _orderHandler = orderHandler;
            _positionHandler = positionHandler;
            _riskLimitHandler = riskLimitHandler;
            _adlHandler = adlHandler;
            _positionModeHandler = positionModeHandler;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<MexcFuturesUpdate<MexcFuturesOrder>>("push.personal.order", DoHandleMessage),
                new MessageHandlerLink<MexcFuturesUpdate<MexcFuturesBalanceUpdate>>("push.personal.asset", DoHandleMessage),
                new MessageHandlerLink<MexcFuturesUpdate<MexcPosition>>("push.personal.position", DoHandleMessage),
                new MessageHandlerLink<MexcFuturesUpdate<MexcRiskLimit>>("push.personal.risk.limit", DoHandleMessage),
                new MessageHandlerLink<MexcFuturesUpdate<MexcAdlUpdate>>("push.personal.adl.level", DoHandleMessage),
                new MessageHandlerLink<MexcFuturesUpdate<MexcPositionModeUpdate>>("push.personal.position.mode", DoHandleMessage),
                ]);

            MessageRouter = MessageRouter.Create([
                MessageRoute<MexcFuturesUpdate<MexcFuturesOrder>>.CreateWithoutTopicFilter("push.personal.order", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcFuturesBalanceUpdate>>.CreateWithoutTopicFilter("push.personal.asset", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcPosition>>.CreateWithoutTopicFilter("push.personal.position", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcRiskLimit>>.CreateWithoutTopicFilter("push.personal.risk.limit", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcAdlUpdate>>.CreateWithoutTopicFilter("push.personal.adl.level", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcPositionModeUpdate>>.CreateWithoutTopicFilter("push.personal.position.mode", DoHandleMessage),
                ]);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcFuturesBalanceUpdate> message)
        {
            _balanceHandler?.Invoke(
                new DataEvent<MexcFuturesBalanceUpdate>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcFuturesOrder> message)
        {
            _orderHandler?.Invoke(
                new DataEvent<MexcFuturesOrder>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcPosition> message)
        {
            _positionHandler?.Invoke(
                new DataEvent<MexcPosition>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcRiskLimit> message)
        {
            _riskLimitHandler?.Invoke(
                new DataEvent<MexcRiskLimit>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcPositionModeUpdate> message)
        {
            _positionModeHandler?.Invoke(
                new DataEvent<MexcPositionModeUpdate>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcAdlUpdate> message)
        {
            _adlHandler?.Invoke(
                new DataEvent<MexcAdlUpdate>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp));
            return CallResult.SuccessResult;
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new MexcFuturesQuery("personal.filter", new Dictionary<string, object>() { { "filters", new SubFilter[0] } }, Authenticated);
        }

        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            // There doesn't seem to be a way to unsubscribe from all user updates
            // Work around is to filter all message except a single one which shouldn't trigger too often
            var filters = new[] { "adl.level" };
            return new MexcFuturesQuery("personal.filter", new Dictionary<string, object>()
            {
                { "filters", filters.Select(x => new SubFilter{ Filter = x }).ToArray() }
            }, Authenticated);
        }

        internal record SubFilter
        {
            [JsonPropertyName("filter")]
            public string Filter { get; set; } = string.Empty;
        }
    }
}
