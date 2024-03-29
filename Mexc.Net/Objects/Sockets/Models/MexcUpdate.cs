﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Sockets.Models
{
    internal class MexcUpdate<T>
    {
        [JsonProperty("c")]
        public string Channel { get; set; } = string.Empty;
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        [JsonProperty("t")]
        public DateTime Timestamp { get; set; }
        [JsonProperty("d")]
        public T Data { get; set; } = default!;
    }
}
