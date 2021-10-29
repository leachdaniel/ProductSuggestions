using FluentAssertions;
using GraphQLProductsDemo.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GraphQLProductsDemo.Tests.Integration.Repositories
{
    public class ProductsRepositoryShould
    {
        public ProductsRepositoryShould(IProductsRepository testSubject)
        {
            TestSubject = testSubject;
        }
        public IProductsRepository TestSubject { get; }

        [Fact]
        public async Task ReturnProductsWithSameVirtualGroupId()
        {
            var product = await TestSubject.GetAsync(13);

            var results = (await TestSubject.GetMembersAsync(product)).ToList();

            results.Select(r => r.ItemNumberId).Should().BeEquivalentTo(new[] { 14, 15, 16 });
        }
    }
}
