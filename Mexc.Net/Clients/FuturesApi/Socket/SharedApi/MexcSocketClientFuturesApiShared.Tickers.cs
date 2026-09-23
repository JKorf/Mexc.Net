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
        #region Subscribe Ticker

        async Task<WebSocketResult<UpdateSubscription>> ISubscribeTickerSocket.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
            => await SubscribeToTickerUpdatesAsync(request, x => handler(x.ToType<SharedTicker>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickerOptions SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToTickerUpdatesAsync(symbol, update => handler(update.ToType(
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol),
                    symbol,
                    update.Data.LastPrice,
                    update.Data.HighPrice,
                    update.Data.LowPrice,
                    new SharedOrderQuantity(null, update.Data.QuoteVolume24h, update.Data.Volume24h),
                    update.Data.ChangePercentage)

            {
            })), ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Subscribe All Tickers

        async Task<WebSocketResult<UpdateSubscription>> ISubscribeAllTickersSocket.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedTicker[]>> handler, CancellationToken ct)
            => await SubscribeToAllTickersUpdatesAsync(request, x => handler(x.ToType<SharedTicker[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickersOptions SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions(_exchangeName);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToTickersUpdatesAsync( update => handler(update.ToType(update.Data.Select(x => 
            new SharedSpotTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol, 
                x.LastPrice,
                x.HighPrice, 
                x.LowPrice,
                new SharedOrderQuantity(null, x.QuoteVolume24h, x.Volume24h),
                x.ChangePercentage)
            {
            }).ToArray())), ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
