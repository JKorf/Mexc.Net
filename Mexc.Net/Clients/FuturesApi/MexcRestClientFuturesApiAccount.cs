using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.RateLimiting.Guards;
using Mexc.Net.Objects.Models;

namespace Mexc.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class MexcRestClientFuturesApiAccount : IMexcRestClientFuturesApiAccount
    {
        private readonly MexcRestClientFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal MexcRestClientFuturesApiAccount(MexcRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Balances

        /// <inheritdoc />
        public async Task<HttpResult<MexcFuturesBalance>> GetBalanceAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"api/v1/private/account/asset/{asset}", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesBalance>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public async Task<HttpResult<MexcFuturesBalance[]>> GetBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/private/account/assets", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesBalance[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Transfer History

        /// <inheritdoc />
        public async Task<HttpResult<MexcFuturesTransferPage>> GetTransferHistoryAsync(string? asset = null, TransferStatus? status = null, TransferDirection? direction = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("state", status);
            parameters.Add("type", direction);
            parameters.Add("page_num", page ?? 1);
            parameters.Add("page_size", pageSize ?? 20);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/private/account/transfer_record", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesTransferPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding History

        /// <inheritdoc />
        public async Task<HttpResult<MexcFundingRecordPage>> GetFundingHistoryAsync(string? symbol = null, long? positionId = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("position_id", positionId);
            parameters.Add("page_num", page ?? 1);
            parameters.Add("page_size", pageSize ?? 20);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/private/position/funding_records", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFundingRecordPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trading Fees

        /// <inheritdoc />
        public async Task<HttpResult<MexcFuturesFee>> GetTradingFeesAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/private/account/tiered_fee_rate", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcFuturesFee>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Change Margin

        /// <inheritdoc />
        public async Task<HttpResult> ChangeMarginAsync(long positionId, decimal quantity, ChangeType changeType, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            parameters.Add("positionId", positionId);
            parameters.Add("amount", quantity);
            parameters.Add("type", changeType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v1/private/position/change_margin", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Leverage

        /// <inheritdoc />
        public async Task<HttpResult<MexcLeverage[]>> GetLeverageAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/private/position/leverage", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcLeverage[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<HttpResult> SetLeverageAsync(int leverage, long? positionId = null, MarginType? marginType = null, string? symbol = null, PositionSide? positionSide = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            parameters.Add("leverage", leverage);
            parameters.Add("positionId", positionId);
            parameters.Add("openType", marginType, EnumSerialization.Number);
            parameters.Add("symbol", symbol);
            parameters.Add("positionType", positionSide, EnumSerialization.Number);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v1/private/position/change_leverage", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<PositionMode>> GetPositionModeAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/private/position/position_mode", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<PositionMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Position Mode

        /// <inheritdoc />
        public async Task<HttpResult> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            parameters.Add("positionMode", positionMode, EnumSerialization.Number);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v1/private/position/change_position_mode", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Profit Rate

        /// <inheritdoc />
        public async Task<HttpResult<MexcProfitRate>> GetProfitRateAsync(ProfitPeriod period, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v1/private/account/profit_rate/{(period == ProfitPeriod.Day ? 1 : 2)}", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcProfitRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deduction Config

        /// <inheritdoc />
        public async Task<HttpResult<MexcDeductionConfig[]>> GetDeductionConfigAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/private/account/feeDeductConfigs", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcDeductionConfig[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Discount Types

        /// <inheritdoc />
        public async Task<HttpResult<MexcDiscountTypes>> GetDiscountTypesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/private/account/discountType", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcDiscountTypes>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Zero Fee Symbols

        /// <inheritdoc />
        public async Task<HttpResult<MexcZeroFeeSymbols>> GetZeroFeeSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/private/account/contract/zero_fee_rate", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<MexcZeroFeeSymbols>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Toggle Auto Add Margin

        /// <inheritdoc />
        public async Task<HttpResult> ToggleAutoAddMarginAsync(long positionId,
        bool isEnabled,
        CancellationToken ct = default)
        {
            var parameters = new Parameters(MexcExchange._futuresParameterSerializationSettings);
            parameters.Add("positionId", positionId);
            parameters.Add("isEnabled", isEnabled);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/private/position/change_auto_add_im", MexcExchange.RateLimiter.FuturesRest, 1, true, limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
