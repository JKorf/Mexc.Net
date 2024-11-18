using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Options
{
    /// <summary>
    /// Mexc options
    /// </summary>
    public class MexcOptions
    {
        /// <summary>
        /// Rest client options
        /// </summary>
        public MexcRestOptions Rest { get; set; } = new MexcRestOptions();

        /// <summary>
        /// Socket client options
        /// </summary>
        public MexcSocketOptions Socket { get; set; } = new MexcSocketOptions();

        /// <summary>
        /// Trade environment. Contains info about URL's to use to connect to the API. Use `MexcEnvironment` to swap environment, for example `Environment = MexcEnvironment.Live`
        /// </summary>
        public MexcEnvironment? Environment { get; set; }

        /// <summary>
        /// The api credentials used for signing requests.
        /// </summary>
        public ApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// The DI service lifetime for the IMexcSocketClient
        /// </summary>
        public ServiceLifetime? SocketClientLifeTime { get; set; }
    }
}
