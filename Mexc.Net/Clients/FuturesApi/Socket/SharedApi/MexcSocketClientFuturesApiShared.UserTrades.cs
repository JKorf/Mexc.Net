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
        #region Subscribe User Trades

        public SubscribeUserTradeOptions SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                userTradeUpdate: update => handler(update.ToType<SharedUserTrade[]>(new[] {
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol) ?? ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        update.Data.Id.ToString(),
                        (update.Data.Side == FuturesOrderSide.OpenLong || update.Data.Side == FuturesOrderSide.CloseShort) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        new SharedOrderQuantity(contractQuantity: update.Data.Quantity),
                        update.Data.Price,
                        update.Data.Timestamp)
                    {
                        Role = update.Data.IsTaker ? SharedRole.Taker : SharedRole.Maker,
                        Fee = update.Data.Fee,
                        FeeAsset = update.Data.FeeAsset,
                    }
                })),
                ct: ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
