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
        #region User Trade client

        public SubscribeUserTradeOptions SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserTradeUpdatesAsync(
                update => handler(update.ToType<SharedUserTrade[]>(new[] {
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol),
                            update.Symbol!,
                            update.Data.OrderId,
                            update.Data.TradeId.ToString(),
                            update.Data.TradeSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            new SharedOrderQuantity(update.Data.Quantity, update.Data.QuoteQuantity),
                            update.Data.Price,
                            update.Data.TradeTime)
                        {
                            ClientOrderId = update.Data.ClientOrderId,
                            Role = update.Data.IsMaker ? SharedRole.Maker : SharedRole.Taker,
                            Fee = update.Data.Fee,
                            FeeAsset = update.Data.FeeAsset
                        }
                })),
                ct: ct).ConfigureAwait(false);
            return result;
        }


        #endregion
    }
}
