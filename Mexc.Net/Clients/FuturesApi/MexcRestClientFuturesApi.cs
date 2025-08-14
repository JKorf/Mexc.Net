using Mexc.Net.Objects.Options;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Enums;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.SharedApis;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Futures;
using CryptoExchange.Net.Objects.Errors;

namespace Mexc.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal partial class MexcRestClientFuturesApi : RestApiClient, IMexcRestClientFuturesApi
    {
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Futures Api");

        protected override ErrorCollection ErrorMapping { get; } = new ErrorCollection([
            new ErrorInfo(ErrorType.Unauthorized, true, "Unauthorized", "401"),
            new ErrorInfo(ErrorType.Unauthorized, true, "API key expired", "402"),
            new ErrorInfo(ErrorType.Unauthorized, true, "IP address not allowed", "406"),
            new ErrorInfo(ErrorType.Unauthorized, true, "Insufficient permissions", "701", "702", "703", "704"),
            new ErrorInfo(ErrorType.Unauthorized, true, "Trading forbidden", "6001"),

            new ErrorInfo(ErrorType.SystemError, true, "System error", "500"),
            new ErrorInfo(ErrorType.SystemError, true, "System busy", "501"),

            new ErrorInfo(ErrorType.RequestRateLimited, true, "Too many requests", "510", "2037"),

            new ErrorInfo(ErrorType.InvalidParameter, true, "Parameter error", "600"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Invalid order direction", "2001"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Invalid open type", "2002"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Leverage ratio error", "2006"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Too many orders in batch operation", "2013", "2014", "2034"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Price or quantity decimal precision invalid", "2015"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Incorrect position type", "2022"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Order type invalid", "2029"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Invalid clientOrderId", "2030"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Trigger price type error", "3001"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Trigger type error", "3002"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Time range invalid", "6003"),

            new ErrorInfo(ErrorType.StopParametersInvalid, true, "Trigger price error", "3004"),
            new ErrorInfo(ErrorType.StopParametersInvalid, true, "Take profit and stop loss price can't both be empty", "5001"),
            new ErrorInfo(ErrorType.StopParametersInvalid, true, "Take profit and stop loss price invalid", "5003"),
            new ErrorInfo(ErrorType.StopParametersInvalid, true, "Take profit and stop loss quantity invalid", "5004"),

            new ErrorInfo(ErrorType.UnknownOrder, true, "Stop limit order not found", "5002"),

            new ErrorInfo(ErrorType.UnknownSymbol, true, "Symbol does not exist", "1001"),

            new ErrorInfo(ErrorType.UnknownAsset, true, "Unsupported asset", "4001"),

            new ErrorInfo(ErrorType.SymbolNotTrading, true, "Symbol not currently trading", "1002"),
            new ErrorInfo(ErrorType.SymbolNotTrading, true, "Symbol not available", "6005"),

            new ErrorInfo(ErrorType.QuantityInvalid, true, "Quantity invalid", "1004", "2011"),
            new ErrorInfo(ErrorType.QuantityInvalid, true, "Order quantity too low", "2008"),
            new ErrorInfo(ErrorType.QuantityInvalid, true, "Order quantity too high", "2028"),

            new ErrorInfo(ErrorType.PriceInvalid, true, "Price too low", "2004"),
            new ErrorInfo(ErrorType.PriceInvalid, true, "Price error", "2007"),
            new ErrorInfo(ErrorType.PriceInvalid, true, "Price lower than liquidation price", "2032"),
            new ErrorInfo(ErrorType.PriceInvalid, true, "Price higher than liquidation price", "2033"),

            new ErrorInfo(ErrorType.NoPosition, true, "No open position", "2009"),

            new ErrorInfo(ErrorType.BalanceInsufficient, true, "Insufficient balance", "2005"),
            new ErrorInfo(ErrorType.BalanceInsufficient, true, "Maximum available margin exceeded", "2018"),

            new ErrorInfo(ErrorType.OrderRateLimited, true, "Too many open orders", "2036"),

            ]);
        /// <inheritdoc />
        public IMexcRestClientFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IMexcRestClientFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IMexcRestClientFuturesApiTrading Trading { get; }

        /// <inheritdoc />
        public IMexcRestClientFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public string ExchangeName => "Mexc";

        internal readonly string _brokerId;

        public new MexcRestOptions ClientOptions => (MexcRestOptions)base.ClientOptions;

        #region constructor/destructor
        internal MexcRestClientFuturesApi(ILogger logger, HttpClient? httpClient, MexcRestOptions options)
            : base(logger, httpClient, options.Environment.FuturesRestAddress, options, options.FuturesOptions)
        {
            Account = new MexcRestClientFuturesApiAccount(this);
            ExchangeData = new MexcRestClientFuturesApiExchangeData(this);
            Trading = new MexcRestClientFuturesApiTrading(this);

            RequestBodyEmptyContent = "";
            ArraySerialization = ArrayParametersSerialization.MultipleValues;

            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;

            _brokerId = !string.IsNullOrEmpty(options.BrokerId) ? options.BrokerId! : "EASYT";
            StandardRequestHeaders = new Dictionary<string, string>()
            {
                { "source", _brokerId }
            };
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new MexcFuturesAuthenticationProvider(credentials);

        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(MexcExchange.SerializerContext));

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange.SerializerContext));


        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null)
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight, requestHeaders);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null)
        {
            var result = await base.SendAsync<MexcFuturesResponse<T>>(baseAddress, definition, parameters, cancellationToken, requestHeaders, weight).ConfigureAwait(false);
            if (!result && result.Error!.ErrorType == ErrorType.TimestampInvalid && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                _timeSyncState.LastSyncTime = DateTime.MinValue;
            }

            if (!result)
                return result.As<T>(default);

            return result.As(result.Data.Data);
        }

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight, requestHeaders);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null)
        {
            var result = await base.SendAsync<MexcFuturesResponse>(baseAddress, definition, parameters, cancellationToken, requestHeaders, weight).ConfigureAwait(false);
            if (!result && result.Error!.ErrorType == ErrorType.TimestampInvalid && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                _timeSyncState.LastSyncTime = DateTime.MinValue;
            }

            return result.AsDataless();
        }

        /// <inheritdoc />
        protected override Error? TryParseError(KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor)
        {
            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            if (code == 0 || code == 200 || code == null)
                return null;

            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            return new ServerError(code.Value, GetErrorInfo(code.Value, msg!));
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => MexcExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        protected override ServerRateLimitError ParseRateLimitResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor)
        {
            var error = GetRateLimitError(accessor);
            var retryAfterHeader = responseHeaders.SingleOrDefault(r => r.Key.Equals("Retry-After", StringComparison.InvariantCultureIgnoreCase));
            if (retryAfterHeader.Value?.Any() != true)
                return error;

            var value = retryAfterHeader.Value.First();
            if (!int.TryParse(value, out var seconds))
                return error;

            if (seconds == 0)
            {
                var now = DateTime.UtcNow;
                seconds = (int)(new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Utc).AddMinutes(1) - now).TotalSeconds + 1;
            }

            error.RetryAfter = DateTime.UtcNow.AddSeconds(seconds);
            return error;
        }

        private MexcRateLimitError GetRateLimitError(IMessageAccessor accessor)
        {
            if (!accessor.IsValid)
                return new MexcRateLimitError(accessor.GetOriginalString());

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("msg"));
            if (msg == null)
                return new MexcRateLimitError(accessor.GetOriginalString());

            if (code == null)
                return new MexcRateLimitError(msg);

            return new MexcRateLimitError(code.Value, msg);
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("msg"));
            if (msg == null)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            if (code == null)
                return new ServerError(ErrorInfo.Unknown with { Message = msg }, exception);

            return new ServerError(code.Value, GetErrorInfo(code.Value, msg), exception);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp), (ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval), _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;
    }
}
