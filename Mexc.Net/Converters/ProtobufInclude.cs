using Mexc.Net.Objects.Models.Protobuf;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Converters
{
    internal static class ProtobufInclude
    {
        private static RuntimeTypeModel? _model;
        public static RuntimeTypeModel Model 
        { 
            get
            {
                if (_model != null)
                    return _model;

                var model = RuntimeTypeModel.Create();
                model.Add(typeof(SocketEvent));
                model.Add(typeof(MexcUpdateBookTicker));
                model.Add(typeof(ProtoStreamBookTickerUpdate));
                model.Add(typeof(ProtoStreamBookTicker));
                model.Add(typeof(List<ProtoStreamBookTicker>));

                model.Add(typeof(MexcUpdatePartialOrderBook));
                model.Add(typeof(ProtoOrderBookUpdate));
                model.Add(typeof(ProtoStreamBookEntry));

                model.Add(typeof(MexcUpdateTrades));
                model.Add(typeof(ProtoTradeUpdate));
                model.Add(typeof(ProtoStreamTrade));

                model.Add(typeof(MexcUpdateKlines));
                model.Add(typeof(ProtoStreamKline));

                model.UseImplicitZeroDefaults = false;

                _model = model;
                return _model;
            }
        }
    }
}
