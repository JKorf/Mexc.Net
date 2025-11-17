using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.HighPerf;
using Microsoft.Extensions.Logging;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Mexc.Net.Objects
{
    public class HighPerfProtobufConnectionFactory : IHighPerfConnectionFactory
    {
        private readonly RuntimeTypeModel _model;

        public HighPerfProtobufConnectionFactory(RuntimeTypeModel model)
        {
            _model = model;
        }

        public HighPerfSocketConnection<T> CreateHighPerfConnection<T>(
            ILogger logger, IWebsocketFactory factory, WebSocketParameters parameters, SocketApiClient client, string address)
        {
            return new HighPerfProtobufSocketConnection<T>(logger, factory, parameters, client, _model, address);
        }
    }
}
