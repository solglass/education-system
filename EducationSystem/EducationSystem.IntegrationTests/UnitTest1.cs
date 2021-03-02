using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace EducationSystem.IntegrationTests
{
    public class Tests
    {
        private TestServer _server;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _server = new TestServer(
                new WebHostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseEnvironment("Testing")
                    .UseStartup<Startup>()
            );

            _client = _server.CreateClient();
        }

        [Test]
        public async Task Test1()
        {
            var result = await _client.GetAsync("url");
            Assert.Pass();
        }

        [OneTimeTearDown]
        public void UberTearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}