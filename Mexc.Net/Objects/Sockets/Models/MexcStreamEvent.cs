using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Sockets.Models
{
    /// <summary>
    /// Stream event
    /// </summary>
    public abstract record MexcStreamEvent
    {
        /// <summary>
        /// Event type
        /// </summary>
        [JsonProperty("e")]
        public string Event { get; set; } = string.Empty;
    }
}
