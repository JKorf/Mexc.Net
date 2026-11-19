using Mexc.Net.Clients.MessageHandlers;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Protobuf.Futures;
using Mexc.Net.Objects.Sockets.Subscriptions;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.WebSockets;

namespace Mexc.Net.UnitTests
{
    [TestFixture]
    public class FuturesProtobufUpdateTests
    {
        private static readonly MexcFuturesProtobufMessageHandler MessageHandler = new();

        [Test]
        public void RoutesContractUpdateWithSymbolInEnvelope()
        {
            var message = Decode<MexcUpdateFuturesContract>(
                "120D707573682E636F6E74726163740A084254435F5553445418CFF0BC8BAA33A27D1D0A0842" +
                "54435F5553445420036A03313030C00201CA0203302E31B80302");

            MexcUpdateFuturesContract received = null;
            var subscription = new MexcFuturesSubscription<MexcUpdateFuturesContract>(
                NullLogger.Instance, "contract", null, null, null, (_, _, update) => received = update, false);

            var router = subscription.MessageRouter;
            router.BuildSubscriptionRouter();
            var type = MessageHandler.GetTypeIdentifier([], WebSocketMessageType.Binary);
            var topic = MessageHandler.GetTopicFilter(message);
            Assert.That(router.Handle(type, topic, null, DateTime.UtcNow, null, message, out _), Is.True);
            Assert.That(received, Is.SameAs(message));

            var update = received.ToModel();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(update.Symbol, Is.EqualTo("BTC_USDT"));
                Assert.That(update.MaxLeverage, Is.EqualTo(100m));
                Assert.That(update.RiskLimitType, Is.EqualTo(RiskLimitType.ByVolume));
                Assert.That(update.TriggerProtect, Is.EqualTo(0.1m));
                Assert.That(update.FeeRateMode, Is.EqualTo(2));
            }
        }

