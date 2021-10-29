using GraphQLProductsDemo.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLProductsDemo.Repositories
{
    public interface IProductsRepository
    {
        Task<Product?> GetAsync(int itemNumberId);

        Task<IEnumerable<IGroupMember>> GetMembersAsync(Product product);
    }
}
