using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets.Default.Interfaces;
using CryptoExchange.Net.Sockets.HighPerf;
using ProtoBuf.Meta;

namespace Mexc.Net.Objects
{
    public class HighPerfProtobufSocketConnection<T> : HighPerfSocketConnection<T>
    {
        private RuntimeTypeModel _runtimeModel;

        /// <summary>
        /// ctor
        /// </summary>
        public HighPerfProtobufSocketConnection(
            ILogger logger,
            IWebsocketFactory socketFactory,
            WebSocketParameters parameters,
            SocketApiClient apiClient,
            RuntimeTypeModel runtimeModel,
            string tag)
            : base(logger, socketFactory, parameters, apiClient, tag)
        {
            _runtimeModel = runtimeModel;
        }

        /// <inheritdoc />
        protected override Task ProcessAsync(CancellationToken ct)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var stream = _pipe.Reader.AsStream();
                    while (!ct.IsCancellationRequested)
                    {
                        var buf = new byte[1];
                        var actRead = stream.Read(buf, 0, 1);


                        await Task.Delay(100);
                    }
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception ex)
                {
                }
            });
        }

    }
}
