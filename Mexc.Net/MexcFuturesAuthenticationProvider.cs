using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Mexc.Net.Clients.FuturesApi;
using Mexc.Net.Objects.Sockets.Queries;

namespace Mexc.Net
{
    internal class MexcFuturesAuthenticationProvider : AuthenticationProvider
    {
        private static readonly IStringMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange.SerializerContext));

        public override ApiCredentialsType[] SupportedCredentialTypes => [ApiCredentialsType.Hmac];

        public MexcFuturesAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);
            var queryString = request.GetQueryString(true);
            var body = request.ParameterPosition == HttpMethodParameterPosition.InBody ? GetSerializedBody(_serializer, request.BodyParameters ?? new Dictionary<string, object>()) : string.Empty;

            var signStr = $"{ApiKey}{timestamp}{queryString}{body}";
            var signature = SignHMACSHA256(signStr);

            request.Headers ??= new Dictionary<string, string>();
            request.Headers["ApiKey"] = ApiKey;
            request.Headers["Request-Time"] = timestamp;
            request.Headers["Signature"] = signature.ToLowerInvariant();
            request.Headers["Recv-Window"] = ((MexcRestClientFuturesApi)apiClient).ClientOptions.ReceiveWindow.TotalMilliseconds.ToString();

            request.SetBodyContent(body);
            request.SetQueryString(queryString);
        }

        public override Query? GetAuthenticationQuery(SocketApiClient apiClient, SocketConnection connection, Dictionary<string, object?>? context = null)
        {
            var timestamp = GetMillisecondTimestamp(apiClient);
            var sign = SignHMACSHA256(ApiKey + timestamp).ToLowerInvariant();
            var parameters = new Dictionary<string, object>
            {
                { "apiKey", ApiKey },
                { "reqTime", timestamp },
                { "signature", sign },
                { "subscribe", false }
            };
            return new MexcFuturesQuery("login", parameters, false);
        }
    }
}
