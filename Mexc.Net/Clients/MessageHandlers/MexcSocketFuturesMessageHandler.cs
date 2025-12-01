using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using Mexc.Net;
using Mexc.Net.Objects.Sockets.Models;
using System.Linq;
using System.Text.Json;

namespace Mexc.Net.Clients.MessageHandlers
{
    internal class MexcSocketFuturesMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(MexcExchange.SerializerContext);

        public MexcSocketFuturesMessageHandler()
        {
            AddTopicMapping<MexcFuturesUpdate>(x => x.Symbol);
        }

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("channel"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("channel")!
            }
        ];
    }
}
