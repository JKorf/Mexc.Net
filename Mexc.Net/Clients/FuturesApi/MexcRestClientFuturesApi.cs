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
    internal partial class MexcRestClientFuturesApi : RestApiClient<MexcEnvironment, MexcFuturesAuthenticationProvider, MexcCredentials>, IMexcRestClientFuturesApi
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

        public new MexcRestOptions ClientOptions => (MexcRestOptions)base.ClientOptions;

        protected override IRestMessageHandler MessageHandler { get; } = new MexcRestMessageHandler(MexcErrors.FuturesErrors);

        #region constructor/destructor
        internal MexcRestClientFuturesApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, MexcRestOptions options)
            : base(loggerFactory, MexcExchange.Metadata.Id, httpClient, options.Environment.FuturesRestAddress, options, options.FuturesOptions)
        {
            Account = new MexcRestClientFuturesApiAccount(this);
            ExchangeData = new MexcRestClientFuturesApiExchangeData(this);
            Trading = new MexcRestClientFuturesApiTrading(this);

            RequestBodyEmptyContent = "";

            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;

            StandardRequestHeaders = new Dictionary<string, string>()
            {
                { "source", LibraryHelpers.GetClientReference(() => ClientOptions.BrokerId, Exchange) }
            };
        }
        #endregion

        /// <inheritdoc />
        protected override MexcFuturesAuthenticationProvider CreateAuthenticationProvider(MexcCredentials credentials)
            => new MexcFuturesAuthenticationProvider(credentials);

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange._serializerContext));

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null)
        {
            var result = await base.SendAsync<MexcFuturesResponse<T>>(definition, parameters, cancellationToken, requestHeaders, weight).ConfigureAwait(false);
            if (!result.Success && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }

            if (!result.Success)
                return HttpResult.Fail<T>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null)
        {
            var result = await base.SendAsync<MexcFuturesResponse>(definition, parameters, cancellationToken, requestHeaders, weight).ConfigureAwait(false);
            if (!result.Success && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }

            return result;
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => MexcExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

    }
}
