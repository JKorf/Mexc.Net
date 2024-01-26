using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net;
using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Mexc.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Mexc.Net.Interfaces.Clients.SpotApi;

namespace Mexc.Net.Clients.SpotApi
{
    public class MexcRestClientSpotApi : RestApiClient, IMexcRestClientSpotApi//, ISpotClient
    {
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");

        public IMexcRestClientSpotApiAccount Account { get; }
        public IMexcRestClientSpotApiExchangeData ExchangeData { get; }
        public IMexcRestClientSpotApiTrading Trading { get; }

        /// <inheritdoc />
        public string ExchangeName => "Mexc";

        #region constructor/destructor
        internal MexcRestClientSpotApi(ILogger logger, HttpClient? httpClient, MexcRestOptions options)
            : base(logger, httpClient, options.Environment.SpotRestAddress, options, options.SpotOptions)
        {
            Account = new MexcRestClientSpotApiAccount(logger, this);
            ExchangeData = new MexcRestClientSpotApiExchangeData(logger, this);
            Trading = new MexcRestClientSpotApiTrading(logger, this);

            DefaultSerializer = JsonSerializer.Create(SerializerOptions.WithConverters);

            requestBodyEmptyContent = "";
            requestBodyFormat = RequestBodyFormat.FormData;
            arraySerialization = ArrayParametersSerialization.MultipleValues;
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new MexcAuthenticationProvider(credentials);

        internal async Task<WebCallResult<T>> SendRequestInternal<T>(string path, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false) where T : class
        {
            var result = await SendRequestAsync<T>(new Uri(BaseAddress.AppendPath(path)), method, cancellationToken, parameters, signed, null, postPosition, arraySerialization, weight, ignoreRatelimit: ignoreRateLimit).ConfigureAwait(false);
            if (!result && result.Error!.Code == 700003 && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                _timeSyncState.LastSyncTime = DateTime.MinValue;
            }
            return result;
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
