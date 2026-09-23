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

        #region Subscribe Book Ticker

        public SubscribeBookTickerOptions SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false)
        {
            MaxSymbolCount = 18,
            SupportsMultipleSymbols = true
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToBookTickerUpdatesAsync(symbols, update => handler(update.ToType(
                new SharedBookTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol), 
                    update.Symbol!,
                    update.Data.BestAskPrice,
                    new SharedOrderQuantity(update.Data.BestAskQuantity), 
                    update.Data.BestBidPrice,
                    new SharedOrderQuantity(update.Data.BestBidQuantity)))), ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
