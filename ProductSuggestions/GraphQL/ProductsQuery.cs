using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using ProductSuggestions.Products;
using ProductSuggestions.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductSuggestions
{
    [ExtendObjectType(Name = "Query")]
    public class ProductsQuery
    {

        public ProductsQuery(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [GraphQLDescription("provides product information")]
        [GraphQLType(typeof(ProductType))]
        public Task<Product?> GetByItemNumberIdAsync(int itemNumberId)
        {
            return _productsRepository.GetAsync(itemNumberId);
        }

        private readonly IProductsRepository _productsRepository;

    }
}

