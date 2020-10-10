namespace ProductSuggestions.Products
{
    public interface IProductSuggestion
    {
        bool Available { get; }
        string Category { get; }
        string Name { get; }
        decimal Price { get; }
        int ProductID { get; }
    }
}