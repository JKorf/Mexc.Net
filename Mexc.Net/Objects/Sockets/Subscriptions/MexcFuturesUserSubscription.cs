using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Mexc.Net.Clients.FuturesApi;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Objects.Models.Protobuf.Futures;
using Mexc.Net.Objects.Sockets.Models;
using MexcFuturesUpdate = Mexc.Net.Objects.Models.Protobuf.Futures.MexcFuturesUpdate;

namespace Mexc.Net.Objects.Sockets.Subscriptions
{
    internal class MexcFuturesUserSubscription : Subscription
    {
        private readonly MexcSocketClientFuturesApi _client;

        private readonly Action<DataEvent<MexcFuturesBalanceUpdate>>? _balanceHandler;
        private readonly Action<DataEvent<MexcFuturesOrder>>? _orderHandler;
        private readonly Action<DataEvent<MexcPosition>>? _positionHandler;
        private readonly Action<DataEvent<MexcRiskLimit>>? _riskLimitHandler;
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
                MessageRoute.CreateForEvent<MexcUpdateFuturesOrder>("pb", "push.personal.order", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesBalance>("pb", "push.personal.asset", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesPosition>("pb", "push.personal.position", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesUserRiskLimit>("pb", "push.personal.risk.limit", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesPositionMode>("pb", "push.personal.position.mode", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesPlanOrder>("pb", "push.personal.plan.order", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesTpSlOrder>("pb", "push.personal.stop.planorder", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesTrailingOrder>("pb", "push.personal.track.order", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesTpSlPrice>("pb", "push.personal.stop.order", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesUserTrade>("pb", "push.personal.order.deal", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesChaseOrderFailure>("pb", "push.personal.order.chase", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesLiquidationRisk>("pb", "push.personal.liquidate.risk", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesLeverageMode>("pb", "push.personal.leverage.mode", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesReversePosition>("pb", "push.personal.reverse.position", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcFuturesUpdate>("pb", "push.personal.position.closeall.fail", DoHandleMessage),
                MessageRoute.CreateForEvent<MexcUpdateFuturesLiquidation>("pb", "push.personal.generic.notify", DoHandleMessage),
            ]);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesBalance message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _balanceHandler?.Invoke(
                new DataEvent<MexcFuturesBalanceUpdate>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesOrder message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _orderHandler?.Invoke(
                new DataEvent<MexcFuturesOrder>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesPosition message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _positionHandler?.Invoke(
                new DataEvent<MexcPosition>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesUserRiskLimit message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _riskLimitHandler?.Invoke(
                new DataEvent<MexcRiskLimit>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesPositionMode message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _positionModeHandler?.Invoke(
                new DataEvent<MexcPositionModeUpdate>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesPlanOrder message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _planOrderHandler?.Invoke(
                new DataEvent<MexcStopOrder>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesTpSlOrder message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _tpSlOrderHandler?.Invoke(
                new DataEvent<MexcTpSlOrder>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesTrailingOrder message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _trailingOrderHandler?.Invoke(
                new DataEvent<MexcTrailingOrder>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesTpSlPrice message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _tpSlPriceUpdate?.Invoke(
                new DataEvent<MexcTpSlPriceUpdate>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesUserTrade message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _userTradeUpdate?.Invoke(
                new DataEvent<MexcFuturesUserTradeUpdate>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesChaseOrderFailure message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _chaseOrderFailUpdate?.Invoke(
                new DataEvent<MexcChaseOrderFailure>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesLiquidationRisk message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _liquidationRiskUpdate?.Invoke(
                new DataEvent<MexcLiquidationRiskUpdate>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesLeverageMode message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _leverageModeUpdate?.Invoke(
                new DataEvent<MexcLeverageModeUpdate>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcFuturesUpdate message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _closeAllFailUpdate?.Invoke(
                new DataEvent<object>(MexcExchange.ExchangeName, new object(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesReversePosition message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            _reversePositionUpdate?.Invoke(
                new DataEvent<MexcReversePositionUpdate>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, MexcUpdateFuturesLiquidation message)
        {
            var timestamp = DateTimeConverter.ParseFromDecimal(message.CreateTime);
            _client.UpdateTimeOffset(timestamp);

            if (message.Type != 1 || message.Param == null)
                return CallResult.Ok();

            _liquidationUpdate?.Invoke(
                new DataEvent<MexcLiquidationUpdate>(MexcExchange.ExchangeName, message.ToModel(), receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(timestamp, _client.GetTimeOffset()));
            return CallResult.Ok();
        }


        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return null;
            //return new MexcFuturesQuery("personal.filter", new Dictionary<string, object>() { { "filters", new SubFilter[0] } }, Authenticated);
        }

        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return null;
            //// There doesn't seem to be a way to unsubscribe from all user updates
            //// Work around is to filter all message except a single one which shouldn't trigger too often
            //var filters = new[] { "adl.level" };
            //return new MexcFuturesQuery("personal.filter", new Dictionary<string, object>()
            //{
            //    { "filters", filters.Select(x => new SubFilter{ Filter = x }).ToArray() }
            //}, Authenticated);
        }

        internal record SubFilter
        {
            [JsonPropertyName("filter")]
            public string Filter { get; set; } = string.Empty;
        }
    }
}
