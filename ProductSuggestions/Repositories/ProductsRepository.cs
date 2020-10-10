using Dapper;
using ProductSuggestions.DataAccess;
using ProductSuggestions.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductSuggestions.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        public ProductsRepository(IProductsDbConnection conn)
        {
            Conn = conn;
        }

        public async Task<IEnumerable<IProductSuggestion>> GetSuggestionsAsync(Product product, SellMode sellMode, int limit = 3)
        {
            var sb = new SqlBuilder();
            var sql = sb.AddTemplate(@"
                SELECT TOP (@limit) *
                  FROM dbo.Products AS p
                 /**where**/
                ORDER BY p.Price DESC
            ", product);

            sb.AddParameters(new { limit });

            sb.Where(@"
                    p.Category = @Category
                AND p.ProductID <> @ProductID
                AND p.Cancelled = 0
                AND p.QuantityOnHand > 0
            ");

            if (sellMode == SellMode.Downsell)
            {
                sb.Where("p.Price <= @Price");
            }
            else
            {
                sb.Where("p.Price >= @Price");
            }

            return await Conn.QueryAsync<Product>(sql.RawSql, sql.Parameters);
        }



        public IProductsDbConnection Conn { get; }

        public Task<Product?> GetAsync(int productID) => Conn.GetAsync<Product>(productID);


    }
}
