using Dapper;
using ProductSuggestions.DataAccess;
using ProductSuggestions.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductSuggestions.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        public ProductsRepository(IProductsDbConnection conn)
        {
            Conn = conn;
        }

        public async Task<IEnumerable<IGroupMember>> GetMembersAsync(Product product)
        {
            var sb = new SqlBuilder();
            var sql = sb.AddTemplate(@"
                SELECT *
                  FROM dbo.Products AS p
                 /**where**/
                ORDER BY p.Price DESC
            ", product);

            sb.Where(@"
                    p.VirtualGroupId = @VirtualGroupId
                AND p.ItemNumberId <> @ItemNumberId
            ");

            return await Conn.QueryAsync<Product>(sql.RawSql, sql.Parameters);
        }



        public IProductsDbConnection Conn { get; }

        public Task<Product?> GetAsync(int itemNumberId) => Conn.GetAsync<Product>(itemNumberId);
    }
}
