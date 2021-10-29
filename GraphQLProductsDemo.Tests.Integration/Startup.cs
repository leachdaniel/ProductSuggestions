using Microsoft.Extensions.DependencyInjection;
using GraphQLProductsDemo.Tests.Integration.GraphQL;
using GraphQLProductsDemo.Repositories;

namespace GraphQLProductsDemo.Tests.Integration
{

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<InitializeFixture>();
            services.AddTransient<GraphQLIntegrationTestHelper>();

            services.AddTransient(sp => sp.GetRequiredService<InitializeFixture>().Services.GetRequiredService<IProductsRepository>());

        }

    }
}
