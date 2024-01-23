using Mexc.Net.Interfaces.Clients.SpotApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Interfaces.Clients
{
    public interface IMexcRestClient
    {
        IMexcRestClientSpotApi SpotApi { get; }
    }
}
