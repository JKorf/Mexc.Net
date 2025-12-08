using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets.Default.Interfaces;
using CryptoExchange.Net.Sockets.HighPerf;
using CryptoExchange.Net.Sockets.HighPerf.Interfaces;
using ProtoBuf.Meta;

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
