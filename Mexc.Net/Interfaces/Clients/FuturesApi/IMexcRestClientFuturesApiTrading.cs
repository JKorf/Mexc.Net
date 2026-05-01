using Mexc.Net.Enums;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Futures;
using System;

namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Mexc Futures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IMexcRestClientFuturesApiTrading
    {
        /// <summary>
        /// Get open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#get-current-orders" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/order/list/open_orders/{symbol}
        /// </para>
        /// </summary>
        /// <param name="page">["<c>page_num</c>"] Page number</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrder[]>> GetOpenOrdersAsync(int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-all-of-the-user-39-s-historical-orders" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/order/list/history_orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="status">["<c>states</c>"] Filter by status</param>
        /// <param name="category">["<c>category</c>"] Filter by category</param>
        /// <param name="side">["<c>side</c>"] Filter by side</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="page">["<c>page_num</c>"] Page number</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrder[]>> GetOrderHistoryAsync(string? symbol = null, IEnumerable<FuturesOrderStatus>? status = null, OrderCategory? category = null, FuturesOrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get order by client order id
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#query-the-order-based-on-the-external-number" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/order/external/{symbol}/{clientOrderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrder>> GetOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get order by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#query-the-order-based-on-the-order-number" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/order/get/{orderId}
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] The order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrder>> GetOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get orders by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#query-the-order-in-bulk-based-on-the-order-number" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/order/batch_query
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>order_ids</c>"] Ids of the orders to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrder[]>> GetOrdersByIdAsync(IEnumerable<long> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Get trades for an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-order-transaction-details-based-on-the-order-id" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/order/deal_details/{orderId}
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] The order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesUserTrade[]>> GetOrderTradesAsync(long orderId, CancellationToken ct = default);
        
        /// <summary>
        /// Get user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-all-transaction-details-of-the-user-s-order" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/order/list/order_deals
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="page">["<c>page_num</c>"] Page number</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesUserTrade[]>> GetUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get trigger orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-all-of-the-user-39-s-historical-orders" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/planorder/list/orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="status">["<c>states</c>"] Filter by status</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="page">["<c>page_num</c>"] Page number</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesTriggerOrder[]>> GetTriggerOrdersAsync(string? symbol = null, IEnumerable<TpSlStatus>? status = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get stop orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-stop-limit-order-list" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/stoporder/list/orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="finished">["<c>is_finished</c>"] Is finished</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="page">["<c>page_num</c>"] Page number</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcStopOrder[]>> GetTpSlOrdersAsync(string? symbol = null, bool? finished = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get risk limits
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-risk-limits" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/account/risk_limit
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<Dictionary<string, MexcRiskLimit[]>>> GetRiskLimitsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get position history
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-user-s-history-position-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/position/list/history_positions
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Filter by position side</param>
        /// <param name="page">["<c>page_num</c>"] Page number</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcPosition[]>> GetPositionHistoryAsync(string? symbol = null, PositionSide? positionSide = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get current open positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-user-39-s-current-holding-position" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/position/open_positions
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcPosition[]>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#place-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/order/create<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="side">["<c>side</c>"] Side</param>
        /// <param name="type">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>vol</c>"] Quantity in number of contracts</param>
        /// <param name="price">["<c>price</c>"] Price</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="marginType">["<c>openType</c>"] Margin type</param>
        /// <param name="clientOrderId">["<c>externalOid</c>"] Client order id</param>
        /// <param name="positionId">["<c>positionId</c>"] Position id</param>
        /// <param name="stopLossPrice">["<c>stopLossPrice</c>"] Stop loss price</param>
        /// <param name="takeProfitPrice">["<c>takeProfitPrice</c>"] Take profit price</param>
        /// <param name="stopLossPriceType">["<c>lossTrend</c>"] Stop loss trigger price type</param>
        /// <param name="takeProfitPriceType">["<c>profitTrend</c>"] Take profit trigger price type</param>
        /// <param name="priceProtect">["<c>priceProtect</c>"] Price protect</param>
        /// <param name="positionMode">["<c>positionMode</c>"] Position mode</param>
        /// <param name="reduceOnly">["<c>reduceOnly</c>"] Reduce only</param>
        /// <param name="marketCeiling">["<c>marketCeiling</c>"] Market ceiling</param>
        /// <param name="flashClose">["<c>flashClose</c>"] Flash close</param>
        /// <param name="limitOrderType">["<c>bboTypeNum</c>"] Limit order type</param>
        /// <param name="stpMode">["<c>stpMode</c>"] Self trade prevention mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrderResult>> PlaceOrderAsync(
            string symbol, 
            FuturesOrderSide side,
            FuturesOrderType type, 
            decimal quantity,
            decimal? price = null,
            int? leverage = null, 
            MarginType? marginType = null,
            string? clientOrderId = null,
            long? positionId = null, 
            decimal? stopLossPrice = null,
            decimal? takeProfitPrice = null,
            TriggerPriceType? stopLossPriceType = null,
            TriggerPriceType? takeProfitPriceType = null,
            bool? priceProtect = null,
            PositionMode? positionMode = null,
            bool? reduceOnly = null, 
            bool? marketCeiling = null,
            bool? flashClose = null,
            LimitOrderType? limitOrderType = null,
            StpMode? stpMode = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place multiple new orders in a single call
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#batch-place-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/order/submit_batch<br />
        /// </para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CallResult<MexcFuturesBatchOrderResult>[]>> PlaceMultipleOrdersAsync(
            IEnumerable<MexcFuturesOrderRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel orders by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#cancel-orders" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/order/cancel<br />
        /// </para>
        /// </summary>
        /// <param name="orderIds">Ids of orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcCancelResult[]>> CancelOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Chase order, set the price to the best current order book offer
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#chase-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/order/chase_limit_order<br />
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] The order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> ChaseOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Edit an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#chase-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/order/change_limit_order<br />
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Id of the order</param>
        /// <param name="quantity">["<c>vol</c>"] New quantity</param>
        /// <param name="price">["<c>price</c>"] New price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> EditOrderAsync(long orderId,
            decimal quantity,
            decimal price,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel orders by client order ids
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#batch-cancel-by-external-order-id" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/order/batch_cancel_with_external<br />
        /// </para>
        /// </summary>
        /// <param name="orders">["<c>a</c>"] Orders</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcCancelResult[]>> CancelOrdersByClientOrderIdsAsync(IEnumerable<MexcCancelRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#cancel-all-orders-under-a-contract" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/order/cancel_all<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Reverse a position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#cancel-all-orders-under-a-contract" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/position/reverse<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="positionId">["<c>positionId</c>"] Position id</param>
        /// <param name="quantity">["<c>vol</c>"] Quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> ReversePositionAsync(
            string symbol,
            long positionId,
            decimal quantity,
            CancellationToken ct = default);

        /// <summary>
        /// Close all open positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#close-all" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/position/close_all<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CloseAllPositionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get current open order counts
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#query-in-flight-order-counts" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/order/open_order_total_count<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcOrderCount>> GetOpenOrderCountsAsync(CancellationToken ct = default);

        /// <summary>
        /// Place a new plan order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#place-plan-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/planorder/place/v2<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="side">["<c>side</c>"] Side</param>
        /// <param name="orderType">["<c>orderType</c>"] Order type</param>
        /// <param name="quantity">["<c>vol</c>"] Order quantity</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="marginType">["<c>openType</c>"] Margin type</param>
        /// <param name="triggerPrice">["<c>triggerPrice</c>"] Trigger price</param>
        /// <param name="triggerType">["<c>triggerType</c>"] Trigger direction</param>
        /// <param name="executeCycle">["<c>executeCycle</c>"] </param>
        /// <param name="triggerPriceType">["<c>trend</c>"] Trigger price type</param>
        /// <param name="price">["<c>price</c>"] Execution limit price</param>
        /// <param name="priceProtect">["<c>priceProtect</c>"] Price protect</param>
        /// <param name="positionMode">["<c>positionMode</c>"] Position mode</param>
        /// <param name="stopLossPriceType">["<c>lossTrend</c>"] Stop loss price type</param>
        /// <param name="takeProfitPriceType">["<c>profitTrend</c>"] Take profit price type</param>
        /// <param name="stopLossPrice">["<c>stopLossPrice</c>"] Stop loss price</param>
        /// <param name="takeProfitPrice">["<c>takeProfitPrice</c>"] Take profit price</param>
        /// <param name="reduceOnly">["<c>reduceOnly</c>"] Reduce only</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> PlacePlanOrderAsync(
            string symbol,
            FuturesOrderSide side,
            FuturesOrderType orderType,
            decimal quantity,
            MarginType marginType,
            decimal triggerPrice,
            TriggerType triggerType,
            ExecuteCycle executeCycle,
            TriggerPriceType triggerPriceType,
            int? leverage = null,
            decimal? price = null,
            bool? priceProtect = null,
            PositionMode? positionMode = null,
            TriggerPriceType? stopLossPriceType = null,
            TriggerPriceType? takeProfitPriceType = null,
            decimal? stopLossPrice = null,
            decimal? takeProfitPrice = null,
            bool? reduceOnly = null,
            CancellationToken ct = default);

        /// <summary>
        /// Edit an active plan order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#place-plan-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/planorder/change_price<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Order id</param>
        /// <param name="triggerPrice">["<c>triggerPrice</c>"] Trigger price</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="orderType">["<c>orderType</c>"] Order type</param>
        /// <param name="triggerType">["<c>triggerType</c>"] Trigger type</param>
        /// <param name="triggerPriceType">["<c>trend</c>"] Trigger price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> EditPlanOrderAsync(
            string symbol,
            long orderId,
            decimal triggerPrice,
            decimal price,
            FuturesOrderType orderType,
            TriggerType triggerType,
            TriggerPriceType triggerPriceType,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel plan orders by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#cancel-planned-ordersmaintenance" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/planorder/cancel<br />
        /// </para>
        /// </summary>
        /// <param name="orders">Orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelPlanOrdersAsync(IEnumerable<MexcCancelRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel all plan orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#cancel-all-planned-ordersmaintenance" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/planorder/cancel_all<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllPlannedOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#place-tpsl-order-by-position" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/stoporder/place<br />
        /// </para>
        /// </summary>
        /// <param name="positionId">["<c>positionId</c>"] Position id</param>
        /// <param name="quantity">["<c>vol</c>"] Quantity</param>
        /// <param name="stopLossPriceType">["<c>lossTrend</c>"] Stop loss trigger price type</param>
        /// <param name="takeProfitPriceType">["<c>profitTrend</c>"] Take profit trigger price type</param>
        /// <param name="stopLossPrice">["<c>stopLossPrice</c>"] Stop loss price</param>
        /// <param name="takeProfitPrice">["<c>takeProfitPrice</c>"] Take profit price</param>
        /// <param name="profitLossVolumeType">["<c>profitLossVolType</c>"] Profit loss volume type</param>
        /// <param name="takeProfitVolume">["<c>takeProfitVol</c>"] Take profit quantity</param>
        /// <param name="stopLossVolume">["<c>stopLossVol</c>"] Stop loss quantity</param>
        /// <param name="volumeType">["<c>volType</c>"] Volume type</param>
        /// <param name="takeProfitReverse">["<c>takeProfitReverse</c>"] Take profit reverse</param>
        /// <param name="stopLossReverse">["<c>stopLossReverse</c>"] Stop loss reverse</param>
        /// <param name="takeProfitType">["<c>takeProfitType</c>"] Take profit type</param>
        /// <param name="takeProfitOrderPrice">["<c>takeProfitOrderPrice</c>"] Take profit limit order price</param>
        /// <param name="stopLossType">["<c>stopLossType</c>"] Stop loss type</param>
        /// <param name="stopLossOrderPrice">["<c>stopLossOrderPrice</c>"] Stop loss limit order price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> PlaceTpSlOrderAsync(
            long positionId,
            decimal quantity,
            TriggerPriceType stopLossPriceType,
            TriggerPriceType takeProfitPriceType,
            decimal? stopLossPrice = null,
            decimal? takeProfitPrice = null,
            ProfitLossVolumeType? profitLossVolumeType = null,
            decimal? takeProfitVolume = null,
            decimal? stopLossVolume = null,
            VolumeType? volumeType = null,
            bool? takeProfitReverse = null,
            bool? stopLossReverse = null,
            TpSlType? takeProfitType = null,
            decimal? takeProfitOrderPrice = null,
            TpSlType? stopLossType = null,
            decimal? stopLossOrderPrice = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel take profit/stop loss orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#cancel-tpsl-planned-ordersmaintenance" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/stoporder/cancel<br />
        /// </para>
        /// </summary>
        /// <param name="orders">["<c>o</c>"] Orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelTpSlOrdersAsync(IEnumerable<MexcCancelRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel all take profit/stop loss orders matching the params
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#cancel-all-tpsl-planned-ordersmaintenance" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/stoporder/cancel_all<br />
        /// </para>
        /// </summary>
        /// <param name="positionId">["<c>positionId</c>"] Filter by position id</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllTpSlOrdersAsync(
            long? positionId = null,
            string? symbol = null,
            CancellationToken ct = default);

        /// <summary>
        /// Edit an active take profit/stop loss order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#modify-tpsl-prices-on-a-limit-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/stoporder/change_price<br />
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id</param>
        /// <param name="stopLossPriceType">["<c>lossTrend</c>"] Stop loss trigger price type</param>
        /// <param name="takeProfitPriceType">["<c>profitTrend</c>"] Take profit trigger price type</param>
        /// <param name="stopLossPrice">["<c>stopLossPrice</c>"] Stop loss price</param>
        /// <param name="takeProfitPrice">["<c>takeProfitPrice</c>"] Take profit price</param>
        /// <param name="takeProfitReverse">["<c>takeProfitReverse</c>"] Take profit reverse</param>
        /// <param name="stopLossReverse">["<c>stopLossReverse</c>"] Stop loss reverse</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> EditLimitOrderTpSlAsync(
            long orderId,
            TriggerPriceType? stopLossPriceType = null,
            TriggerPriceType? takeProfitPriceType = null,
            decimal? stopLossPrice = null,
            decimal? takeProfitPrice = null,
            bool? takeProfitReverse = null,
            bool? stopLossReverse = null,
            CancellationToken ct = default);

        /// <summary>
        /// 
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#modify-tpsl-prices-on-a-tpsl-planned-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/stoporder/change_plan_price<br />
        /// </para>
        /// </summary>
        /// <param name="stopPlanOrderId">["<c>stopPlanOrderId</c>"] Order id</param>
        /// <param name="stopLossPriceType">["<c>lossTrend</c>"] Stop loss price type</param>
        /// <param name="takeProfitPriceType">["<c>profitTrend</c>"] Take profit price type</param>
        /// <param name="stopLossPrice">["<c>stopLossPrice</c>"] Stop loss price</param>
        /// <param name="takeProfitPrice">["<c>takeProfitPrice</c>"] Take profit price</param>
        /// <param name="takeProfitReverse">["<c>takeProfitReverse</c>"] Take profit reverse</param>
        /// <param name="stopLossReverse">["<c>stopLossReverse</c>"] Stop loss reverse</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> EditTpSlOrderAsync(
            long stopPlanOrderId,
            TriggerPriceType? stopLossPriceType = null,
            TriggerPriceType? takeProfitPriceType = null,
            decimal? stopLossPrice = null,
            decimal? takeProfitPrice = null,
            bool? takeProfitReverse = null,
            bool? stopLossReverse = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get open TP/SL orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#get-current-take-profitstop-loss-order-list" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/stoporder/open_orders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcTpSlOrder[]>> GetOpenTpSlOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new trailing stop order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#place-trailing-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/trackorder/place<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="quantity">["<c>vol</c>"] Quantity</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="marginType">["<c>openType</c>"] Margin type</param>
        /// <param name="triggerType">["<c>trend</c>"] Trigger price type</param>
        /// <param name="callbackType">["<c>backType</c>"] Callback type</param>
        /// <param name="callbackValue">["<c>backValue</c>"] Callback value</param>
        /// <param name="positionMode">["<c>positionMode</c>"] Position mode</param>
        /// <param name="reduceOnly">["<c>reduceOnly</c>"] Reduce only</param>
        /// <param name="activationPrice">["<c>activePrice</c>"] Activation price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> PlaceTrailingOrderAsync(
            string symbol,
            FuturesOrderSide side,
            decimal quantity,
            int leverage,
            MarginType marginType,
            TriggerPriceType triggerType,
            CallbackType callbackType,
            decimal callbackValue,
            PositionMode positionMode,
            bool reduceOnly,
            decimal? activationPrice = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel active trailing order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#cancel-trailing-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/trackorder/cancel<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">["<c>trackOrderId</c>"] Trailing order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelTrailingOrderAsync(
            string symbol,
            long orderId,
            CancellationToken ct = default);

        /// <summary>
        /// Edit an active trailing order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#modify-trailing-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/trackorder/change_order<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">["<c>trackOrderId</c>"] Trailing order id</param>
        /// <param name="triggerType">["<c>trend</c>"] Trigger price type</param>
        /// <param name="callbackType">["<c>backType</c>"] Callback type</param>
        /// <param name="callbackValue">["<c>backValue</c>"] Callback value</param>
        /// <param name="quantity">["<c>vol</c>"] Quantity</param>
        /// <param name="activationPrice">["<c>activePrice</c>"] Activation price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> EditTrailingOrderAsync(
            string symbol,
            long orderId,
            TriggerPriceType triggerType,
            CallbackType callbackType,
            decimal callbackValue,
            decimal quantity,
            decimal? activationPrice = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get trailing orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#query-trailing-orders" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/trackorder/list/orders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="status">["<c>states</c>"] Filter by status</param>
        /// <param name="side">["<c>side</c>"] Filter by side</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="page">["<c>pageIndex</c>"] Page number</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcTrailingOrder[]>> GetTrailingOrdersAsync(
            string? symbol = null,
            IEnumerable<TpSlStatus>? status = null,
            FuturesOrderSide? side = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default);

    }
}
