﻿using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.Interfaces.Socket.Spot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    public interface IMexcSocketClientSpotApiShared :
        ITickerSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        IBalanceSocketClient,
        ISpotOrderSocketClient,
        IUserTradeSocketClient
    {
    }
}
