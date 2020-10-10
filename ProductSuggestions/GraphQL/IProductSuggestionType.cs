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
    public class IProductSuggestionType : InterfaceType<IProductSuggestion>
    {
        protected override void Configure(IInterfaceTypeDescriptor<IProductSuggestion> descriptor)
        {

            descriptor.Name("IProductSuggestion");

            descriptor.Field(_ => _.ProductID).Description("unique identifier for a product");

            descriptor.Field(_ => _.Name).Description("the name of the product");

            descriptor.Field(_ => _.Category).Description("the category or kind of product");

            descriptor.Field(_ => _.Price).Description("the price the product is sold for");

            descriptor.Field(_ => _.Available).Description("true if the product can be sold");
        }
    }
}

