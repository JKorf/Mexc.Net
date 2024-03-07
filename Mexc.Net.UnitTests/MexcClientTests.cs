using Mexc.Net.Clients;
using Mexc.Net.Objects.Models;
using Mexc.Net.UnitTests.Helpers;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;

namespace Mexc.Net.UnitTests
{
    [TestFixture]
    internal class MexcClientTests
    {
        [Test]
        public void CheckRestInterfaces()
        {
            var assembly = Assembly.GetAssembly(typeof(MexcRestClient));
            var ignore = new string[] { "IMexcRestClientSpotApi" };
            var clientInterfaces = assembly.GetTypes().Where(t => t.Name.StartsWith("IMexcRestClient") && !ignore.Contains(t.Name));

            foreach (var clientInterface in clientInterfaces)
            {
                var implementation = assembly.GetTypes().Single(t => t.IsAssignableTo(clientInterface) && t != clientInterface);
                int methods = 0;
                foreach (var method in implementation.GetMethods().Where(m => m.ReturnType.IsAssignableTo(typeof(Task))))
                {
                    var interfaceMethod = clientInterface.GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                    Assert.NotNull(interfaceMethod, $"Missing interface for method {method.Name} in {implementation.Name} implementing interface {clientInterface.Name}");
                    methods++;
                }
                Debug.WriteLine($"{clientInterface.Name} {methods} methods validated");
            }
        }

        [TestCase()]
        public async Task ReceivingHttpErrorWithNoJson_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            TestHelpers.SetResponse((MexcRestClient)client, "", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.SpotApi.ExchangeData.GetTickersAsync();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
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
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error!.Code == 400001);
            Assert.IsTrue(result.Error.Message == "Error occured");
        }
    }
}
