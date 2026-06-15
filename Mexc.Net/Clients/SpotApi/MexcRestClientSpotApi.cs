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
    internal partial class MexcRestClientSpotApi : RestApiClient<MexcEnvironment, MexcAuthenticationProvider, MexcCredentials>, IMexcRestClientSpotApi
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
        internal MexcRestClientSpotApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, MexcRestOptions options)
            : base(loggerFactory, MexcExchange.Metadata.Id, httpClient, options.Environment.SpotRestAddress, options, options.SpotOptions)
        {
            Account = new MexcRestClientSpotApiAccount(this);
            ExchangeData = new MexcRestClientSpotApiExchangeData(this);
            Trading = new MexcRestClientSpotApiTrading(this);
            SubAccount = new MexcRestClientSpotApiSubAccount(this);

            RequestBodyEmptyContent = "";
            RequestBodyFormat = RequestBodyFormat.FormData;

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
        protected override MexcAuthenticationProvider CreateAuthenticationProvider(MexcCredentials credentials)
            => new MexcAuthenticationProvider(credentials);

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange._serializerContext));

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null) where T : class
        {
            var result = await base.SendAsync<T>(definition, parameters, cancellationToken, requestHeaders, weight).ConfigureAwait(false);
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
