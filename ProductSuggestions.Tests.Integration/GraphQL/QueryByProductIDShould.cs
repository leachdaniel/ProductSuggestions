using JsonDiffPatchDotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductSuggestions.Tests.Integration.Startup;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ProductSuggestions.Tests.Integration.GraphQL
{
    [TestClass]
    public class QueryByProductIDShould : GraphQLIntegrationTestBase
    {
        [TestMethod]
        public async Task ReturnAllFields()
        {
            string query = @"
                {
                  byProductID(productID: 4)
                  {
                    productID
                    name
                    category
                    available
                    price
                    upsells {
                      productID
                      name
                      category
                      available
                      price
                    }
                    downsells {
                      productID
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
