﻿using Microsoft.AspNetCore.Mvc.Testing;
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
    public class UpdateFeedbackTest
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
        public static IEnumerable<UpdateFeedbackEntity> UpdateFeedbackTestData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\FeedbackTest\UpdateFeedbackData.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<UpdateFeedbackEntity>(filePath);
        }


        [Test, TestCaseSource(nameof(UpdateFeedbackTestData))]
        public async Task TestSuccessUpdateFeedback(UpdateFeedbackEntity testData)
        {
            var url = "/api/feedback";
            var json = JsonConvert.SerializeObject(new
            {
                UserId = testData.UserId,
                Id = testData.Id,
                Comment = testData.Comment,
                Rating = testData.Rating
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, content);

            string responseContent = await response.Content.ReadAsStringAsync();

            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            string message = responseData.message;

            await Console.Out.WriteLineAsync($"{message}");

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for UserId: {testData.UserId}, Id: {testData.Id}, Comment: {testData.Comment}, Rating: {testData.Rating}");
        }
    }
}
