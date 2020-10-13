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
    public class ProductType : ObjectType<Product>
    {

        public ProductType(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            descriptor.Name("Product");

            // HotChocolate as of 10.5.3 doesn't seem to support inheriting descriptions from the interface type

            descriptor.Field(_ => _.ProductID).Description("unique identifier for a product");

            descriptor.Field(_ => _.Name).Description("the name of the product");

            descriptor.Field(_ => _.Category).Description("the category or kind of product");

            descriptor.Field(_ => _.Price).Description("the price the product is sold for");

            descriptor.Field(_ => _.Available).Description("true if the product can be sold");

            descriptor.Field("downsells")
                 .Description("suggested lower priced products")
                 .Type<ListType<IProductSuggestionType>>()
                 .Resolver(_ => _productsRepository.GetSuggestionsAsync(_.Parent<Product>(), SellMode.Downsell));

            descriptor.Field("upsells")
                 .Description("suggested higher priced products")
                 .Type<ListType<IProductSuggestionType>>()
                 .Resolver(_ => _productsRepository.GetSuggestionsAsync(_.Parent<Product>(), SellMode.Upsell));

            descriptor.Field(_ => _.Cancelled).Ignore();
            descriptor.Field(_ => _.QuantityOnHand).Ignore();
        }


        private readonly IProductsRepository _productsRepository;

    }
}

