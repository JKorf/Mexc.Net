using LightProto;
using Mexc.Net.Objects.Models.Protobuf;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Binance.Net.Benchmark.Controllers
{
    [ApiController]
    [Route("ws")]
    public class WsController : ControllerBase
    {
        private const int _sendTarget = 10000; // Should match the number in the client

        [HttpGet]
        public async Task Get()
        {
            var webSocket = await Request.HttpContext.WebSockets.AcceptWebSocketAsync();

            // Start after receiving sub request
            var buffer = new byte[1024];
            var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
            var msg = JsonSerializer.Deserialize<SubscribeMessage>(Encoding.UTF8.GetString(buffer, 0, result.Count))!;

            var totalWritter = 0;

            // Sub response
            var response = $"{{ \"id\": {msg.Id}, \"code\":0, \"msg\":\"spot@public.aggre.deals.v3.api.pb@10ms@ETHUSDT\" }}";
            await SendAsync(webSocket, response);
            totalWritter += response.Length;

            var cts = new CancellationTokenSource();
            // Apply cts to wait at end

            _ = Task.Run(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        cts.Cancel();

                        if (webSocket.State == WebSocketState.CloseReceived)
                        {
                            //Console.WriteLine("Closed received, sending close response");
                            await webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Closing", default).ConfigureAwait(false);
                        }
                        else
                        {
                            //Console.WriteLine("Closed received as answer on close request");
                        }
                    }
                }
            });

            var message = new MexcUpdateTrades()
            {
                Channel = "spot@public.aggre.deals.v3.api.pb@10ms@ETHUSDT",
                Data = [
                    new MexcUpdateStreamTrade
                    {
                        Price = "12",
                        Quantity = "1",
                        Side = 1,
                        Timestamp = 1712312312
                    }
                ],
                Symbol = "ETHUSDT"
            };

            var stream = new MemoryStream();
            Serializer.Serialize(stream, message);
            var data = stream.ToArray();
            for (var i = 0; i < _sendTarget; i++)
            {
                if (cts.IsCancellationRequested)
                    break;

                await SendAsync(webSocket, data);
                totalWritter += data.Length;
            }

            if (!cts.IsCancellationRequested)
            {
                //Console.WriteLine("Writing done, closing output");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "done", CancellationToken.None);
            }
            else
            {
                //Console.WriteLine("Writing done, cancellation already requested");
            }

            try
            {
                await Task.Delay(5000, cts.Token);
            }
            catch (Exception) { }

            //Console.WriteLine("Finished");
        }
        private async Task SendAsync(WebSocket webSocket, string message)
        {
            await webSocket.SendAsync(Encoding.UTF8.GetBytes(message),
                WebSocketMessageType.Text,
                endOfMessage: true,
                CancellationToken.None);
        }

        private async Task SendAsync(WebSocket webSocket, byte[] data)
        {
            await webSocket.SendAsync(data,
                WebSocketMessageType.Binary,
                endOfMessage: true,
                CancellationToken.None);
        }

    }

    public record SubscribeMessage
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
