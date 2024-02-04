using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Sockets.Models
{
    internal class MexcRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; } = string.Empty;
        [JsonProperty("params")]
        public IEnumerable<string> Parameters { get; set; } = Array.Empty<string>();
    }
}
