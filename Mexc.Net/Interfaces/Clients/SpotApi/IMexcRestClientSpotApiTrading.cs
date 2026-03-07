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
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#test-new-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/order/test
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `BTCUSDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="type">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity</param>
        /// <param name="quoteQuantity">["<c>quoteOrderQty</c>"] Quote quantity</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="clientOrderId">["<c>newClientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult> PlaceTestOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#new-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `BTCUSDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="type">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity</param>
        /// <param name="quoteQuantity">["<c>quoteOrderQty</c>"] Quote quantity</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="clientOrderId">["<c>newClientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple new orders in a single request
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#batch-orders" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/batchOrders
        /// </para>
        /// </summary>
        /// <param name="requests">["<c>batchOrders</c>"] Order requests, max 20</param>
        /// <param name="ct">Cancelation Token</param>
        Task<WebCallResult<CallResult<MexcOrderResult>[]>> PlaceMultipleOrdersAsync(IEnumerable<MexcPlaceOrderRequest> requests, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#cancel-order" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `BTCUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Cancel by order id</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Cancel by client order id</param>
        /// <param name="newClientOrderId">["<c>newClientOrderId</c>"] New client order id after canceled</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder>> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, string? newClientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#cancel-all-open-orders-on-a-symbol" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to close all orders on, for example `BTCUSDT`</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder[]>> CancelAllOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#cancel-all-open-orders-on-a-symbol" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbol</c>"] The symbols to close all orders on (max 5), for example `BTCUSDT`</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder[]>> CancelAllOrdersAsync(IEnumerable<string> symbols, CancellationToken ct = default);

        /// <summary>
        /// Get an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#query-order" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `BTCUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Get by order id</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Get by client order id</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder>> GetOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get all open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#current-open-orders" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `BTCUSDT`</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder[]>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get all orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#all-orders" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/allOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `BTCUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max results</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder[]>> GetOrdersAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#account-trade-list" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/myTrades
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `BTCUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max results</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcUserTrade[]>> GetUserTradesAsync(string symbol, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
    }
}
