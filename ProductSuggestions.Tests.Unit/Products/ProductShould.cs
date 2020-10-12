using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductSuggestions.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductSuggestions.Tests.Unit.Products
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ShowNotAvailableWhenCancelled()
        {
            var product = new Product(1, "name", "cat", 1.0m, 1, true);

            Assert.IsFalse(product.Available);
        }

        [TestMethod]
        public void ShowNotAvailableWhenNoQuantity()
        {
            var product = new Product(1, "name", "cat", 1.0m, 0, false);

            Assert.IsFalse(product.Available);
        }
    }
}
