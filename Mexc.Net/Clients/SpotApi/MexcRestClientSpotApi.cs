using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Mexc.Net.Clients.MessageHandlers;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Options;
using System.Net.Http.Headers;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal partial class MexcRestClientSpotApi : RestApiClient, IMexcRestClientSpotApi
    {
        protected override IRestMessageHandler MessageHandler { get; } = new MexcRestMessageHandler(MexcErrors.SpotErrors);
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

            StandardRequestHeaders = new Dictionary<string, string>()
            {
                { "source", LibraryHelpers.GetClientReference(() => ClientOptions.BrokerId, Exchange) }
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
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }

            return result;
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => MexcExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

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
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();
    }
}
