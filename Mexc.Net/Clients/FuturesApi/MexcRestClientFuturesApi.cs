using Mexc.Net.Objects.Options;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.SharedApis;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Futures;
using CryptoExchange.Net.Objects.Errors;
using System.Net.Http.Headers;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using Mexc.Net.Clients.MessageHandlers;

namespace Mexc.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal partial class MexcRestClientFuturesApi : RestApiClient, IMexcRestClientFuturesApi
    {
        protected override ErrorMapping ErrorMapping => MexcErrors.FuturesErrors;
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

        public new MexcRestOptions ClientOptions => (MexcRestOptions)base.ClientOptions;

        protected override IRestMessageHandler MessageHandler { get; } = new MexcRestMessageHandler(MexcErrors.FuturesErrors);

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

            StandardRequestHeaders = new Dictionary<string, string>()
            {
                { "source", LibraryHelpers.GetClientReference(() => ClientOptions.BrokerId, Exchange) }
            };
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new MexcFuturesAuthenticationProvider(credentials);

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange.SerializerContext));


        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null)
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight, requestHeaders);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null)
        {
            var result = await base.SendAsync<MexcFuturesResponse<T>>(baseAddress, definition, parameters, cancellationToken, requestHeaders, weight).ConfigureAwait(false);
            if (!result && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
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
            if (!result && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }

            return result.AsDataless();
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => MexcExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

    }
}
