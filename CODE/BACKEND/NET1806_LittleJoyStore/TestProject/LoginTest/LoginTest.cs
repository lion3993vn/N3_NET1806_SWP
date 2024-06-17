using Microsoft.AspNetCore.Mvc.Testing;
using NET1806_LittleJoy.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestProject.DemoTest;
using TestProject.Ultils;

namespace TestProject.LoginTest
{
    public class LoginTest
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
        public static IEnumerable<LoginEntity> LoginTestData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\LoginTest\LoginData.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<LoginEntity>(filePath);
        }

        public static IEnumerable<RefreshTokenEntity> RefreshToken()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\LoginTest\RefreshTokenData.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<RefreshTokenEntity>(filePath);
        }

        [Test, TestCaseSource(nameof(LoginTestData))]
        public async Task TestSuccessLogin(LoginEntity testData)
        {
            var url = "/api/authen/login";
            var json = JsonConvert.SerializeObject(new
            {
                username = testData.Username,
                password = testData.Password,
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);

            string responseContent = await response.Content.ReadAsStringAsync();

            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            string message = responseData.message;

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for Username: {testData.Username}, Password: {testData.Password}");
            Assert.AreEqual(testData.Message, message,
                            $"Failed for Username: {testData.Username}, Password: {testData.Password}");
        }

        [Test, TestCaseSource(nameof(RefreshToken))]
        public async Task TestRefreshToken(RefreshTokenEntity entity)
        {
            var url = "/api/authen/refresh-token";
            var json = $"\"{entity.Token}\"";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);

            Assert.AreEqual((HttpStatusCode)entity.ResponseCode, response.StatusCode,
                            $"Failed for Token: {entity.Token}");
        }
    }
}
