using System.Threading.Tasks;
using Xunit;

namespace GraphQLProductsDemo.Tests.Integration.GraphQL
{
    public class QueryByItemNumberIdShould
    {
        private readonly GraphQLIntegrationTestHelper _graphQLIntegrationTestHelper;

        public QueryByItemNumberIdShould(GraphQLIntegrationTestHelper graphQLIntegrationTestHelper)
        {
            _graphQLIntegrationTestHelper = graphQLIntegrationTestHelper;
        }


        [Fact]
        public async Task ReturnAllFields()
        {
            string query = @"
                {
                  byItemNumberId(itemNumberId: 13)
                  {
                    itemNumberId
                    virtualGroupId
                    name
                    category
                    price
                    groupMembers
                    {
                        itemNumberId
                        name
                        category
                        price    
                    }
                  }
                }
            ";

            await _graphQLIntegrationTestHelper.AssertQueryReturnsExpectedDataAsync(query);
        }
    }
}
