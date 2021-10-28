using System.Threading.Tasks;
using Xunit;

namespace ProductSuggestions.Tests.Integration.GraphQL
{
    public class QueryByItemNumberIdShould : GraphQLIntegrationTestBase
    {
        public QueryByItemNumberIdShould(InitializeFixture fixture) : base(fixture) { }
        
        [Fact]
        public async Task ReturnAllFields()
        {
            string query = @"
                {
                  byItemNumberId(itemNumberId: 4)
                  {
                    itemNumberId
                    name
                    category
                    available
                    price
                    upsells {
                      itemNumberId
                      name
                      category
                      available
                      price
                    }
                    downsells {
                      itemNumberId
                      name
                      category
                      available
                      price
                    }
                  }
                }
            ";

            await AssertQueryReturnsExpectedDataAsync(query);
        }
    }
}
