using Microsoft.AspNetCore.Mvc.Testing;
using NET1806_LittleJoy.API;
using NET1806_LittleJoy.API.ViewModels.RequestModels;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using Moq;
using Moq.Language;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.Ultils;
using NET1806_LittleJoy.Service.Services.Interface;
using NET1806_LittleJoy.Service.BusinessModels;
using System.Net.Http.Headers;

namespace TestProject.CreateAccountTest
{
    public class Tests
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
			_mockUserService.Setup(s => s.GetUserByIdAsync(2))
		.ReturnsAsync(new UserModel { Id = 2, UserName = "admin", Password = "another123", RoleId = 3 });
			_mockUserService.Setup(s => s.GetUserByIdAsync(6))
				.ReturnsAsync(new UserModel { Id = 6, UserName = "staff", Password = "another123", RoleId = 2 });

			_mockUserService.Setup(s => s.GetUserByIdAsync(3))
				.ReturnsAsync(new UserModel { Id = 3, UserName = "user", Password = "another123", RoleId = 1 });

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

        //provide data (important), nhớ chỉnh entity nữa nha, bật file entity để hiểu
        public static IEnumerable<CreateAccountEntity> CreateAccountData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\CreateAccountTest\CreateAccountData.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<CreateAccountEntity>(filePath);
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

		//viết testcase, gọi hàm ở trên để truyền file csv vô
		[Test, TestCaseSource(nameof(CreateAccountData))]
        public async Task TestCreateAccount(CreateAccountEntity testData)
        {
			var user = await _mockUserService.Object.GetUserByIdAsync(testData.CreatorId);

			if (user == null)
			{
				Assert.AreEqual((HttpStatusCode)testData.ResponseCode, HttpStatusCode.Forbidden,
								$"Failed for Testcase: {testData.Testcase} because test user not found!");
				return;
			}

			string accessToken = await GetAccessToken(user.UserName, user.Password);

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var url = "/api/user";
            var json = JsonConvert.SerializeObject(new
            {
                username = testData.UserName,
                password = testData.Password,
                roleId = testData.RoleId,
                fullName = testData.FullName,
                email = testData.Email,
                avatar = testData.Avatar,
                phoneNumber = testData.PhoneNumber,
                mainAddress = testData.MainAddress,

            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);

			await Console.Out.WriteLineAsync("Response: " + response);
			await Console.Out.WriteLineAsync("Testcase: " + testData.Testcase);
			await Console.Out.WriteLineAsync($"Expected Response Status: {testData.ResponseCode}, Actual Response Status: {response.StatusCode} ");

			Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
							$"Failed for Testcase: {testData.Testcase}");

			if (testData.Testcase.Contains("Invalid"))
			{
				string responseContent = await response.Content.ReadAsStringAsync();

				dynamic responseData = JsonConvert.DeserializeObject(responseContent);

				string message = responseData.message;

				await Console.Out.WriteLineAsync($"Expected Response Message: {testData.ResponseMessage}, Actual Response Message: {message} ");
				Assert.AreEqual(testData.ResponseMessage, message,
								$"Failed for Testcase: {testData.Testcase}");
			}

			//nếu muốn đọc json từ cái trả về thì

			//// Đọc nội dung phản hồi JSON
			//string responseContent = await response.Content.ReadAsStringAsync();

			//// Chuyển đổi nội dung JSON thành đối tượng (nếu cần thiết)
			//dynamic responseData = JsonConvert.DeserializeObject(responseContent);

			//// Truy cập thông điệp từ đối tượng JSON
			//string message = responseData.message; // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn

			Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for Username: {testData.UserName}");
        }
    }
}
