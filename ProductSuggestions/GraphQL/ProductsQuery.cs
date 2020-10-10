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
    public class ProductsQuery : ObjectTypeExtension
    {

        public ProductsQuery(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }


        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("Query");

            descriptor.Field("byProductID")
                    .Argument("productID", _ => _.Type<IntType>().Description("The identifier of the product to lookup."))
                 .Description("provides product information")
                 .Type<ProductType>()
                 .Resolver(_ => _productsRepository.GetAsync(_.Argument<int>("productID")));

        }


        private readonly IProductsRepository _productsRepository;

    }
}

