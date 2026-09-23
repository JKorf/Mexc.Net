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
        #region Subscribe Order Book

        public SubscribeOrderBookOptions SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 5, 10, 20 });
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToPartialOrderBookUpdatesAsync(symbol, request.Limit ?? 5, update => handler(
                update.ToType(
                    new SharedOrderBook(SharedQuantityType.Contracts, update.Data.SequenceEnd, update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
