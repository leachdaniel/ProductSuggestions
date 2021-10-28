using HotChocolate.Types;
using ProductSuggestions.Products;

namespace ProductSuggestions
{
    public class IGroupMemberType : InterfaceType<IGroupMember>
    {
        protected override void Configure(IInterfaceTypeDescriptor<IGroupMember> descriptor)
        {

            descriptor.Name("IGroupMember");

            descriptor.Field(_ => _.ItemNumberId).Description("unique identifier for a product");

            descriptor.Field(_ => _.Name).Description("the name of the product");

            descriptor.Field(_ => _.Category).Description("the category or kind of product");

            descriptor.Field(_ => _.Price).Description("the price the product is sold for");
        }
    }
}

