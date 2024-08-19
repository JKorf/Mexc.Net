using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Trade side enabled status
    /// </summary>
    public enum TradeSidesStatus
    {
        /// <summary>
        /// Both buying and selling are enabled
        /// </summary>
        [Map("1")]
        AllEnabled,
        /// <summary>
        /// Only buying is enabled
        /// </summary>
        [Map("2")]
        BuyEnabled,
        /// <summary>
        /// Only selling is enabled
        /// </summary>
        [Map("3")]
        SellEnabled,
        /// <summary>
        /// Not enabled
        /// </summary>
        [Map("4")]
        NoneEnabled
    }
}
