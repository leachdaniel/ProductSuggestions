using MartinCostello.SqlLocalDb;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLProductsDemo.DataAccess
{
    public class ProductsDbConnection : DapperDbConnection<SqlConnection>, IProductsDbConnection
    {
        public ProductsDbConnection(ISqlLocalDbInstanceInfo db) : base($"{GetRunningConnectionString(db)};MultipleActiveResultSets=True") { }


        public static string GetRunningConnectionString(ISqlLocalDbInstanceInfo db)
        {
            if (!db.IsRunning)
            {
                db.Manage().Start();
            }

            return db.GetConnectionString();
        }
    }
}
