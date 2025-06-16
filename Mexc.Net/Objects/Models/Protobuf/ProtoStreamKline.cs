using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [ProtoContract]
    internal class ProtoStreamKline
    {
        /// <summary>
        /// Start time
        /// </summary>
        [ProtoMember(2)]
        public long StartTime { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [ProtoMember(9)]
        public long EndTime { get; set; }
        /// <summary>
        /// Volume in quote asset
        /// </summary>
        [ProtoMember(8)]
        public string QuoteVolume { get; set; } = string.Empty;
        /// <summary>
        /// Close price
        /// </summary>
        [ProtoMember(4)]
        public string ClosePrice { get; set; } = string.Empty;
        /// <summary>
        /// Highest price
        /// </summary>
        [ProtoMember(5)]
        public string HighPrice { get; set; } = string.Empty;
        /// <summary>
        /// Interval
        /// </summary>
        [ProtoMember(1)]
        public string Interval { get; set; } = string.Empty;
        /// <summary>
        /// Lowest price
        /// </summary>
        [ProtoMember(6)]
        public string LowPrice { get; set; } = string.Empty;
        /// <summary>
        /// Open price
        /// </summary>
        [ProtoMember(3)]
        public string OpenPrice { get; set; } = string.Empty;
        /// <summary>
        /// Volume
        /// </summary>
        [ProtoMember(7)]
        public string Volume { get; set; } = string.Empty;

        public MexcStreamKline ToModel()
        {
            return new MexcStreamKline
            {
                ClosePrice = ExchangeHelpers.ParseDecimal(ClosePrice) ?? 0,
                OpenPrice = ExchangeHelpers.ParseDecimal(OpenPrice) ?? 0,
                HighPrice = ExchangeHelpers.ParseDecimal(HighPrice) ?? 0,
                LowPrice = ExchangeHelpers.ParseDecimal(LowPrice) ?? 0,
                QuoteVolume = ExchangeHelpers.ParseDecimal(QuoteVolume) ?? 0,
                Volume = ExchangeHelpers.ParseDecimal(Volume) ?? 0,
                StartTime = DateTimeConverter.ParseFromDouble(StartTime),
                EndTime = DateTimeConverter.ParseFromDouble(EndTime),
                Interval = EnumConverter.ParseString<KlineInterval>(Interval) ?? default
            };
        }
    }
}
