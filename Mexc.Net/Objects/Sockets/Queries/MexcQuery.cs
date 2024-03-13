using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Objects.Sockets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Sockets.Queries
{
    internal class MexcQuery : Query<MexcResponse>
    {
        private readonly IEnumerable<string> _expectedTopics;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public MexcQuery(string method, IEnumerable<string> parameters, bool authenticated, int weight = 1) : base(new MexcRequest
        {
            Id = ExchangeHelpers.NextId(),
            Method = method,
            Parameters = parameters
        }, authenticated, weight)
        {
            _expectedTopics = parameters;
            ListenerIdentifiers = new HashSet<string> { ((MexcRequest)Request).Id.ToString() };
        }

        public override CallResult<MexcResponse> HandleMessage(SocketConnection connection, DataEvent<MexcResponse> message)
        {
            var topics = message.Data.Message.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (!topics.All(t => _expectedTopics.Contains(t)))
                return new CallResult<MexcResponse>(new ServerError(message.Data.Message));

            return base.HandleMessage(connection, message);
        }
    }
}
