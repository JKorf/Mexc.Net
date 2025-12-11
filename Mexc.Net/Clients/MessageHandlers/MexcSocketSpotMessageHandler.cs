using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using LightProto;
using Mexc.Net.Objects.Models.Protobuf;
using System.Net.WebSockets;
using System.Text.Json;

namespace Mexc.Net.Clients.MessageHandlers
{
    internal class MexcSocketSpotMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(MexcExchange.SerializerContext);

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

              new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("msg").WithEqualConstraint("PONG"),
                ],
                StaticIdentifier = "PONG"
            },

              new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!
              },
        ];
    }

    internal class MexcProtobufMessageHandler : ISocketMessageHandler
    {
        public object Deserialize(ReadOnlySpan<byte> data, Type type)
        {
            return Serializer.Deserialize<MexcUpdate>(data, MexcUpdate.ProtoReader);
        }

        public string? GetTopicFilter(object deserializedObject)
        {
            return ((MexcUpdate)deserializedObject).Channel;
        }

        public string? GetTypeIdentifier(ReadOnlySpan<byte> data, WebSocketMessageType? webSocketMessageType) => "pb";
    }
}
