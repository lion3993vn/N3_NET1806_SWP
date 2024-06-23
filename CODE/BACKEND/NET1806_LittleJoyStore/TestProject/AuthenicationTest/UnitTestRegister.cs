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

namespace TestProject.AuthenicationTest
{
    public class UnitTestRegister
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

        //provide data (important), nhớ chỉnh entity nữa nha, bật file entity để hiểu
        public static IEnumerable<RegisterCsvEntity> RegisterTestData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\AuthenicationTest\DataTestRegister.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<RegisterCsvEntity>(filePath);
        }


        //viết testcase, gọi hàm ở trên để truyền file csv vô
        [Test, TestCaseSource(nameof(RegisterTestData))]
        public async Task TestSuccessRegister(RegisterCsvEntity testData)
        {
            var url = "/api/authen/register";
            var json = JsonConvert.SerializeObject(new
            {
                fullname = testData.FullName,
                username = testData.UserName,
                email = testData.Email,
                phonenumber = testData.PhoneNumber,
                password = testData.Password,
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);

            // Đọc nội dung phản hồi JSON
            string responseContent = await response.Content.ReadAsStringAsync();

            // Chuyển đổi nội dung JSON thành đối tượng động
            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            // Kiểm tra nếu phản hồi chứa thông tin lỗi
            if (response.StatusCode == HttpStatusCode.BadRequest && responseData.errors != null)
            {
                // Duyệt qua các lỗi và in ra
                foreach (var error in responseData.errors)
                {
                    string field = error.Name;
                    string[] messages = error.Value.ToObject<string[]>();
                    foreach (var message in messages)
                    {
                        await Console.Out.WriteLineAsync($"Field: {field}, Error: {message}");
                    }
                }
            }
            else if (responseData.message != null)
            {
                // Truy cập thông điệp từ đối tượng JSON
                string message = responseData.message;
                await Console.Out.WriteLineAsync($"Message: {message}");
            }
            else
            {
                // Nếu không có trường "message" hoặc "errors", in ra toàn bộ nội dung phản hồi
                await Console.Out.WriteLineAsync($"Response: {responseContent}");
            }

            // Kiểm tra mã trạng thái HTTP
            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for Username: {testData.UserName}, Password: {testData.Password}, FullName: {testData.FullName}, Email: {testData.Email}, PhoneNumber: {testData.PhoneNumber}");
        }

    }
}
