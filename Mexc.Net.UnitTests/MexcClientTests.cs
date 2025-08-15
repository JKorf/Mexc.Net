using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Mexc.Net.Clients;
using Mexc.Net.Objects.Models;
using Mexc.Net.UnitTests.Helpers;
using System.Text.Json;
using NUnit.Framework.Legacy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mexc.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Converters.SystemTextJson;

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
                Message = "Error occurred"
            };

            TestHelpers.SetResponse((MexcRestClient)client, JsonSerializer.Serialize(resultObj), System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.SpotApi.ExchangeData.GetTickersAsync();

            // assert
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
            Assert.That(result.Error!.ErrorCode == "400001");
            Assert.That(result.Error.Message == "Error occurred");
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

        [Test]
        [TestCase(TradeEnvironmentNames.Live, "https://api.mexc.com")]
        [TestCase("", "https://api.mexc.com")]
        public void TestConstructorEnvironments(string environmentName, string expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Mexc:Environment:Name", environmentName },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddMexc(configuration.GetSection("Mexc"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IMexcRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo(expected));
        }

        [Test]
        public void TestConstructorNullEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Mexc", null },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddMexc(configuration.GetSection("Mexc"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IMexcRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.mexc.com"));
        }

        [Test]
        public void TestConstructorApiOverwriteEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Mexc:Environment:Name", "test" },
                    { "Mexc:Rest:Environment:Name", "live" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddMexc(configuration.GetSection("Mexc"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IMexcRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.mexc.com"));
        }

        [Test]
        public void TestConstructorConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ApiCredentials:Key", "123" },
                    { "ApiCredentials:Secret", "456" },
                    { "ApiCredentials:PassPhrase", "222" },
                    { "Socket:ApiCredentials:Key", "456" },
                    { "Socket:ApiCredentials:Secret", "789" },
                    { "Socket:ApiCredentials:PassPhrase", "111" },
                    { "Rest:OutputOriginalData", "true" },
                    { "Socket:OutputOriginalData", "false" },
                    { "Rest:Proxy:Host", "host" },
                    { "Rest:Proxy:Port", "80" },
                    { "Socket:Proxy:Host", "host2" },
                    { "Socket:Proxy:Port", "81" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddMexc(configuration);
            var provider = collection.BuildServiceProvider();

            var restClient = provider.GetRequiredService<IMexcRestClient>();
            var socketClient = provider.GetRequiredService<IMexcSocketClient>();

            Assert.That(((BaseApiClient)restClient.SpotApi).OutputOriginalData, Is.True);
            Assert.That(((BaseApiClient)socketClient.SpotApi).OutputOriginalData, Is.False);
            Assert.That(((BaseApiClient)restClient.SpotApi).AuthenticationProvider.ApiKey, Is.EqualTo("123"));
            Assert.That(((BaseApiClient)socketClient.SpotApi).AuthenticationProvider.ApiKey, Is.EqualTo("456"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(80));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host2"));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(81));
        }
    }
}
