using Mexc.Net.Objects.Models.Spot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Sockets.Models
{
    internal class MexcKlineUpdate : MexcStreamEvent
    {
        [JsonProperty("k")]
        public MexcStreamKline Data { get; set; } = default!;
    }
}
