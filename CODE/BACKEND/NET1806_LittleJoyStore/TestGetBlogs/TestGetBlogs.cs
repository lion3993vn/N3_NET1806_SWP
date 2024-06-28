using Microsoft.AspNetCore.Mvc.Testing;
using NET1806_LittleJoy.API;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using TestProject.Ultils;

namespace TestGetBlogs
{
    public class TestGetBlogs
    {
        //setup (don't change)
        private WebApplicationFactory<Program> _factory;

        private HttpClient _client;

        //setup (don't change)
        [SetUp]
        public void Setup()
        { 
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        //setup (don't change)
        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
        public static IEnumerable<BlogEntity> BlogTestData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestGetBlogs\GetBlogData.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<BlogEntity>(filePath);
        }        


        [Test, TestCaseSource(nameof(BlogTestData))]
        public async Task TestGetBlogById(BlogEntity testData)
        {
            var url = $"/api/blog/{testData.Id}";
            var response = await _client.GetAsync(url);

            // Check the response status code first
            await Console.Out.WriteLineAsync("Testcase: " + testData.Testcase);
            await Console.Out.WriteLineAsync($"Expected Response Status: {testData.ResponseCode}, Actual Response Status: {response.StatusCode} ");

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for Testcase: {testData.Testcase}");

        }

        [Test]
        public async Task TestGetBlogList()
        {
            var url = $"/api/blog?PageIndex=1&PageSize=5";
            var response = await _client.GetAsync(url);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Failed to get the expected status code: 200");

        }


    }
}