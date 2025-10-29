using Mexc.Net.Objects.Options;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Enums;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.SharedApis;
using Mexc.Net.Objects.Models;
using CryptoExchange.Net.Objects.Errors;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal partial class MexcRestClientSpotApi : RestApiClient, IMexcRestClientSpotApi
    {
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");

        protected override ErrorMapping ErrorMapping => MexcErrors.SpotErrors;

        /// <inheritdoc />
        public IMexcRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IMexcRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IMexcRestClientSpotApiTrading Trading { get; }
        /// <inheritdoc />
        public IMexcRestClientSpotApiSubAccount SubAccount { get; }

        /// <inheritdoc />
        public IMexcRestClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        public string ExchangeName => "Mexc";

        internal readonly string _brokerId;

        public new MexcRestOptions ClientOptions => (MexcRestOptions)base.ClientOptions;

        #region constructor/destructor
        internal MexcRestClientSpotApi(ILogger logger, HttpClient? httpClient, MexcRestOptions options)
            : base(logger, httpClient, options.Environment.SpotRestAddress, options, options.SpotOptions)
        {
            Account = new MexcRestClientSpotApiAccount(this);
            ExchangeData = new MexcRestClientSpotApiExchangeData(this);
            Trading = new MexcRestClientSpotApiTrading(this);
            SubAccount = new MexcRestClientSpotApiSubAccount(this);

            RequestBodyEmptyContent = "";
            RequestBodyFormat = RequestBodyFormat.FormData;
            ArraySerialization = ArrayParametersSerialization.MultipleValues;

            ParameterPositions[HttpMethod.Post] = HttpMethodParameterPosition.InUri;
            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;
            ParameterPositions[HttpMethod.Put] = HttpMethodParameterPosition.InUri;

            _brokerId = !string.IsNullOrEmpty(options.BrokerId) ? options.BrokerId! : "EASYT";
            StandardRequestHeaders = new Dictionary<string, string>()
            {
                { "source", _brokerId }
            };
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new MexcAuthenticationProvider(credentials);

        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(MexcExchange.SerializerContext));

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange.SerializerContext));


        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight, requestHeaders);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, requestHeaders, weight).ConfigureAwait(false);
            if (!result && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                _timeSyncState.LastSyncTime = DateTime.MinValue;
            }

            return result;
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
