using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

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
    }
}
