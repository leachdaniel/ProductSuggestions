using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ProductSuggestions.Tests.Integration.Startup
{
    /// <summary>
    /// The test engine runs a method that is marked with the AssemblyInitialize attribute only if that method is a member of a class that is marked with the TestClass attribute.
    /// </summary>
    [TestClass]
    public class Initialize
    {
        public static HttpClient Client { get; private set; }
        private static WebApplicationFactory<ProductSuggestions.Startup> Application { get;  set; }

        public static IServiceProvider Services { get; private set; }
        

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Application = new CustomWebApplicationFactory<ProductSuggestions.Startup>();
            Client = Application.CreateClient();
            Services = Application.Services;
        }

        [AssemblyCleanup]
        public static void TearDown()
        {
            Application.Dispose();
        }
    }
}
