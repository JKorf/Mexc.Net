using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class MexcSocketClientSpotApi : SocketApiClient, IMexcSocketClientSpotApi
    {
        #region constructor/destructor

        internal MexcSocketClientSpotApi(ILogger logger, MexcSocketOptions options) :
            base(logger, options.Environment.SpotSocketAddress, options, options.SpotOptions)
        {
        }

        protected override Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection socketConnection)
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new MexcAuthenticationProvider(credentials);

        protected override bool HandleQueryResponse<T>(SocketConnection socketConnection, object request, JToken data, out CallResult<T>? callResult)
        {
            throw new NotImplementedException();
        }

        protected override bool HandleSubscriptionResponse(SocketConnection socketConnection, SocketSubscription subscription, object request, JToken data, out CallResult<object>? callResult)
        {
            throw new NotImplementedException();
        }

        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, object request)
        {
            throw new NotImplementedException();
        }

        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, string identifier)
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription subscriptionToUnsub)
        {
            throw new NotImplementedException();
        }
    }
}
