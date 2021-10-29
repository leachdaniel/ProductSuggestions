using HotChocolate.Types;
using GraphQLProductsDemo.Products;
using GraphQLProductsDemo.Repositories;

namespace GraphQLProductsDemo
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

            descriptor.Field(_ => _.ItemNumberId).Description("unique identifier for a product");

            descriptor.Field(_ => _.Name).Description("the name of the product");

            descriptor.Field(_ => _.Category).Description("the category or kind of product");

            descriptor.Field(_ => _.Price).Description("the price the product is sold for");

            descriptor.Field(_ => _.VirtualGroupId).Description("the group this product belongs to");

            descriptor.Field("groupMembers")
                 .Description("groupMembers")
                 .Type<ListType<IGroupMemberType>>()
                 .Resolver(_ => _productsRepository.GetMembersAsync(_.Parent<Product>()));
        }


        private readonly IProductsRepository _productsRepository;

    }
}

