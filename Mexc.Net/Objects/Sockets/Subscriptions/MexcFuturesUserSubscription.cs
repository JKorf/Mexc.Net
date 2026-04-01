using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Mexc.Net.Clients.FuturesApi;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Queries;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcFuturesUserSubscription : Subscription
    {
        private readonly MexcSocketClientFuturesApi _client;

        private readonly Action<DataEvent<MexcFuturesBalanceUpdate>>? _balanceHandler;
        private readonly Action<DataEvent<MexcFuturesOrder>>? _orderHandler;
        private readonly Action<DataEvent<MexcPosition>>? _positionHandler;
        private readonly Action<DataEvent<MexcRiskLimit>>? _riskLimitHandler;
        private readonly Action<DataEvent<MexcAdlUpdate>>? _adlHandler;
        private readonly Action<DataEvent<MexcPositionModeUpdate>>? _positionModeHandler;
        private readonly Action<DataEvent<MexcStopOrder>>? _planOrderHandler;
        private readonly Action<DataEvent<MexcTpSlOrder>>? _tpSlOrderHandler;
        private readonly Action<DataEvent<MexcTrailingOrder>>? _trailingOrderHandler;
        private readonly Action<DataEvent<MexcTpSlPriceUpdate>>? _tpSlPriceUpdate;
        private readonly Action<DataEvent<MexcFuturesUserTradeUpdate>>? _userTradeUpdate;
        private readonly Action<DataEvent<MexcChaseOrderFailure>>? _chaseOrderFailUpdate;
        private readonly Action<DataEvent<MexcLiquidationRiskUpdate>>? _liquidationRiskUpdate;
        private readonly Action<DataEvent<MexcLeverageModeUpdate>>? _leverageModeUpdate;
        private readonly Action<DataEvent<object>>? _closeAllFailUpdate;
        private readonly Action<DataEvent<MexcReversePositionUpdate>>? _reversePositionUpdate;
        private readonly Action<DataEvent<MexcLiquidationUpdate>>? _liquidationUpdate;

        public MexcFuturesUserSubscription(ILogger logger,
            MexcSocketClientFuturesApi client,
            Action<DataEvent<MexcFuturesBalanceUpdate>>? balanceHandler,
            Action<DataEvent<MexcFuturesOrder>>? orderHandler,
            Action<DataEvent<MexcPosition>>? positionHandler,
            Action<DataEvent<MexcRiskLimit>>? riskLimitHandler,
            Action<DataEvent<MexcAdlUpdate>>? adlHandler,
            Action<DataEvent<MexcPositionModeUpdate>>? positionModeHandler,
            Action<DataEvent<MexcStopOrder>>? planOrderHandler,
            Action<DataEvent<MexcTpSlOrder>>? tpSlOrderHandler,
            Action<DataEvent<MexcTrailingOrder>>? trailingOrderHandler,
            Action<DataEvent<MexcTpSlPriceUpdate>>? tpSlPriceUpdate,
            Action<DataEvent<MexcFuturesUserTradeUpdate>>? userTradeUpdate,
            Action<DataEvent<MexcChaseOrderFailure>>? chaseOrderFailUpdate,
            Action<DataEvent<MexcLiquidationRiskUpdate>>? liquidationRiskUpdate,
            Action<DataEvent<MexcLeverageModeUpdate>>? leverageModeUpdate,
            Action<DataEvent<object>>? closeAllFailUpdate,
            Action<DataEvent<MexcReversePositionUpdate>>? reversePositionUpdate,
            Action<DataEvent<MexcLiquidationUpdate>>? liquidationUpdate
            ) : base(logger, true)
        {
            _client = client;
            _balanceHandler = balanceHandler;
            _orderHandler = orderHandler;
            _positionHandler = positionHandler;
            _riskLimitHandler = riskLimitHandler;
            _adlHandler = adlHandler;
            _positionModeHandler = positionModeHandler;
            _planOrderHandler = planOrderHandler;
            _tpSlOrderHandler = tpSlOrderHandler;
            _trailingOrderHandler = trailingOrderHandler;
            _tpSlPriceUpdate = tpSlPriceUpdate;
            _userTradeUpdate = userTradeUpdate;
            _chaseOrderFailUpdate = chaseOrderFailUpdate;
            _liquidationRiskUpdate = liquidationRiskUpdate;
            _leverageModeUpdate = leverageModeUpdate;
            _reversePositionUpdate = reversePositionUpdate;
            _closeAllFailUpdate = closeAllFailUpdate;
            _liquidationUpdate = liquidationUpdate;

            MessageRouter = MessageRouter.Create([
                MessageRoute<MexcFuturesUpdate<MexcFuturesOrder>>.CreateWithoutTopicFilter("push.personal.order", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcFuturesBalanceUpdate>>.CreateWithoutTopicFilter("push.personal.asset", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcPosition>>.CreateWithoutTopicFilter("push.personal.position", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcRiskLimit>>.CreateWithoutTopicFilter("push.personal.risk.limit", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcAdlUpdate>>.CreateWithoutTopicFilter("push.personal.adl.level", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcPositionModeUpdate>>.CreateWithoutTopicFilter("push.personal.position.mode", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcStopOrder>>.CreateWithoutTopicFilter("push.personal.plan.order", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcTpSlOrder>>.CreateWithoutTopicFilter("push.personal.stop.planorder", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcTrailingOrder>>.CreateWithoutTopicFilter("push.personal.track.order", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcTpSlPriceUpdate>>.CreateWithoutTopicFilter("push.personal.stop.order", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcFuturesUserTradeUpdate>>.CreateWithoutTopicFilter("push.personal.order.deal", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcChaseOrderFailure>>.CreateWithoutTopicFilter("push.personal.order.chase", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcLiquidationRiskUpdate>>.CreateWithoutTopicFilter("push.personal.liquidate.risk", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcLeverageModeUpdate>>.CreateWithoutTopicFilter("push.personal.leverage.mode", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcReversePositionUpdate>>.CreateWithoutTopicFilter("push.personal.reverse.position", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<object>>.CreateWithoutTopicFilter("push.personal.position.closeall.fail", DoHandleMessage),
                MessageRoute<MexcFuturesUpdate<MexcLiquidationUpdate>>.CreateWithoutTopicFilter("push.personal.generic.notify", DoHandleMessage),
                ]);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcFuturesBalanceUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _balanceHandler?.Invoke(
                new DataEvent<MexcFuturesBalanceUpdate>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcFuturesOrder> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _orderHandler?.Invoke(
                new DataEvent<MexcFuturesOrder>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcPosition> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _positionHandler?.Invoke(
                new DataEvent<MexcPosition>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcRiskLimit> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _riskLimitHandler?.Invoke(
                new DataEvent<MexcRiskLimit>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcPositionModeUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _positionModeHandler?.Invoke(
                new DataEvent<MexcPositionModeUpdate>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcAdlUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _adlHandler?.Invoke(
                new DataEvent<MexcAdlUpdate>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcStopOrder> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _planOrderHandler?.Invoke(
                new DataEvent<MexcStopOrder>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcTpSlOrder> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _tpSlOrderHandler?.Invoke(
                new DataEvent<MexcTpSlOrder>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcTrailingOrder> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _trailingOrderHandler?.Invoke(
                new DataEvent<MexcTrailingOrder>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcTpSlPriceUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _tpSlPriceUpdate?.Invoke(
                new DataEvent<MexcTpSlPriceUpdate>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcFuturesUserTradeUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _userTradeUpdate?.Invoke(
                new DataEvent<MexcFuturesUserTradeUpdate>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcChaseOrderFailure> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _chaseOrderFailUpdate?.Invoke(
                new DataEvent<MexcChaseOrderFailure>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcLiquidationRiskUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _liquidationRiskUpdate?.Invoke(
                new DataEvent<MexcLiquidationRiskUpdate>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcLeverageModeUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _leverageModeUpdate?.Invoke(
                new DataEvent<MexcLeverageModeUpdate>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _closeAllFailUpdate?.Invoke(
                new DataEvent<object>(MexcExchange.ExchangeName, new object(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcReversePositionUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _reversePositionUpdate?.Invoke(
                new DataEvent<MexcReversePositionUpdate>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate<MexcLiquidationUpdate> message)
        {
            _client.UpdateTimeOffset(message.Timestamp);

            _liquidationUpdate?.Invoke(
                new DataEvent<MexcLiquidationUpdate>(MexcExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp, _client.GetTimeOffset()));
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
