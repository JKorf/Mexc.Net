using Mexc.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using CryptoExchange.Net;

namespace Mexc.Net.Clients.FuturesApi
{
    internal partial class MexcSocketClientFuturesSharedApi
    {
        #region Futures Order client

        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
            => await SubscribeToFuturesOrderUpdatesAsync(request, x => handler(x.ToType<SharedFuturesOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeFuturesOrderOptions SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                orderUpdateHandler: update => handler(update.ToType(new[] {
                    new SharedFuturesOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol), update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        ParseOrderType(update.Data.OrderType),
                        (update.Data.OrderSide == FuturesOrderSide.OpenLong || update.Data.OrderSide == FuturesOrderSide.CloseShort) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.Status),
                        update.Data.UpdateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId,
                        OrderPrice = update.Data.Price == 0 ? null : update.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: update.Data.Quantity),
                        QuantityFilled = new SharedOrderQuantity(contractQuantity : update.Data.QuantityFilled),
                        UpdateTime = update.Data.UpdateTime,
                        TimeInForce = ParseTimeInForce(update.Data.OrderType),
#pragma warning disable CS0618 // Type or member is obsolete
                        Fee = update.Data.MakerFee + update.Data.TakerFee,
                        FeeAsset = update.Data.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                        Leverage = update.Data.Leverage,
                        StopLossPrice = update.Data.StopLossPrice,
                        TakeProfitPrice = update.Data.TakeProfitPrice,
                        AveragePrice = update.Data.AveragePrice == 0 ? null : update.Data.AveragePrice,
                        PositionSide = (update.Data.OrderSide == FuturesOrderSide.OpenLong || update.Data.OrderSide == FuturesOrderSide.CloseLong) ? SharedPositionSide.Long : SharedPositionSide.Short                        
                    }
                })),
                ct: ct).ConfigureAwait(false);
            return result;
        }

        private SharedOrderStatus ParseOrderStatus(FuturesOrderStatus status)
        {
            if (status == FuturesOrderStatus.Open)
                return SharedOrderStatus.Open;

            if (status == FuturesOrderStatus.Canceled || status == FuturesOrderStatus.Invalid)
                return SharedOrderStatus.Canceled;

            return SharedOrderStatus.Filled;
        }

        private SharedTimeInForce? ParseTimeInForce(FuturesOrderType orderType)
        {
            if (orderType == FuturesOrderType.ImmediateOrCancel || orderType == FuturesOrderType.Market)
                return SharedTimeInForce.ImmediateOrCancel;

            if (orderType == FuturesOrderType.FillOrKill)
                return SharedTimeInForce.FillOrKill;

            if (orderType == FuturesOrderType.Limit)
                return SharedTimeInForce.GoodTillCanceled;

            return null;
        }

        private SharedOrderType ParseOrderType(FuturesOrderType type)
        {
            if (type == FuturesOrderType.Market)
                return SharedOrderType.Market;

            if (type == FuturesOrderType.Limit)
                return SharedOrderType.Limit;

            if (type == FuturesOrderType.LimitMaker)
                return SharedOrderType.LimitMaker;

            return SharedOrderType.Other;
        }
        #endregion
    }
}
