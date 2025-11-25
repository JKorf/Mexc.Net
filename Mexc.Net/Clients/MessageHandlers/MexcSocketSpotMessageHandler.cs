using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using LightProto;
using Mexc.Net;
using Mexc.Net.Objects.Models.Protobuf;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;

namespace Mexc.Net.Clients.MessageHandlers
{
    internal class MexcSocketSpotMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(MexcExchange.SerializerContext);

        protected override MessageEvaluator[] MessageEvaluators { get; } = [

              new MessageEvaluator {
                Priority = 1,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("msg") { Constraint = x => x!.Equals("PONG", StringComparison.Ordinal) },
                ],
                StaticIdentifier = "PONG"
            },

              new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!
              },
        ];
    }

    public class MexcProtobufMessageHandler : ISocketMessageHandler
    {
        public object Deserialize(ReadOnlySpan<byte> data, Type type)
        {
            return Serializer.Deserialize<MexcUpdate>(data, MexcUpdate.ProtoReader);
        }

        public string? GetMessageIdentifier(ReadOnlySpan<byte> data, WebSocketMessageType? webSocketMessageType) => "pb";
    }
}
