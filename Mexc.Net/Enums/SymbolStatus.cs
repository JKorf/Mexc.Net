﻿using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Symbol status
    /// </summary>
    public enum SymbolStatus
    {
        /// <summary>
        /// Trading is enabled
        /// </summary>
        [Map("1")]
        Enabled,
        /// <summary>
        /// Trading is paused
        /// </summary>
        [Map("2")]
        Paused,
        /// <summary>
        /// Symbol is offline
        /// </summary>
        [Map("3")]
        Offline
    }
}
