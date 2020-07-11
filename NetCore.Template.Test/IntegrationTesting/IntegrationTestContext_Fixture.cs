using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetCore.Template.Api;
using NetCore.Template.Context;
using System;
using System.Net.Http;

namespace NetCore.Template.Test
{
    public class IntegrationTestContext_Fixture : IDisposable
    {
        private TestServer server;
        public HttpClient Client { get; private set; }

        public IntegrationTestContext_Fixture()
        {
            SetUpClient();
        }

        private void SetUpClient()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(@"integration-test.settings.json")
                .Build();

            server = new TestServer(new WebHostBuilder()
                .ConfigureTestServices(services => { })
                .UseConfiguration(configuration)
                .UseStartup<Startup>());

            Client = server.CreateClient();
        }

        public void Dispose()
        {
            //Nothing to dispose
        }
    }
}
