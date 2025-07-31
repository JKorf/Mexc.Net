using Mexc.Net.Enums;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Mexc Futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IMexcRestClientFuturesApiAccount
    {
        /// <summary>
        /// Get balance of an asset
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-user-39-s-single-currency-asset-information" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesBalance>> GetBalanceAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get balances
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-all-informations-of-user-39-s-asset" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesBalance[]>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get transfer history
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-user-39-s-asset-transfer-records" /></para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="direction">Filter by direction</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesTransferPage>> GetTransferHistoryAsync(string? asset = null, TransferStatus? status = null, TransferDirection? direction = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding history
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-details-of-user-s-funding-rate" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="positionId">Position id</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFundingRecordPage>> GetFundingHistoryAsync(string? symbol = null, long? positionId = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get current trading fees
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#gets-the-user-39-s-current-trading-fee-rate" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesFee>> GetTradingFeesAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Change margin
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#increase-or-decrease-margin" /></para>
        /// </summary>
        /// <param name="positionId">Position id</param>
        /// <param name="quantity">Quantity to change</param>
        /// <param name="changeType">Change type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> ChangeMarginAsync(long positionId, decimal quantity, ChangeType changeType, CancellationToken ct = default);

        /// <summary>
        /// Get the current leverage
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-leverage" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcLeverage[]>> GetLeverageAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Set leverage
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#switch-leverage" /></para>
        /// </summary>
        /// <param name="leverage">Leverage</param>
        /// <param name="positionId">Position id, required when there is an open position</param>
        /// <param name="marginType">Margin type, required when there is no open position</param>
        /// <param name="symbol">The symbol, for example `ETH_USDT`, required when there is no open position</param>
        /// <param name="positionSide">Position side, required when there is no open position</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetLeverageAsync(int leverage, long? positionId = null, MarginType? marginType = null, string? symbol = null, PositionSide? positionSide = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current position mode
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-position-mode" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<PositionMode>> GetPositionModeAsync(CancellationToken ct = default);

        /// <summary>
        /// Set position mode
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#change-position-mode" /></para>
        /// </summary>
        /// <param name="positionMode">Position mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default);

    }
}
