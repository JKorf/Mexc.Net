using CryptoExchange.Net.Clients;
using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Objects.Options;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Mexc.Net.Clients
{
    /// <inheritdoc />
    public class MexcUserClientProvider : UserClientProvider<
        IMexcRestClient,
        IMexcSocketClient, 
        MexcRestOptions,
        MexcSocketOptions,
        MexcCredentials,
        MexcEnvironment
        >, IMexcUserClientProvider
    {
        /// <inheritdoc />
        public override string ExchangeName => MexcExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public MexcUserClientProvider(Action<MexcOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public MexcUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<MexcRestOptions> restOptions,
            IOptions<MexcSocketOptions> socketOptions)
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IMexcRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<MexcRestOptions> options)
            => new MexcRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IMexcSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<MexcSocketOptions> options)
            => new MexcSocketClient(options, loggerFactory);
    }
}
