using Dapper.Contrib.Extensions;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductSuggestions.Products
{
    public class Product : IProductSuggestion
    {
        public Product(int productID, string name, string category, decimal price, int quantityOnHand, bool cancelled = false)
        {
            ProductID = productID;
            Name = name;
            Category = category;
            Price = price;
            QuantityOnHand = quantityOnHand;
            Cancelled = cancelled;
        }

        protected Product() { }

        [Key]
        public int ProductID { get; private set; }

        public string Name { get; private set; } = "Unknown";

        public string Category { get; private set; } = "Unknown";

        public decimal Price { get; private set; }

        public int QuantityOnHand { get; private set; }

        public bool Cancelled { get; private set; }

        public bool Available => !Cancelled && QuantityOnHand > 0;
    }
}
