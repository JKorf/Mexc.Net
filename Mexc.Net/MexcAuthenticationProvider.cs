using CryptoExchange.Net.Clients;
using System.Text;

namespace Mexc.Net
{
    internal class MexcAuthenticationProvider : AuthenticationProvider
    {
        public MexcAuthenticationProvider(ApiCredentials credentials) : base(credentials)
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
            headers ??= new Dictionary<string, string>();
            headers.Add("X-MEXC-APIKEY", _credentials.Key);

            if (!auth)
                return;

            IDictionary<string, object> parameters;
            if (parameterPosition == HttpMethodParameterPosition.InUri)
            {
                uriParameters ??= new Dictionary<string, object>();
                parameters = uriParameters;
            }
            else
            {
                bodyParameters ??= new Dictionary<string, object>();
                parameters = bodyParameters;
            }
            var timestamp = GetMillisecondTimestamp(apiClient);
            parameters.Add("timestamp", timestamp);

            if (_credentials.CredentialType == ApiCredentialsType.Hmac)
            {
                if (uriParameters != null)
                    uri = uri.SetParameters(uriParameters, arraySerialization);
                parameters.Add("signature", SignHMACSHA256(parameterPosition == HttpMethodParameterPosition.InUri ? uri.Query.Replace("?", "") : parameters.ToFormData()));
            }
            else
            {
                var parameterString = parameters.ToFormData();
                var sign = SignRSASHA256(Encoding.ASCII.GetBytes(parameterString), SignOutputType.Base64);
                parameters.Add("signature", sign);
            }
        }
    }
}
