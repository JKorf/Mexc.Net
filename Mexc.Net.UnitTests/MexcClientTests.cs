using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Mexc.Net.Clients;
using Mexc.Net.Objects.Models;
using Mexc.Net.UnitTests.Helpers;
using Newtonsoft.Json;
using NUnit.Framework.Legacy;
using System.Diagnostics;
using System.Reflection;
using CryptoExchange.Net.Converters.JsonNet;

namespace Mexc.Net.UnitTests
{
    [TestFixture]
    internal class MexcClientTests
    {
        [TestCase()]
        public async Task ReceivingHttpErrorWithNoJson_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            TestHelpers.SetResponse((MexcRestClient)client, "", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.SpotApi.ExchangeData.GetTickersAsync();

            // assert
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
        }

        [TestCase()]
        public async Task ReceivingHttpErrorWithJsonError_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            var resultObj = new MexcResult()
            {
                Code = 400001,
                Message = "Error occured"
            };

            TestHelpers.SetResponse((MexcRestClient)client, JsonConvert.SerializeObject(resultObj), System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.SpotApi.ExchangeData.GetTickersAsync();

            // assert
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
            Assert.That(result.Error!.Code == 400001);
            Assert.That(result.Error.Message == "Error occured");
        }

        [Test]
        public void CheckSignatureExample()
        {
            var authProvider = new MexcAuthenticationProvider(
                new ApiCredentials("mx0aBYs33eIilxBWC5", "45d0b3c26f2644f19bfb98b07741b2f5")
                );
            var client = (RestApiClient)new MexcRestClient().SpotApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v3/order",
                (uriParams, bodyParams, headers) =>
                {
                    return uriParams["signature"].ToString();
                },
                "fd3e4e8543c5188531eb7279d68ae7d26a573d0fc5ab0d18eb692451654d837a",
                new Dictionary<string, object>
                {
                    { "symbol", "BTCUSDT" },
                    { "side", "BUY" },
                    { "type", "LIMIT" },
                    { "quantity", "1" },
                    { "price", "11" },
                    { "recvWindow", "5000" },
                },
                time: DateTimeConverter.ConvertFromMilliseconds(1644489390087),
                disableOrdering: true,
                compareCase: false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<MexcRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<MexcSocketClient>();
        }
    }
}
