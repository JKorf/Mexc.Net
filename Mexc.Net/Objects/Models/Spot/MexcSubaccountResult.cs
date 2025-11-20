using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub account creation result
    /// </summary>
    internal record MexcSubaccountResult
    {
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}