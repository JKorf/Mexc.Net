using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using Mexc.Net;
using System.Linq;
using System.Text.Json;

namespace Mexc.Net.Clients.MessageHandlers
{
    internal class MexcSocketFuturesMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(MexcExchange.SerializerContext);

        protected override MessageEvaluator[] MessageEvaluators { get; } = [

             new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("channel"),
                    new PropertyFieldReference("symbol"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("channel") + x.FieldValue("symbol")
            },

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("channel"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("channel")!
            }
        ];
    }
}
