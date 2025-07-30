using Mexc.Net.Objects.Models.Spot;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Protobuf
{
    /// <summary>
    /// Account update
    /// </summary>
    [ProtoContract]
    internal record ProtoAccountUpdate
    {
        /// <summary>
        /// Asset
        /// </summary>
        [ProtoMember(1)]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [ProtoMember(8)]
        public long Timestamp { get; set; }
        /// <summary>
        /// New free quantity
        /// </summary>
        [ProtoMember(3)]
        public string Free { get; set; } = string.Empty;
        /// <summary>
        /// Changed free quantity
        /// </summary>
        [ProtoMember(4)]
        public string FreeChange { get; set; } = string.Empty;
        /// <summary>
        /// New frozen quantity
        /// </summary>
        [ProtoMember(5)]
        public string Frozen { get; set; } = string.Empty;
        /// <summary>
        /// Changed frozen quantity
        /// </summary>
        [ProtoMember(6)]
        public string FrozenChange { get; set; } = string.Empty;
        /// <summary>
        /// Trigger update type
        /// </summary>
        [ProtoMember(7)]
        public string UpdateType { get; set; } = string.Empty;

        public MexcAccountUpdate ToModel()
        {
            return new MexcAccountUpdate
            {
                Asset = Asset,
                Timestamp = DateTimeConverter.ParseFromDouble(Timestamp),
                Free = ExchangeHelpers.ParseDecimal(Free) ?? 0,
                FreeChange = ExchangeHelpers.ParseDecimal(FreeChange) ?? 0,
                Frozen = ExchangeHelpers.ParseDecimal(Frozen) ?? 0,
                FrozenChange = ExchangeHelpers.ParseDecimal(FrozenChange) ?? 0,
                UpdateType = UpdateType
            };
        }
    }
}
