using DbUp;
using MartinCostello.SqlLocalDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProductSuggestions.CreateDatabase
{
    public static class LocalDatabaseExtensions
    {
        public static void RegisterAndPopulateLocalDatabase(this IServiceCollection services)
        {
            var options = new SqlLocalDbOptions()
            {
                AutomaticallyDeleteInstanceFiles = true,
                StopOptions = StopInstanceOptions.NoWait,
                StopTimeout = TimeSpan.FromDays(1),
            };

            services.AddSingleton(sp => new SqlLocalDbApi(options, sp.GetRequiredService<ILoggerFactory>()));
            services.AddSingleton(sp =>
            {
                ISqlLocalDbInstanceInfo instance = sp.GetRequiredService<SqlLocalDbApi>().GetOrCreateInstance("ProductSuggestions");

                if (!instance.IsRunning)
                {
                    ISqlLocalDbInstanceManager manager = instance.Manage();
                    manager.Start();
                }

                sp.GetRequiredService<ILogger<ISqlLocalDbInstanceInfo>>().LogInformation($"\nConnectionString:\n{instance.GetConnectionString()}");

                var upgrader = DeployChanges.To
                    .SqlDatabase(instance.GetConnectionString())
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    throw result.Error;
                }

                return instance;
            });
        }
    }
}
