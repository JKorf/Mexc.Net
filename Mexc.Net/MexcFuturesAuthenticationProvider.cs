using CryptoExchange.Net.Clients;
using Mexc.Net.Clients.FuturesApi;

namespace Mexc.Net
{
    internal class MexcFuturesAuthenticationProvider : AuthenticationProvider
    {
        private static readonly IStringMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange.SerializerContext));

        public MexcFuturesAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);
            var queryString = request.GetQueryString(true);
            var body = request.ParameterPosition == HttpMethodParameterPosition.InBody ? GetSerializedBody(_serializer, request.BodyParameters) : string.Empty;

            var signStr = $"{ApiKey}{timestamp}{queryString}{body}";
            var signature = SignHMACSHA256(signStr);

            request.Headers["ApiKey"] = ApiKey;
            request.Headers["Request-Time"] = timestamp;
            request.Headers["Signature"] = signature.ToLowerInvariant();
            request.Headers["Recv-Window"] = ((MexcRestClientFuturesApi)apiClient).ClientOptions.ReceiveWindow.TotalMilliseconds.ToString();

            request.SetBodyContent(body);
            request.SetQueryString(queryString);
        }

        public Dictionary<string, object> GetSocketAuthParameters()
        {
            var timestamp = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).Value.ToString(CultureInfo.InvariantCulture);
            var sign = SignHMACSHA256(ApiKey + timestamp).ToLowerInvariant();
            return new Dictionary<string, object>
            {
                { "apiKey", ApiKey },
                { "reqTime", timestamp },
                { "signature", sign },
                { "subscribe", false }
            };
        }
    }
}
