using MartinCostello.SqlLocalDb;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProductSuggestions.DataAccess
{
    public class ProductsDbConnection : DapperDbConnection<SqlConnection>, IProductsDbConnection
    {
        public ProductsDbConnection(ISqlLocalDbInstanceInfo db) : base($"{db.GetConnectionString()};MultipleActiveResultSets=True") { }
    }
}
