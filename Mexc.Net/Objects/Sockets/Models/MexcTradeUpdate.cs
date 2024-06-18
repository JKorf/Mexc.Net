using Mexc.Net.Objects.Models.Spot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Sockets.Models
{
    internal record MexcTradeUpdate : MexcStreamEvent
    {
        [JsonProperty("deals")]
        public IEnumerable<MexcStreamTrade> Data { get; set; } = Array.Empty<MexcStreamTrade>();
    }
}
