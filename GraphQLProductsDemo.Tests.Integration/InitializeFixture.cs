using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;

namespace GraphQLProductsDemo.Tests.Integration
{

    public class InitializeFixture
    {
        public InitializeFixture()
        {
            Application = new WebApplicationFactory<GraphQLProductsDemo.Startup>();
            Client = Application.CreateClient();
            Services = Application.Services;
        }

        public HttpClient Client { get; private set; }

        public WebApplicationFactory<GraphQLProductsDemo.Startup> Application { get; set; }

        public IServiceProvider Services { get; private set; }

        public void Dispose()
        {
            Application.Dispose();
        }
    }
}
