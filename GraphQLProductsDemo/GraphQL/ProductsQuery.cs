using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using GraphQLProductsDemo.Products;
using GraphQLProductsDemo.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLProductsDemo
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

