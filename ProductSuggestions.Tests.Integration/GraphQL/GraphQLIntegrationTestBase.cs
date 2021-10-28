using JsonDiffPatchDotNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace ProductSuggestions.Tests.Integration.GraphQL
{
    public class GraphQLIntegrationTestBase
    {
        private readonly InitializeFixture _fixture;

        public GraphQLIntegrationTestBase(InitializeFixture fixture)
        {
            _fixture = fixture;
        }
        protected HttpClient Client => _fixture.Client;

        protected async Task AssertQueryReturnsExpectedDataAsync(string query, [CallerFilePath] string filePath = null, [CallerMemberName] string methodName = null)
        {
            using var result = await Client.PostAsJsonAsync(string.Empty, new { query });

            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);

            Assert.NotNull(result.Content);

            var jToken = await result.Content.ReadAsAsync<JToken>();

            Assert.NotNull(jToken);
            Assert.IsType<JObject>(jToken);
            var errors = jToken.SelectToken("errors") as JArray;

            Assert.Null(errors);

            var data = jToken.SelectToken("data") as JObject;
            Assert.NotNull(data);

            JToken expected = GetExpectedJson(filePath, methodName);

            var patch = new JsonDiffPatch();

            var diff = patch.Diff(expected, data);

            Console.WriteLine("Json Had the following differences:\n\n:" + diff?.ToString(Formatting.Indented));

            Assert.Null(diff);
        }

        private JToken GetExpectedJson(string filePath, string methodName)
        {
            string path = GetExpectedPath(filePath, methodName);

            return JsonConvert.DeserializeObject<JToken>(File.ReadAllText(path));
        }
        private static string GetExpectedPath(string filePath, string methodName)
        {
            // some flexibility on file locations in case the build is kept and tests are run from a different directory
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string path = Path.GetDirectoryName(filePath);

            string relativePath = $"ExpectedJson/{fileName}.{methodName}.json";

            string combined = Path.Combine(path, relativePath);

            if (File.Exists(combined))
            {
                return combined;
            }

            string pathFromRootOfProject = Path.Combine("GraphQL", relativePath);

            return FindFilePathByRelativePath(pathFromRootOfProject);
        }

        private static string FindFilePathByRelativePath(string expectedPath, DirectoryInfo directory = null)
        {
            directory ??= new DirectoryInfo(Directory.GetCurrentDirectory());

            while (true)
            {
                string combined = Path.Combine(directory.FullName, expectedPath);
                if (File.Exists(combined))
                {
                    return combined;
                }
                if (directory == null || directory.Parent == directory)
                {
                    throw new FileNotFoundException("Unable to locate expected Json file.", expectedPath);
                }
                else
                {
                    directory = directory.Parent;
                }
            }
        }

    }
}
