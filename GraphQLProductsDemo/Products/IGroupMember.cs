namespace GraphQLProductsDemo.Products
{
    public interface IGroupMember
    {
        string Category { get; }
        string Name { get; }
        float Price { get; }
        int ItemNumberId { get; }
    }
}