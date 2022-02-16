using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using GraphQLProductsDemo.Repositories;
using GraphQLProductsDemo.DataAccess;
using GraphQLProductsDemo.Products;
using GraphQLProductsDemo.CreateDatabase;

namespace GraphQLProductsDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // If you need dependency injection with your query object add your query type as a services.
            // services.AddSingleton<Query>();

            // enable InMemory messaging services for subscription support.
            // services.AddInMemorySubscriptions();

            // this enables you to use DataLoader in your resolvers.
            //services.AddDataLoaderRegistry();

            services.RegisterAndPopulateLocalDatabase();

            services.AddScoped<IProductsDbConnection, ProductsDbConnection>();
            services.AddSingleton<IProductsDbConnection, ProductsDbConnection>();
            services.AddSingleton<IProductsRepository, ProductsRepository>();

            // Add GraphQL Services
            services.AddGraphQLServer()
                .AddQueryType(_ => _.Name("Query"))
                .AddType<ProductsQuery>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app
                .UseRouting()
                .UseEndpoints(endpoints => {
                    endpoints
                        .MapGraphQL("/graphql")
                        .WithOptions(new GraphQLServerOptions
                        {
                            AllowedGetOperations = AllowedGetOperations.QueryAndMutation
                        });
                })
                //.UsePlayground("/graphql")
                .UseVoyager();
        }
    }
}
