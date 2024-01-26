using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Mexc.Net
{
    internal class MexcAuthenticationProvider : AuthenticationProvider
    {
        public string GetApiKey() => _credentials.Key!.GetString();

        public MexcAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
        {
            uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            headers = new Dictionary<string, string>() { { "X-MEXC-APIKEY", _credentials.Key!.GetString() } };

            if (!auth)
                return;

            var parameters = parameterPosition == HttpMethodParameterPosition.InUri ? uriParameters : bodyParameters;
            var timestamp = GetMillisecondTimestamp(apiClient);
            parameters.Add("timestamp", timestamp);

            if (_credentials.CredentialType == ApiCredentialsType.Hmac)
            {
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
