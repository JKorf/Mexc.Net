using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Futures API socket subscriptions and requests
    /// </summary>
    public interface IMexcSocketClientFuturesApi : ISocketApiClient<MexcCredentials>
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IMexcSocketClientFuturesApiShared SharedClient { get; }

        /// <summary>
        /// Subscribe to ticker updates for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /><br />
        /// Endpoint:<br />
        /// tickers
        /// </para>
        /// </summary>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToTickersUpdatesAsync(Action<DataEvent<MexcFuturesTickerUpdate[]>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /><br />
        /// Endpoint:<br />
        /// ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesTicker>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /><br />
        /// Endpoint:<br />
        /// deal
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesTrade[]>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /><br />
        /// Endpoint:<br />
        /// kline
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, FuturesKlineInterval interval, Action<DataEvent<MexcFuturesStreamKline>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /><br />
        /// Endpoint:<br />
        /// depth
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcFuturesOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full book updates for the first rows of the order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /><br />
        /// Endpoint:<br />
        /// depth.full
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="limit">Number of rows, 5, 10 or 20</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int? limit, Action<DataEvent<MexcFuturesOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /><br />
        /// Endpoint:<br />
        /// funding.rate
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<MexcFundingRateUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /><br />
        /// Endpoint:<br />
        /// index.price
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<MexcPriceUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /><br />
        /// Endpoint:<br />
        /// fair.price
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<MexcPriceUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to contract/symbol updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#public-channels" /><br />
        /// Endpoint:<br />
        /// contract
        /// </para>
        /// </summary>
        /// <param name="handler">Data handler</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(Action<DataEvent<MexcContract>> handler, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user data updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#private-channels" /><br />
        /// Endpoint:<br />
        /// personal.filter
        /// </para>
        /// </summary>
        /// <param name="balanceUpdateHandler">Balance update handler</param>
        /// <param name="orderUpdateHandler">Order update handler</param>
        /// <param name="positionUpdateHandler">Position update handler</param>
        /// <param name="riskLimitUpdateHandler">Risk limit update handler</param>
        /// <param name="adlUpdateHandler">Adl update handler</param>
        /// <param name="positionModeUpdateHandler">Position update handler</param>
        /// <param name="planOrderHandler">Plan order update handler</param>
        /// <param name="tpSlOrderHandler">Take profit/stop loss order update handler</param>
        /// <param name="trailingOrderHandler">Trailing order update handler</param>
        /// <param name="tpSlPriceUpdate">Take profit/stop loss price update handler</param>
        /// <param name="userTradeUpdate">User trade update</param>
        /// <param name="chaseOrderFailUpdate">Chase order failed update</param>
        /// <param name="liquidationRiskUpdate">Liquidation risk update</param>
        /// <param name="leverageModeUpdate">Leverage mode update</param>
        /// <param name="closeAllFailUpdate">Close all orders failed update</param>
        /// <param name="reversePositionUpdate">Reverse position update</param>
        /// <param name="liquidationUpdate">Liquidation (warning) update</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            Action<DataEvent<MexcFuturesBalanceUpdate>>? balanceUpdateHandler = null,
            Action<DataEvent<MexcFuturesOrder>>? orderUpdateHandler = null,
            Action<DataEvent<MexcPosition>>? positionUpdateHandler = null,
            Action<DataEvent<MexcRiskLimit>>? riskLimitUpdateHandler = null,
            Action<DataEvent<MexcAdlUpdate>>? adlUpdateHandler = null,
            Action<DataEvent<MexcPositionModeUpdate>>? positionModeUpdateHandler = null,
            Action<DataEvent<MexcStopOrder>>? planOrderHandler = null,
            Action<DataEvent<MexcTpSlOrder>>? tpSlOrderHandler = null,
            Action<DataEvent<MexcTrailingOrder>>? trailingOrderHandler = null,
            Action<DataEvent<MexcTpSlPriceUpdate>>? tpSlPriceUpdate = null,
            Action<DataEvent<MexcFuturesUserTradeUpdate>>? userTradeUpdate = null,
            Action<DataEvent<MexcChaseOrderFailure>>? chaseOrderFailUpdate = null,
            Action<DataEvent<MexcLiquidationRiskUpdate>>? liquidationRiskUpdate = null,
            Action<DataEvent<MexcLeverageModeUpdate>>? leverageModeUpdate = null,
            Action<DataEvent<object>>? closeAllFailUpdate = null,
            Action<DataEvent<MexcReversePositionUpdate>>? reversePositionUpdate = null,
            Action<DataEvent<MexcLiquidationUpdate>>? liquidationUpdate = null,
            CancellationToken ct = default);
    }
}
