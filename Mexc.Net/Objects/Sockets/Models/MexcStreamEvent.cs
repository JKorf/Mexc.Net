using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Sockets.Models
{
    public abstract class MexcStreamEvent
    {
        [JsonProperty("e")]
        public string Event { get; set; } = string.Empty;
    }
}
