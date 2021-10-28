using Microsoft.Extensions.DependencyInjection;
using ProductSuggestions.Repositories;
using ProductSuggestions.Tests.Integration.GraphQL;

namespace ProductSuggestions.Tests.Integration
{

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<InitializeFixture>();
            services.AddSingleton(sp => sp.GetRequiredService<InitializeFixture>().Client);
            services.AddTransient<GraphQLIntegrationTestHelper>();

            services.AddTransient(sp => sp.GetRequiredService<InitializeFixture>().Services.GetRequiredService<IProductsRepository>());

        }

    }
}
