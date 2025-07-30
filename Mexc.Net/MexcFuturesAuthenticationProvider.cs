using CryptoExchange.Net.Clients;
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
            headers.Add("X-MEXC-APIKEY", _credentials.Key);

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

            headers ??= new Dictionary<string, string>();
            headers["ApiKey"] = ApiKey;
            headers["Request-Time"] = timestamp;
            headers["Signature"] = sign.ToLowerInvariant();            
        }
    }
}
