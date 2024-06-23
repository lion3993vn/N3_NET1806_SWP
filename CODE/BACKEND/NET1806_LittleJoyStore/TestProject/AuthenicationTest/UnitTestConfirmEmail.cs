using Microsoft.AspNetCore.Mvc.Testing;
using NET1806_LittleJoy.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestProject.Ultils;

namespace TestProject.AuthenicationTest
{
    public class UnitTestConfirmEmail
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
        public static IEnumerable<ConfirmEmailCsvEntity> ConfirmEmailTestData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\AuthenicationTest\DataTestConfirmEmail.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<ConfirmEmailCsvEntity>(filePath);
        }


        //viết testcase, gọi hàm ở trên để truyền file csv vô
        [Test, TestCaseSource(nameof(ConfirmEmailTestData))]
        public async Task TestSuccessConfirmEmail(ConfirmEmailCsvEntity testData)
        {
            var url = $"/api/authen/confirm-email?token={testData.Token}";
            
            var content = new StringContent("");     
            var response = await _client.PostAsync(url, content);

            //nếu muốn đọc json từ cái trả về thì

            // Đọc nội dung phản hồi JSON
            string responseContent = await response.Content.ReadAsStringAsync();

            // Chuyển đổi nội dung JSON thành đối tượng (nếu cần thiết)
            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            // Truy cập thông điệp từ đối tượng JSON
            string message = responseData.message; // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn

            await Console.Out.WriteLineAsync(message);

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for TokenConfirmEmail: {testData.Token}");
        }
    }
}
