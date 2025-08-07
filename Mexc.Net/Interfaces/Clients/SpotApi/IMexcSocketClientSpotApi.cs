using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot API socket subscriptions and requests
    /// </summary>
    public interface IMexcSocketClientSpotApi: ISocketApiClient
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IMexcSocketClientSpotApiShared SharedClient { get; }

        /// <summary>
        /// During reconnection the listenkey which was provided can be renewed by the client. This means the keep-alive mechanism should use this new listen key.
        /// </summary>
        public event Action<ListenKeyRenewedEvent>? ListenkeyRenewed;

        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#trade-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<MexcStreamTrade[]>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#trade-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="updateInterval">Interval for updates, either 10 or 100 ms</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, int updateInterval, Action<DataEvent<MexcStreamTrade[]>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#trade-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe, for example `BTCUSDT`</param>
        /// <param name="updateInterval">Interval for updates, either 10 or 100 ms</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, int updateInterval, Action<DataEvent<MexcStreamTrade[]>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#kline-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="interval">The interval of the candles</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#kline-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSDT`</param>
        /// <param name="interval">The interval of the candles</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to orderbook change updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#diff-depth-stream" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to orderbook change updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#diff-depth-stream" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="updateInterval">Interval for updates, either 10 or 100 ms</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int updateInterval, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to orderbook change updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#diff-depth-stream" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSDT`</param>
        /// <param name="updateInterval">Interval for updates, either 10 or 100 ms</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int updateInterval, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to orderbook change updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#diff-depth-stream" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcStreamOrderBook[]>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to orderbook change updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#diff-depth-stream" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamOrderBook[]>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full orderbook updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="depth">The depth of the book, 5, 10 or 20</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full orderbook updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSDT`</param>
        /// <param name="depth">The depth of the book, 5, 10 or 20</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker (best bid/ask) updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#individual-symbol-book-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker (best bid/ask) updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#individual-symbol-book-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to account balance updates. Prior to using this, the <see cref="IMexcRestClientSpotApiAccount.StartUserStreamAsync(CancellationToken)">restClient.SpotApi.Account.StartUserStreamAsync</see> method should be called to start the stream and obtaining a listen key.
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#spot-account-upadte" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the <see cref="IMexcRestClientSpotApiAccount.StartUserStreamAsync(CancellationToken)">restClient.SpotApi.Account.StartUserStreamAsync</see> method</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(string listenKey, Action<DataEvent<MexcAccountUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to account order updates. Prior to using this, the <see cref="IMexcRestClientSpotApiAccount.StartUserStreamAsync(CancellationToken)">restClient.SpotApi.Account.StartUserStreamAsync</see> method should be called to start the stream and obtaining a listen key.
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#spot-account-orders" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the <see cref="IMexcRestClientSpotApiAccount.StartUserStreamAsync(CancellationToken)">restClient.SpotApi.Account.StartUserStreamAsync</see> method</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string listenKey, Action<DataEvent<MexcUserOrderUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to account trade updates. Prior to using this, the <see cref="IMexcRestClientSpotApiAccount.StartUserStreamAsync(CancellationToken)">restClient.SpotApi.Account.StartUserStreamAsync</see> method should be called to start the stream and obtaining a listen key.
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#spot-account-deals" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the <see cref="IMexcRestClientSpotApiAccount.StartUserStreamAsync(CancellationToken)">restClient.SpotApi.Account.StartUserStreamAsync</see> method</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string listenKey, Action<DataEvent<MexcUserTradeUpdate>> handler, CancellationToken ct = default);
    }
}
