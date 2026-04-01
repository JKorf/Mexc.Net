using Mexc.Net.Enums;
using Mexc.Net.Objects.Models;
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

        /// <summary>
        /// Get profit rate
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#view-personal-profit-rate" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/account/profit_rate/{type}<br />
        /// </para>
        /// </summary>
        /// <param name="period">["<c>type</c>"] Profit period</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcProfitRate>> GetProfitRateAsync(ProfitPeriod period, CancellationToken ct = default);

        /// <summary>
        /// Get deduction configuration
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#deduction-configuration" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/account/feeDeductConfigs<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcDeductionConfig[]>> GetDeductionConfigAsync(CancellationToken ct = default);

        /// <summary>
        /// Get discount type config
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#query-user-discount-usage" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/account/discountType<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcDiscountTypes>> GetDiscountTypesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get zero fee trading pairs
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#zero-fee-trading-pairs" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/private/account/contract/zero_fee_rate<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcZeroFeeSymbols>> GetZeroFeeSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Toggle auto-add margin for a position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.mexc.com/api-docs/futures/account-and-trading-endpoints#enable-or-disable-auto-add-margin" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/private/position/change_auto_add_im<br />
        /// </para>
        /// </summary>
        /// <param name="positionId">["<c>positionId</c>"] Position id</param>
        /// <param name="isEnabled">["<c>isEnabled</c>"] Whether auto-add margin is enabled</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> ToggleAutoAddMarginAsync(long positionId,
            bool isEnabled,
            CancellationToken ct = default);

    }
}
