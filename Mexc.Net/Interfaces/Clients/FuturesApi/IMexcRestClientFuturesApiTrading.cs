using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Mexc Futures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IMexcRestClientFuturesApiTrading
    {
        /// <summary>
        /// Get open orders
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-user-39-s-current-pending-order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrder[]>> GetOpenOrdersAsync(string symbol, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-all-of-the-user-39-s-historical-orders" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="category">Filter by category</param>
        /// <param name="side">Filter by side</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrder[]>> GetOrderHistoryAsync(string? symbol = null, IEnumerable<FuturesOrderStatus>? status = null, OrderCategory? category = null, FuturesOrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get order by client order id
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#query-the-order-based-on-the-external-number" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrder>> GetOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get order by id
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#query-the-order-based-on-the-order-number" /></para>
        /// </summary>
        /// <param name="orderId">The order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrder>> GetOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get orders by id
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#query-the-order-in-bulk-based-on-the-order-number" /></para>
        /// </summary>
        /// <param name="orderIds">Ids of the orders to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrder[]>> GetOrdersByIdAsync(IEnumerable<string> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Get trades for an order
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-order-transaction-details-based-on-the-order-id" /></para>
        /// </summary>
        /// <param name="orderId">The order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesUserTrade[]>> GetOrderTradesAsync(string orderId, CancellationToken ct = default);
        
        /// <summary>
        /// Get user trades
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-all-transaction-details-of-the-user-s-order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesUserTrade[]>> GetUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get trigger orders
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-all-of-the-user-39-s-historical-orders" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesTriggerOrder[]>> GetTriggerOrdersAsync(string? symbol = null, IEnumerable<FuturesOrderStatus>? status = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get stop orders
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-stop-limit-order-list" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="finished">Is finished</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcStopOrder[]>> GetStopOrdersAsync(string? symbol = null, bool? finished = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get risk limits
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-risk-limits" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<Dictionary<string, MexcRiskLimit[]>>> GetRiskLimitsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get position history
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-user-s-history-position-information" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="positionSide">Filter by position side</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcPosition[]>> GetPositionHistoryAsync(string? symbol = null, PositionSide? positionSide = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get current open positions
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-user-39-s-current-holding-position" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcPosition[]>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default);

    }
}
