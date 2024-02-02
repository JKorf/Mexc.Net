using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Sockets.Models
{
    internal class MexcResponse
    {
        public int Id { get; set; }
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
