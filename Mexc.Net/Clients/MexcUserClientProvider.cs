using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Collections.Generic;

namespace Mexc.Net.Clients
{
    /// <inheritdoc />
    public class MexcUserClientProvider : IMexcUserClientProvider
    {
        private static ConcurrentDictionary<string, IMexcRestClient> _restClients = new ConcurrentDictionary<string, IMexcRestClient>();
        private static ConcurrentDictionary<string, IMexcSocketClient> _socketClients = new ConcurrentDictionary<string, IMexcSocketClient>();

        private readonly IOptions<MexcRestOptions> _restOptions;
        private readonly IOptions<MexcSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

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
        {
            _httpClient = httpClient ?? new HttpClient();
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, ApiCredentials credentials, MexcEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public void ClearUserClients(string userIdentifier)
        {
            _restClients.TryRemove(userIdentifier, out _);
            _socketClients.TryRemove(userIdentifier, out _);
        }

        /// <inheritdoc />
        public IMexcRestClient GetRestClient(string userIdentifier, ApiCredentials? credentials = null, MexcEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client))
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public IMexcSocketClient GetSocketClient(string userIdentifier, ApiCredentials? credentials = null, MexcEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client))
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private IMexcRestClient CreateRestClient(string userIdentifier, ApiCredentials? credentials, MexcEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new MexcRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IMexcSocketClient CreateSocketClient(string userIdentifier, ApiCredentials? credentials, MexcEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new MexcSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<MexcRestOptions> SetRestEnvironment(MexcEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new MexcRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<MexcSocketOptions> SetSocketEnvironment(MexcEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new MexcSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}
