using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using Mexc.Net;
using Mexc.Net.Clients;
using Mexc.Net.Interfaces;
using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Objects.Options;
using Mexc.Net.SymbolOrderBooks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add services such as the IMexcRestClient and IMexcSocketClient. Configures the services based on the provided configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddMexc(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new MexcOptions();
            // Reset environment so we know if theyre overriden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            configuration.Bind(options);

            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            var restEnvName = options.Rest.Environment?.Name ?? options.Environment?.Name ?? MexcEnvironment.Live.Name;
            var socketEnvName = options.Socket.Environment?.Name ?? options.Environment?.Name ?? MexcEnvironment.Live.Name;
            options.Rest.Environment = MexcEnvironment.GetEnvironmentByName(restEnvName) ?? options.Rest.Environment!;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = MexcEnvironment.GetEnvironmentByName(socketEnvName) ?? options.Socket.Environment!;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;


            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

            return AddMexcCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IMexcRestClient and IMexcSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the Mexc services</param>
        /// <returns></returns>
        public static IServiceCollection AddMexc(
            this IServiceCollection services,
            Action<MexcOptions>? optionsDelegate = null)
        {
            var options = new MexcOptions();
            // Reset environment so we know if theyre overriden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            optionsDelegate?.Invoke(options);
            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            options.Rest.Environment = options.Rest.Environment ?? options.Environment ?? MexcEnvironment.Live;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = options.Socket.Environment ?? options.Environment ?? MexcEnvironment.Live;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;

            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

            return AddMexcCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// DEPRECATED; use <see cref="AddMexc(IServiceCollection, Action{MexcOptions}?)" /> instead
        /// </summary>
        public static IServiceCollection AddMexc(
            this IServiceCollection services,
            Action<MexcRestOptions> restDelegate,
            Action<MexcSocketOptions>? socketDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.Configure<MexcRestOptions>((x) => { restDelegate?.Invoke(x); });
            services.Configure<MexcSocketOptions>((x) => { socketDelegate?.Invoke(x); });

            return AddMexcCore(services, socketClientLifeTime);
        }

        private static IServiceCollection AddMexcCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {

            services.AddHttpClient<IMexcRestClient, MexcRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<MexcRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new MexcRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<MexcRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var handler = new HttpClientHandler();
                try
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    handler.DefaultProxyCredentials = CredentialCache.DefaultCredentials;
                }
                catch (PlatformNotSupportedException)
                { }

                var options = serviceProvider.GetRequiredService<IOptions<MexcRestOptions>>().Value;
                if (options.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{options.Proxy.Host}:{options.Proxy.Port}"),
                        Credentials = options.Proxy.Password == null ? null : new NetworkCredential(options.Proxy.Login, options.Proxy.Password)
                    };
                }
                return handler;
            });
            services.Add(new ServiceDescriptor(typeof(IMexcSocketClient), x => { return new MexcSocketClient(x.GetRequiredService<IOptions<MexcSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddTransient<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<IMexcOrderBookFactory, MexcOrderBookFactory>();
            services.AddTransient<IMexcTrackerFactory, MexcTrackerFactory>();
            services.AddTransient(x => x.GetRequiredService<IMexcRestClient>().SpotApi.CommonSpotClient);

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IMexcRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IMexcSocketClient>().SpotApi.SharedClient);
            return services;
        }
    }
}
