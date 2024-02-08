using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot API socket subscriptions and requests
    /// </summary>
    public interface IMexcSocketClientSpotApi: ISocketApiClient
    {
        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#trade-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<MexcStreamTrade>>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#trade-streams" /></para>
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<MexcStreamTrade>>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#kline-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candles</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#kline-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candles</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to orderbook change updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#diff-depth-stream" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to orderbook change updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#diff-depth-stream" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full orderbook updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="depth">The depth of the book, 5, 10 or 20</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full orderbook updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="depth">The depth of the book, 5, 10 or 20</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker (best bid/ask) updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#individual-symbol-book-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker (best bid/ask) updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#individual-symbol-book-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mini ticker updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#miniticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="timezone">The timezone to base the statistics on, in the vorm of `UTC+1`. Defaults to `UTC+0`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<MexcStreamMiniTick>> handler, string? timezone = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mini ticker updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#miniticker" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="timezone">The timezone to base the statistics on, in the vorm of `UTC+1`. Defaults to `UTC+0`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamMiniTick>> handler, string? timezone = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mini ticker updates for all symbols
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#minitickers" /></para>
        /// </summary>
        /// <param name="timezone">The timezone to base the statistics on, in the vorm of `UTC+1`. Defaults to `UTC+0`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(Action<DataEvent<IEnumerable<MexcStreamMiniTick>>> handler, string? timezone = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to account balance updates. Listenkey can be retrieved by calling `SpotApi.Account.StartUserStreamAsync()` on the `MexcRestClient`
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#spot-account-upadte" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(string listenKey, Action<DataEvent<MexcAccountUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to account order updates. Listenkey can be retrieved by calling `SpotApi.Account.StartUserStreamAsync()` on the `MexcRestClient`
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#spot-account-orders" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string listenKey, Action<DataEvent<MexcUserOrderUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to account trade updates. Listenkey can be retrieved by calling `SpotApi.Account.StartUserStreamAsync()` on the `MexcRestClient`
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#spot-account-deals" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string listenKey, Action<DataEvent<MexcUserTradeUpdate>> handler, CancellationToken ct = default);
    }
}
