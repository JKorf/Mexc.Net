using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using System;

namespace Mexc.Net.Objects.Options
{
    /// <summary>
    /// Mexc options
    /// </summary>
    public class MexcOptions : LibraryOptions<MexcRestOptions, MexcSocketOptions, MexcCredentials, MexcEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();
        /// <summary>
        /// Create MexcOptions instance using the provided configuration action
        /// </summary>
        public static MexcOptions Create(Action<MexcOptions>? configure = null)
        {
            var options = CreateUnconfigured();
            configure?.Invoke(options);
            return Normalize(options);
        }

        /// <summary>
        /// Create MexcOptions using the provided IConfiguration
        /// </summary>
        public static MexcOptions CreateFromConfiguration(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var options = CreateUnconfigured();
            try
            {
                configuration.Bind(options);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Invalid Mexc configuration provided", ex);
            }

            if (options.Environment != null)
                options.Environment = MexcEnvironment.GetEnvironmentByName(options.Environment.Name) ?? options.Environment;
            if (options.Rest?.Environment != null)
                options.Rest.Environment = MexcEnvironment.GetEnvironmentByName(options.Rest.Environment.Name) ?? options.Rest.Environment;
            if (options.Socket?.Environment != null)
                options.Socket.Environment = MexcEnvironment.GetEnvironmentByName(options.Socket.Environment.Name) ?? options.Socket.Environment;

            return Normalize(options);
        }

        private static MexcOptions CreateUnconfigured()
        {
            var options = new MexcOptions();
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            return options;
        }

        private static MexcOptions Normalize(MexcOptions options)
        {
            if (options.Rest == null)
                throw new ArgumentException("REST options cannot be null", nameof(options));
            if (options.Socket == null)
                throw new ArgumentException("Socket options cannot be null", nameof(options));

            options.Rest.Environment ??= options.Environment ?? MexcEnvironment.Live;
            options.Rest.ApiCredentials ??= options.ApiCredentials;
            options.Socket.Environment ??= options.Environment ?? MexcEnvironment.Live;
            options.Socket.ApiCredentials ??= options.ApiCredentials;
            return options;
        }
    }
}
