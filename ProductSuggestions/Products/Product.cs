using Dapper.Contrib.Extensions;

namespace ProductSuggestions.Products
{
    public class Product : IGroupMember
    {
        public Product(int itemNumberId, string name, string category, decimal price, int? virtualGroupId)
        {
            ItemNumberId = itemNumberId;
            Name = name;
            Category = category;
            Price = price;
            VirtualGroupId = virtualGroupId;
        }

        protected Product() { }

        [Key]
        public int ItemNumberId { get; private set; }

        public string Name { get; private set; } = "Unknown";

        public string Category { get; private set; } = "Unknown";

        public decimal Price { get; private set; }

        public int? VirtualGroupId { get; private set; }
    }
}
