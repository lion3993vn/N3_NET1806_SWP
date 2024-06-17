//using Microsoft.AspNetCore.Mvc.Testing;
//using NET1806_LittleJoy.API;
//using NET1806_LittleJoy.API.ViewModels.RequestModels;
//using Newtonsoft.Json;
//using NUnit.Framework;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using TestProject.Ultils;

//namespace TestProject.DemoTest
//{
//    public class Tests
//    {
//        //setup (don't change)
//        private WebApplicationFactory<Program> _factory;
//        private HttpClient _client;

//        //setup (don't change)
//        [SetUp]
//        public void Setup()
//        {
//            _factory = new WebApplicationFactory<Program>();
//            _client = _factory.CreateClient();
//        }

//        //setup (don't change)
//        [TearDown]
//        public void TearDown()
//        {
//            _client.Dispose();
//            _factory.Dispose();
//        }

//        //provide data (important), nhớ chỉnh entity nữa nha, bật file entity để hiểu
//        public static IEnumerable<LoginCsvEntity> LoginTestData()
//        {
//            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\DemoTest\DataTest.csv"); //nhớ chỉnh file path
//            return DataProvider.GetTestData<LoginCsvEntity>(filePath);
//        }


//        //viết testcase, gọi hàm ở trên để truyền file csv vô
//        [Test, TestCaseSource(nameof(LoginTestData))]
//        public async Task TestSuccessLogin(LoginCsvEntity testData)
//        {
//            var url = "/api/authen/login";
//            var json = JsonConvert.SerializeObject(new
//            {
//                username = testData.Username,
//                password = testData.Password,
//            });
//            var content = new StringContent(json, Encoding.UTF8, "application/json");
//            var response = await _client.PostAsync(url, content);

//            //nếu muốn đọc json từ cái trả về thì

//            //// Đọc nội dung phản hồi JSON
//            //string responseContent = await response.Content.ReadAsStringAsync();

//            //// Chuyển đổi nội dung JSON thành đối tượng (nếu cần thiết)
//            //dynamic responseData = JsonConvert.DeserializeObject(responseContent);

//            //// Truy cập thông điệp từ đối tượng JSON
//            //string message = responseData.message; // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn

//            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
//                            $"Failed for Username: {testData.Username}, Password: {testData.Password}");
//        }
//    }
//}
