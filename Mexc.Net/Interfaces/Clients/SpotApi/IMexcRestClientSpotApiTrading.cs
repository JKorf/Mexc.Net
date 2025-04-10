using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Mexc Spot trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IMexcRestClientSpotApiTrading
    {
        /// <summary>
        /// Place a new test order. Only validates the rules, doesn't actually place any order
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#test-new-order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="type">Order type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="quoteQuantity">Quote quantity</param>
        /// <param name="price">Limit price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult> PlaceTestOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#new-order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="type">Order type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="quoteQuantity">Quote quantity</param>
        /// <param name="price">Limit price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple new orders in a single request
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#batch-orders" /></para>
        /// </summary>
        /// <param name="requests">Order requests, max 20</param>
        /// <param name="ct">Cancelation Token</param>
        Task<WebCallResult<CallResult<MexcOrderResult>[]>> PlaceMultipleOrdersAsync(IEnumerable<MexcPlaceOrderRequest> requests, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#cancel-order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="orderId">Cancel by order id</param>
        /// <param name="clientOrderId">Cancel by client order id</param>
        /// <param name="newClientOrderId">New client order id after canceled</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder>> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, string? newClientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on a symbol
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#cancel-all-open-orders-on-a-symbol" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to close all orders on, for example `BTCUSDT`</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder[]>> CancelAllOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on a symbol
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#cancel-all-open-orders-on-a-symbol" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to close all orders on (max 5), for example `BTCUSDT`</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder[]>> CancelAllOrdersAsync(IEnumerable<string> symbols, CancellationToken ct = default);

        /// <summary>
        /// Get an order
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSDT`</param>
        /// <param name="orderId">Get by order id</param>
        /// <param name="clientOrderId">Get by client order id</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder>> GetOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get all open orders for a symbol
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#current-open-orders" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `BTCUSDT`</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder[]>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get all orders
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#all-orders" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `BTCUSDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max results</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder[]>> GetOrdersAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#account-trade-list" /></para>
        /// </summary>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="symbol">Symbol, for example `BTCUSDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max results</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcUserTrade[]>> GetUserTradesAsync(string symbol, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
    }
}
