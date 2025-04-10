using CryptoExchange.Net.Clients;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

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

                // Url encoded values to upper
                var query = uri.Query.Replace("?", "")
                    .Replace("%5b", "%5B").Replace("%7b", "%7B").Replace("%3a", "%3A").Replace("%2c", "%2C").Replace("%7d", "%7D").Replace("%5d", "%5D");

                parameters.Add("signature", SignHMACSHA256(parameterPosition == HttpMethodParameterPosition.InUri ? query : parameters.ToFormData()));
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
