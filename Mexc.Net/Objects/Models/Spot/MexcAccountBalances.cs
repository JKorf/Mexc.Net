using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Spot
{
    internal record MexcAccountBalances
    {
        [JsonPropertyName("balances")]
        public MexcAccountBalance[] Balances { get; set; } = [];
    }
}
