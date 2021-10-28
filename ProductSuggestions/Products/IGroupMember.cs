namespace ProductSuggestions.Products
{
    public interface IGroupMember
    {
        string Category { get; }
        string Name { get; }
        decimal Price { get; }
        int ItemNumberId { get; }
    }
}