using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Options
{
    /// <summary>
    /// Mexc options
    /// </summary>
    public class MexcOptions : LibraryOptions<MexcRestOptions, MexcSocketOptions, ApiCredentials, MexcEnvironment>
    {
    }
}
