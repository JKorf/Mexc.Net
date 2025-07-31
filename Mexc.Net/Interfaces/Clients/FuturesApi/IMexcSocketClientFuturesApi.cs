using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Futures API socket subscriptions and requests
    /// </summary>
    public interface IMexcSocketClientFuturesApi : ISocketApiClient
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IMexcSocketClientFuturesApiShared SharedClient { get; }

        /// <summary>
        /// Subscribe to ticker updates for all symbols
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /></para>
        /// </summary>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToTickersUpdatesAsync(Action<DataEvent<MexcFuturesTickerUpdate[]>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates for a symbol
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesTicker>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates for a symbol
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesTrade>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, FuturesKlineInterval interval, Action<DataEvent<MexcFuturesStreamKline>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full book updates for the first rows of the order book
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="limit">Number of rows, 5, 10 or 20</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int? limit, Action<DataEvent<MexcFuturesOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<MexcFundingRateUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates for a symbol
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<MexcPriceUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates for a symbol
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<MexcPriceUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user data updates
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#private-channels" /></para>
        /// </summary>
        /// <param name="balanceUpdateHandler">Balance update handler</param>
        /// <param name="orderUpdateHandler">Order update handler</param>
        /// <param name="positionUpdateHandler">Position update handler</param>
        /// <param name="riskLimitUpdateHandler">Risk limit update handler</param>
        /// <param name="adlUpdateHandler">Adl update handler</param>
        /// <param name="positionModeUpdateHandler">Position update handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            Action<DataEvent<MexcFuturesBalanceUpdate>>? balanceUpdateHandler = null,
            Action<DataEvent<MexcFuturesOrder>>? orderUpdateHandler = null,
            Action<DataEvent<MexcPosition>>? positionUpdateHandler = null,
            Action<DataEvent<MexcRiskLimit>>? riskLimitUpdateHandler = null,
            Action<DataEvent<MexcAdlUpdate>>? adlUpdateHandler = null,
            Action<DataEvent<MexcPositionModeUpdate>>? positionModeUpdateHandler = null,
            CancellationToken ct = default);
    }
}
