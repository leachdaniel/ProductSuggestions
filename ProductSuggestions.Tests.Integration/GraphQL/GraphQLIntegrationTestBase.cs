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
    public class GraphQLIntegrationTestBase
    {
        protected HttpClient Client => Initialize.Client;

        protected async Task AssertQueryReturnsExpectedDataAsync(string query, [CallerFilePath] string filePath = null, [CallerMemberName] string methodName = null)
        {
            using var result = await Client.PostAsJsonAsync(string.Empty, new { query });

            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            Assert.IsNotNull(result.Content);

            var jToken = await result.Content.ReadAsAsync<JToken>();

            Assert.IsNotNull(jToken);
            Assert.IsInstanceOfType(jToken, typeof(JObject));
            var errors = jToken.SelectToken("errors") as JArray;

            Assert.IsNull(errors, errors?.ToString(Formatting.Indented));

            var data = jToken.SelectToken("data") as JObject;
            Assert.IsNotNull(data);

            JToken expected = GetExpectedJson(filePath, methodName);

            var patch = new JsonDiffPatch();

            var diff = patch.Diff(expected, data);

            Assert.IsNull(diff, "Json Had the following differences:\n\n:" + diff?.ToString(Formatting.Indented));
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
