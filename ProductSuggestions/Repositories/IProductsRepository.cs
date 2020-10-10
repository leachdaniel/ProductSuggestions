using ProductSuggestions.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductSuggestions.Repositories
{
    public interface IProductsRepository
    {
        Task<Product?> GetAsync(int productID);

        Task<IEnumerable<IProductSuggestion>> GetSuggestionsAsync(Product product, SellMode sellMode, int limit = 3); 
    }
}
