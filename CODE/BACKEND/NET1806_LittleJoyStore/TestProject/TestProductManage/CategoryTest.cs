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
using TestProject.Test_ProductManage;
using TestProject.Ultils;

namespace TestProject.TestProductManage
{
    public class CategoryTest
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
        public static IEnumerable<CategoryCsvEntity> CategoryAddData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\TestProductManage\CreateCategory.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<CategoryCsvEntity>(filePath);
        }

        //viết testcase, gọi hàm ở trên để truyền file csv vô
        [Test, TestCaseSource(nameof(CategoryAddData))]
        public async Task TestCategoryAddData(CategoryCsvEntity testData)
        {
            var url = "/api/category";
            var json = JsonConvert.SerializeObject(new
            {   
                CategoryName = testData.CategoryName
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);

            //nếu muốn đọc json từ cái trả về thì

            //// Đọc nội dung phản hồi JSON
            string responseContent = await response.Content.ReadAsStringAsync();

            //// Chuyển đổi nội dung JSON thành đối tượng (nếu cần thiết)
            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            //// Truy cập thông điệp từ đối tượng JSON
            string message = responseData.message; // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn

            await Console.Out.WriteLineAsync(message);

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for CategoryName:{testData.CategoryName}");
        }

        /***************************************************************************************/

        public static IEnumerable<UpdateCategoryCsvEntity> CategoryUpdateData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\TestProductManage\UpdateCategory.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<UpdateCategoryCsvEntity>(filePath);
        }

        //viết testcase, gọi hàm ở trên để truyền file csv vô
        [Test, TestCaseSource(nameof(CategoryUpdateData))]
        public async Task TestCategoryUpdateData(UpdateCategoryCsvEntity testData)
        {
            var url = "/api/category";
            var json = JsonConvert.SerializeObject(new
            {
                Id = testData.Id,
                CategoryName = testData.CategoryName
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, content);

            //nếu muốn đọc json từ cái trả về thì

            //// Đọc nội dung phản hồi JSON
            string responseContent = await response.Content.ReadAsStringAsync();

            //// Chuyển đổi nội dung JSON thành đối tượng (nếu cần thiết)
            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            //// Truy cập thông điệp từ đối tượng JSON
            string message = responseData.message; // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn

            await Console.Out.WriteLineAsync(message);

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for CategoryName:{testData.CategoryName}, Failed for Id: {testData.Id}");
        }

        public static IEnumerable<DeleteCategoryCsvEntity> CategoryDeleteData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\TestProductManage\RemoveCategory.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<DeleteCategoryCsvEntity>(filePath);
        }

        [Test, TestCaseSource(nameof(CategoryDeleteData))]
            public async Task TestCategoryRemoveData(DeleteCategoryCsvEntity testData)
        {
            var baseUrl = "/api/category"; // Base URL for the GET endpoint

            // Constructing the URL with query string parameters
            var url = $"{baseUrl}?Id={testData.Id}";

            var response = await _client.DeleteAsync(url);

            //nếu muốn đọc json từ cái trả về thì

            //// Đọc nội dung phản hồi JSON
            string responseContent = await response.Content.ReadAsStringAsync();

            //// Chuyển đổi nội dung JSON thành đối tượng (nếu cần thiết)
            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            //// Truy cập thông điệp từ đối tượng JSON
            string message = responseData.message; // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn

            await Console.Out.WriteLineAsync(message);

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for Id: {testData.Id}");
        }

    }
}
