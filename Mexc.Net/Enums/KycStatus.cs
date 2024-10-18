using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// KYC status
    /// </summary>
    public enum KycStatus
    {
        /// <summary>
        /// Unverified
        /// </summary>
        [Map("1")]
        Unverified,
        /// <summary>
        /// Primary
        /// </summary>
        [Map("2")]
        Primary,
        /// <summary>
        /// Advanced
        /// </summary>
        [Map("3")]
        Advanced,
        /// <summary>
        /// Institutional
        /// </summary>
        [Map("4")]
        Institutional
    }
}
