using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Moq.Language;
using NET1806_LittleJoy.API;
using NET1806_LittleJoy.Repository.Entities;
using NET1806_LittleJoy.Service.BusinessModels;
using NET1806_LittleJoy.Service.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestProject.Ultils;

namespace TestCreateBlog
{
    public class CreateBlogTest
    {
        //setup (don't change)
        private WebApplicationFactory<Program> _factory;

        private Mock<IUserService> _mockUserService;
        private HttpClient _client;

        //setup (don't change)
        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _mockUserService.Setup(s => s.GetUserByIdAsync(3))
        .ReturnsAsync(new UserModel { Id = 3, UserName = "hoastaff", Password = "another123", RoleId = 2 });

            _mockUserService.Setup(s => s.GetUserByIdAsync(16))
                .ReturnsAsync(new UserModel { Id = 16, UserName = "hoaadmin", Password = "another123", RoleId = 1 });

            _mockUserService.Setup(s => s.GetUserByIdAsync(It.Is<int>(id => id != 3 && id != 16)))
                .ReturnsAsync(new UserModel { Id = 2, UserName = "hoahoa", Password = "another123", RoleId = 3 });

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
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestCreateBlog\CreateBlogData.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<BlogEntity>(filePath);
        }

        public async Task<string> GetAccessToken(string username, string password)
        {
            var loginUrl = "/api/authen/login"; 
            var json = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(loginUrl, content);

            response.EnsureSuccessStatusCode(); // Throw if not a success code.
            string responseContent = await response.Content.ReadAsStringAsync();


            var responseData = JsonConvert.DeserializeObject<AuthenModel>(responseContent);

            if (responseData == null || string.IsNullOrEmpty(responseData.AccessToken))
            {
                throw new Exception("Failed to retrieve access token.");
            }

            return responseData.AccessToken;
        }


        [Test, TestCaseSource(nameof(BlogTestData))]
        public async Task TestCreateBlog(BlogEntity testData)
        {
            var user = await _mockUserService.Object.GetUserByIdAsync(testData.UserId);

            if (user == null)
            {
                Assert.AreEqual((HttpStatusCode)testData.ResponseCode, HttpStatusCode.Forbidden,
                                $"Failed for Testcase: {testData.Testcase} because test user not found!");
                return;
            }
            
            string accessToken = await GetAccessToken(user.UserName, user.Password);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var url = "/api/blog";
            var json = JsonConvert.SerializeObject(new
            {
                userId = testData.UserId,
                title = testData.Title,
                banner = testData.Banner,
                content = testData.Content
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);

            // Check the response status code first
            await Console.Out.WriteLineAsync("Testcase: " + testData.Testcase);
            await Console.Out.WriteLineAsync($"Expected Response Status: {testData.ResponseCode}, Actual Response Status: {response.StatusCode} ");

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for Testcase: {testData.Testcase}");

            


            if (testData.Testcase != "Regular User Add New Blog")
            {

                string responseContent = await response.Content.ReadAsStringAsync();

                dynamic responseData = JsonConvert.DeserializeObject(responseContent);

                string message = responseData.message;

                await Console.Out.WriteLineAsync($"Expected Response Message: {testData.Message}, Actual Response Message: {message} ");
                Assert.AreEqual(testData.Message, message,
                                $"Failed for Testcase: {testData.Testcase}");

            }


        }

    }
}
