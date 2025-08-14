using CryptoExchange.Net.Clients;
using Mexc.Net.Clients.FuturesApi;
using Mexc.Net.Clients.SpotApi;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Mexc.Net
{
    internal class MexcFuturesAuthenticationProvider : AuthenticationProvider
    {
        private static readonly IStringMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(MexcExchange.SerializerContext));

        public MexcFuturesAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            ref IDictionary<string, object>? uriParameters,
            ref IDictionary<string, object>? bodyParameters,
            ref Dictionary<string, string>? headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat requestBodyFormat)
        {
            if (!auth)
                return;

            headers ??= new Dictionary<string, string>();

            string paramStr;
            if (parameterPosition == HttpMethodParameterPosition.InUri)
            {
                uriParameters ??= new Dictionary<string, object>();
                paramStr = uriParameters.CreateParamString(true, arraySerialization);
            }
            else
            {
                bodyParameters ??= new Dictionary<string, object>();
                paramStr = GetSerializedBody(_serializer, bodyParameters);
            }

            var timestamp = GetMillisecondTimestamp(apiClient);
            var signStr = $"{ApiKey}{timestamp}{paramStr}";
            var sign = SignHMACSHA256(signStr);

            headers["ApiKey"] = ApiKey;
            headers["Request-Time"] = timestamp;
            headers["Signature"] = sign.ToLowerInvariant();
            headers["Recv-Window"] = ((MexcRestClientFuturesApi)apiClient).ClientOptions.ReceiveWindow.TotalMilliseconds.ToString();
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
