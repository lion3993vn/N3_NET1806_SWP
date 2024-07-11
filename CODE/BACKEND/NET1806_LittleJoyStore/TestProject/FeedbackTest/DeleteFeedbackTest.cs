using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestProject.Ultils;
using NET1806_LittleJoy.API;

namespace TestProject.FeedbackTest
{
    public class DeleteFeedbackTest
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
        public static IEnumerable<DeleteFeedbackEntity> DeleteFeedbackTestData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\FeedbackTest\DeleteFeedbackData.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<DeleteFeedbackEntity>(filePath);
        }


        [Test, TestCaseSource(nameof(DeleteFeedbackTestData))]
        public async Task TestSuccessDeleteFeedback(DeleteFeedbackEntity testData)
        {
            var url = $"api/feedback?Id={testData.Id}&UserId={testData.UserId}";
            var response = await _client.DeleteAsync(url);

            string responseContent = await response.Content.ReadAsStringAsync();

            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            string message = responseData.message;

            await Console.Out.WriteLineAsync($"{message}");

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for Id: {testData.Id}, UserId: {testData.UserId}");
        }

    }
}
