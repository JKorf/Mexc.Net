using Mexc.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using CryptoExchange.Net;

namespace Mexc.Net.Clients.SpotApi
{
    internal partial class MexcSocketClientSpotSharedApi
    {

        #region Subscribe Spot Orders

        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
            => await SubscribeToSpotOrderUpdatesAsync(request, x => handler(x.ToType<SharedSpotOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeSpotOrderOptions SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToOrderUpdatesAsync(
                update => handler(update.ToType<SharedSpotOrderUpdate[]>(new[] {
                        new SharedSpotOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol),
                            update.Symbol!,
                            update.Data.OrderId!,
                            update.Data.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : update.Data.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(update.Data.Status),
                            update.Data.Timestamp)
                        {
                            ClientOrderId = update.Data.ClientOrderId,
                            OrderPrice = update.Data.Price,
                            OrderQuantity = new SharedOrderQuantity(update.Data.Quantity == 0 ? null : update.Data.Quantity, update.Data.QuoteQuantity),
                            QuantityFilled = new SharedOrderQuantity(update.Data.CumulativeQuantity, update.Data.CumulativeQuoteQuantity),
                            AveragePrice = update.Data.AveragePrice == 0 ? null : update.Data.AveragePrice,
                            UpdateTime = update.Data.Timestamp,
                            TimeInForce = update.Data.OrderType == Enums.OrderType.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : update.Data.OrderType == Enums.OrderType.FillOrKill ? SharedTimeInForce.FillOrKill : null
                        }
                })),
                ct: ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == Enums.OrderStatus.Canceled || status == Enums.OrderStatus.PartiallyCanceled)
                return SharedOrderStatus.Canceled;
            if (status == Enums.OrderStatus.New || status == Enums.OrderStatus.PartiallyFilled)
                return SharedOrderStatus.Open;
            if (status == OrderStatus.Filled)
                return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }
    }
}
