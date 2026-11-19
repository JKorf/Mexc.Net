using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesPositionMode : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public int PositionMode { get; set; }

        public MexcPositionModeUpdate ToModel()
        {
            return new MexcPositionModeUpdate
            {
                PositionMode = EnumConverter.ParseString<PositionMode>(PositionMode.ToString())!.Value
            };
        }
    }
}
