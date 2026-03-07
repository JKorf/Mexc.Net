using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Mexc Futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IMexcRestClientFuturesApiAccount
    {
        /// <summary>
        /// Get balance of an asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-user-39-s-single-currency-asset-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/account/assets/{asset}
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Asset name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesBalance>> GetBalanceAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-all-informations-of-user-39-s-asset" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/account/assets
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesBalance[]>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get transfer history
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-user-39-s-asset-transfer-records" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/account/transfer_record
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="status">["<c>state</c>"] Filter by status</param>
        /// <param name="direction">["<c>type</c>"] Filter by direction</param>
        /// <param name="page">["<c>page_num</c>"] Page number</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesTransferPage>> GetTransferHistoryAsync(string? asset = null, TransferStatus? status = null, TransferDirection? direction = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding history
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-details-of-user-s-funding-rate" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/position/funding_records
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="positionId">["<c>position_id</c>"] Position id</param>
        /// <param name="page">["<c>page_num</c>"] Page number</param>
        /// <param name="pageSize">["<c>page_size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFundingRecordPage>> GetFundingHistoryAsync(string? symbol = null, long? positionId = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get current trading fees
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#gets-the-user-39-s-current-trading-fee-rate" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/account/tiered_fee_rate
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesFee>> GetTradingFeesAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Change margin
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#increase-or-decrease-margin" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/position/change_margin
        /// </para>
        /// </summary>
        /// <param name="positionId">["<c>positionId</c>"] Position id</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to change</param>
        /// <param name="changeType">["<c>type</c>"] Change type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> ChangeMarginAsync(long positionId, decimal quantity, ChangeType changeType, CancellationToken ct = default);

        /// <summary>
        /// Get the current leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-leverage" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/position/leverage
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcLeverage[]>> GetLeverageAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Set leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#switch-leverage" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/position/change_leverage
        /// </para>
        /// </summary>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="positionId">["<c>positionId</c>"] Position id, required when there is an open position</param>
        /// <param name="marginType">["<c>openType</c>"] Margin type, required when there is no open position</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`, required when there is no open position</param>
        /// <param name="positionSide">["<c>positionType</c>"] Position side, required when there is no open position</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetLeverageAsync(int leverage, long? positionId = null, MarginType? marginType = null, string? symbol = null, PositionSide? positionSide = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current position mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-position-mode" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/position/position_mode
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<PositionMode>> GetPositionModeAsync(CancellationToken ct = default);

        /// <summary>
        /// Set position mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#change-position-mode" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/position/change_position_mode
        /// </para>
        /// </summary>
        /// <param name="positionMode">["<c>positionMode</c>"] Position mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default);

    }
}