        [Test]
        public void MapsDetailedTickerTimeZonesAndChangeRates()
        {
            var update = Decode<MexcUpdateFuturesDetailTicker>(
                "120B707573682E7469636B65720A084254435F5553445418CFF0BC8BAA338A7E3C0A08425443" +
                "5F5553445412033130301A04302E30319201055554432B389A0104302E30329A01052D302E30" +
                "31A201055554432B30A201055554432B38").ToModel();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(update.ChangePercentage, Is.EqualTo(1m));
                Assert.That(update.TimeZone, Is.EqualTo("UTC+8"));
                Assert.That(update.ChangeRates, Is.EqualTo(new[] { 0.02m, -0.01m }));
                Assert.That(update.ChangeRatesTimeZones, Is.EqualTo(new[] { "UTC+0", "UTC+8" }));
            }
        }

        [Test]
        public void MapsOrderQuantitiesAndFeeRates()
        {
            var message = Decode<MexcUpdateFuturesOrder>(
                "1213707573682E706572736F6E616C2E6F726465720A084254435F5553445418CFF0BC8BAA33" +
                "CA3E650A1231303230363730303336333139303738343018012001320231303A03322E354A03" +
                "372E3550026001680172084254435F55534454B00180BCC1960BC00103C80101F20106302E30" +
                "303031FA0106302E303030358002048A02034255599202044C4F4E47");
            var update = message.ToModel();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(MessageHandler.GetTopicFilter(message), Is.EqualTo("push.personal.order"));
                Assert.That(update.OrderId, Is.EqualTo(102067003631907840L));
                Assert.That(update.QuantityFilled, Is.EqualTo(7.5m));
                Assert.That(update.RemainingQuantity, Is.EqualTo(2.5m));
                Assert.That(update.MarketOrderLevel, Is.EqualTo(3));
                Assert.That(update.MakerFeeRate, Is.EqualTo(0.0001m));
                Assert.That(update.TakerFeeRate, Is.EqualTo(0.0005m));
                Assert.That(update.BusinessSource, Is.EqualTo(4));
                Assert.That(update.SideType, Is.EqualTo(OrderSide.Buy));
                Assert.That(update.PositionType, Is.EqualTo(PositionSide.Long));
                Assert.That(update.ErrorCode, Is.EqualTo(3000000000L));
            }
        }

        [Test]
        public void MapsPositionPrecisionMarginAndFeeDeductions()
        {
            var update = Decode<MexcUpdateFuturesPosition>(
                "1216707573682E706572736F6E616C2E706F736974696F6E0A084254435F5553445418CFF0BC" +
                "8BAA339A3F5B082A12084254435F555344541801420C3130302E3132333435363738520C3130" +
                "302E30313233343536377A04312E3235980101A001018202100A024D581204302E30311A0430" +
                "2E30348A0206302E30303031920206302E30303035").ToModel();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(update.HoldAveragePriceFullyScale, Is.EqualTo(100.12345678m));
                Assert.That(update.OpenAveragePriceFullyScale, Is.EqualTo(100.01234567m));
                Assert.That(update.MaintenanceMargin, Is.EqualTo(1.25m));
                Assert.That(update.FeeDeductions, Has.Length.EqualTo(1));
                Assert.That(update.FeeDeductions.Single().Asset, Is.EqualTo("MX"));
                Assert.That(update.FeeDeductions.Single().DeductedFee, Is.EqualTo(0.01m));
                Assert.That(update.FeeDeductions.Single().SettlementFee, Is.EqualTo(0.04m));
                Assert.That(update.MakerFeeRate, Is.EqualTo(0.0001m));
                Assert.That(update.TakerFeeRate, Is.EqualTo(0.0005m));
            }
        }

        [Test]
        public void MapsExecutionCycleAndTpSlFlags()
        {
            var planOrder = Decode<MexcUpdateFuturesPlanOrder>(
                "1218707573682E706572736F6E616C2E706C616E2E6F726465720A084254435F5553445418CF" +
                "F0BC8BAA338A3F260A02343212084254435F55534454200148015001581860016801C80101D20106" +
                "302E30303035").ToModel();
            var tpSlOrder = Decode<MexcUpdateFuturesTpSlOrder>(
                "121C707573682E706572736F6E616C2E73746F702E706C616E6F726465720A084254435F5553" +
                "445418CFF0BC8BAA33C23F22082A1A084254435F5553445448015801C00101C80102F80100820206302E3030" +
                "3035").ToModel();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(planOrder.ExecuteCycle, Is.EqualTo(ExecuteCycle.OneDay));
                Assert.That(planOrder.EnsureStopLoss, Is.True);
                Assert.That(planOrder.ExtraTakerFeeRate, Is.EqualTo(0.0005m));
                Assert.That(planOrder.StopLossPriceType, Is.Null);
                Assert.That(tpSlOrder.TakeProfitReverse, Is.EqualTo(FuturesBool.Yes));
                Assert.That(tpSlOrder.StopLossReverse, Is.EqualTo(FuturesBool.No));
                Assert.That(tpSlOrder.EnsureStopLoss, Is.False);
                Assert.That(tpSlOrder.ExtraTakerFeeRate, Is.EqualTo(0.0005m));
                Assert.That(tpSlOrder.TakeProfitPriceType, Is.Null);
            }
        }

        [Test]
        public void MapsTradeIdentityPositionChangeAndMatchingTime()
        {
            var update = Decode<MexcUpdateFuturesTrade>(
                "1209707573682E6465616C0A084254435F5553445418CFF0BC8BAA33AA7D210A033130301203" +
                "322E3518012002280130CEF0BC8BAA3338B96040CDF0BC8BAA33").ToModel();
            var updateWithoutOptionalFields = Decode<MexcUpdateFuturesTrade>(
                "1209707573682E6465616C0A084254435F5553445418CFF0BC8BAA33AA7D130A033130301203" +
                "322E35180130CEF0BC8BAA33").ToModel();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(update.Id, Is.EqualTo(12345L));
                Assert.That(update.PositionChange, Is.EqualTo(2));
                Assert.That(update.MatchTime, Is.EqualTo(DateTimeOffset.FromUnixTimeMilliseconds(1763645012045).UtcDateTime));
                Assert.That(updateWithoutOptionalFields.Id, Is.Null);
                Assert.That(updateWithoutOptionalFields.PositionChange, Is.Null);
                Assert.That(updateWithoutOptionalFields.MatchTime, Is.Null);
            }
        }

        private static T Decode<T>(string hex) where T : MexcFuturesUpdate
        {
            var update = MessageHandler.Deserialize(Convert.FromHexString(hex), typeof(T));
            Assert.That(update, Is.TypeOf<T>());
            return (T)update;
        }
    }
}
