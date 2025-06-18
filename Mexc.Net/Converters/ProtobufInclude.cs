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

                _model = RuntimeTypeModel.Create();
                _model.Add(typeof(SocketEvent));
                _model.Add(typeof(MexcUpdateBookTicker));
                _model.Add(typeof(ProtoStreamBookTickerUpdate));
                _model.Add(typeof(ProtoStreamBookTicker));
                _model.Add(typeof(List<ProtoStreamBookTicker>));

                _model.Add(typeof(MexcUpdatePartialOrderBook));
                _model.Add(typeof(ProtoOrderBookUpdate));
                _model.Add(typeof(ProtoStreamBookEntry));

                _model.Add(typeof(MexcUpdateTrades));
                _model.Add(typeof(ProtoTradeUpdate));
                _model.Add(typeof(ProtoStreamTrade));

                _model.Add(typeof(MexcUpdateKlines));
                _model.Add(typeof(ProtoStreamKline));

                _model.UseImplicitZeroDefaults = false;
                return _model;
            }
        }
    }
}
