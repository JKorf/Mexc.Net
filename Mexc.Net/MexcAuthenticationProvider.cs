using CryptoExchange.Net.Clients;
using Mexc.Net.Clients.SpotApi;
using System.Text;

namespace Mexc.Net
{
    internal class MexcAuthenticationProvider : AuthenticationProvider<MexcCredentials>
    {
        public override ApiCredentialsType[] SupportedCredentialTypes => [ApiCredentialsType.Hmac, ApiCredentialsType.Rsa];
        public override string Key => ApiCredentials.Key;

        public MexcAuthenticationProvider(MexcCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            request.Headers ??= new Dictionary<string, string>();
            request.Headers.Add("X-MEXC-APIKEY", ApiCredentials.Key);

            var parameters = request.GetPositionParameters();
            var timestamp = GetMillisecondTimestamp(apiClient);
            parameters.Add("recvWindow", ((MexcRestClientSpotApi)apiClient).ClientOptions.ReceiveWindow.TotalMilliseconds.ToString());
            parameters.Add("timestamp", timestamp);

            if (ApiCredentials.CredentialType == ApiCredentialsType.Hmac)
            {
                if (request.ParameterPosition == HttpMethodParameterPosition.InUri)
                {
                    var queryString = request.GetQueryString(true);
                    queryString = queryString
                        .Replace("%5b", "%5B")
                        .Replace("%7b", "%7B")
                        .Replace("%3a", "%3A")
                        .Replace("%2c", "%2C")
                        .Replace("%7d", "%7D")
                        .Replace("%5d", "%5D");

                    var signature = SignHMACSHA256(ApiCredentials.Hmac!, queryString);
                    request.QueryParameters ??= new Dictionary<string, object>();
                    request.QueryParameters.Add("signature", signature);
                    request.SetQueryString($"{queryString}&signature={signature}");
                }
                else
                {
                    var body = parameters.ToFormData();
                    var signature = SignHMACSHA256(ApiCredentials.Hmac!, body);
                    request.BodyParameters ??= new Dictionary<string, object>();
                    request.BodyParameters.Add("signature", signature);
                    request.SetBodyContent($"{body}&signature={signature}");
                }
            }
            else
            {
                var parameterString = parameters.ToFormData();
                var sign = SignRSASHA256(ApiCredentials.Rsa!, Encoding.ASCII.GetBytes(parameterString), SignOutputType.Base64);
                var signed = $"{parameterString}&signature={sign}";

                if (request.ParameterPosition == HttpMethodParameterPosition.InUri)
                    request.SetQueryString(signed);
                else
                    request.SetBodyContent(signed);
            }
        }
    }
}
