using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ProductSuggestions.Tests.Integration
{
    
    public class InitializeFixture
    {
        public InitializeFixture()
        {
            Application = new WebApplicationFactory<ProductSuggestions.Startup>();
            Client = Application.CreateClient();
            Services = Application.Services;
        }

        public HttpClient Client { get; private set; }

        public WebApplicationFactory<ProductSuggestions.Startup> Application { get;  set; }

        public IServiceProvider Services { get; private set; }
        

        public void Dispose()
        {
            Application.Dispose();
        }
    }
}
