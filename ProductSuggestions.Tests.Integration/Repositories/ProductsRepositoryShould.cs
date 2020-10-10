using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductSuggestions.Repositories;
using ProductSuggestions.Tests.Integration.Startup;
using System.Linq;
using System.Threading.Tasks;

namespace ProductSuggestions.Tests.Integration.Repositories
{
    [TestClass]
    public class ProductsRepositoryShould
    {
        public IProductsRepository TestSubject { get; private set; }
        public IServiceScope Scope { get; private set; }

        [TestInitialize]
        public void Init()
        {
            Scope = Initialize.Services.CreateScope();
            TestSubject = Scope.ServiceProvider.GetRequiredService<IProductsRepository>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Scope.Dispose();
        }

        [TestMethod]
        public async Task SuggestionsOnlyReturnLimitedResults()
        {
            var product = await TestSubject.GetAsync(1);

            var results = (await TestSubject.GetSuggestionsAsync(product, Products.SellMode.Downsell)).ToList();

            Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public async Task DownsellsReturnLowerPricedProducts()
        {
            var product = await TestSubject.GetAsync(1);

            var results = (await TestSubject.GetSuggestionsAsync(product, Products.SellMode.Downsell)).ToList();

            Assert.IsTrue(results.All(_ => _.Price <= product.Price));
        }


        [TestMethod]
        public async Task OnlyReturnAvailable()
        {
            var product = await TestSubject.GetAsync(1);

            var results = (await TestSubject.GetSuggestionsAsync(product, Products.SellMode.Downsell)).ToList();

            Assert.IsTrue(results.All(_ => _.Available));
        }

        [TestMethod]
        public async Task ExcludeSameProduct()
        {
            var product = await TestSubject.GetAsync(3);

            var results = (await TestSubject.GetSuggestionsAsync(product, Products.SellMode.Downsell)).ToList();

            Assert.IsTrue(results.All(_ => _.ProductID != product.ProductID));
        }

        [TestMethod]
        public async Task OnlySameCategory()
        {
            var product = await TestSubject.GetAsync(1);

            var results = (await TestSubject.GetSuggestionsAsync(product, Products.SellMode.Downsell)).ToList();

            Assert.IsTrue(results.All(_ => _.Category == product.Category));
        }

    }
}
