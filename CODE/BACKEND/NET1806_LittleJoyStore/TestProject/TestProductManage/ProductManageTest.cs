using Microsoft.AspNetCore.Mvc.Testing;
using NET1806_LittleJoy.API;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestProject.Test_ProductManage;
using TestProject.Ultils;

namespace TestProject.TestProductManage
{
    public class ProductManageTest
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
        public static IEnumerable<PaginatedProductCsvEntity> PaginatedProductData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\TestProductManage\PaginatedProduct.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<PaginatedProductCsvEntity>(filePath);
        }


        //viết testcase, gọi hàm ở trên để truyền file csv vô
        [Test, TestCaseSource(nameof(PaginatedProductData))]
        public async Task TestPaginatedProductData(PaginatedProductCsvEntity testData)
        {
            var baseUrl = "/api/product"; // Base URL for the GET endpoint

            // Constructing the URL with query string parameters
            var url = $"{baseUrl}?PageIndex={testData.PageIndex}&PageSize={testData.PageSize}";

            var response = await _client.GetAsync(url);

            //nếu muốn đọc json từ cái trả về thì

            //// Đọc nội dung phản hồi JSON
            string responseContent = await response.Content.ReadAsStringAsync();

            //// Chuyển đổi nội dung JSON thành đối tượng (nếu cần thiết)
            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            //// Truy cập thông điệp từ đối tượng JSON
            if (responseData is JArray)
            {
                foreach (var item in responseData)
                {
                    // Truy cập thông điệp từ mỗi phần tử trong mảng JSON (nếu có)
                    string message = item.message; // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn
                    Console.WriteLine($"Message: {message}");
                }
            }
            else
            {
                // Truy cập thông điệp từ đối tượng JSON đơn lẻ
                string message = responseData.message; // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn
                Console.WriteLine($"Message: {message}");
            } // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for Paginating:{testData.PageSize}, {testData.PageIndex}");
        }


        public static IEnumerable<ProductIDEntity> GetProductByID()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\TestProductManage\ProductID.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<ProductIDEntity>(filePath);
        }


        //viết testcase, gọi hàm ở trên để truyền file csv vô
        [Test, TestCaseSource(nameof(GetProductByID))]
        public async Task TestGetProductByID(ProductIDEntity testData)
        {
            var baseUrl = $"/api/product/{testData.Id}"; // Base URL for the GET endpoint

            // Constructing the URL with query string parameters

            var response = await _client.GetAsync(baseUrl);

            //nếu muốn đọc json từ cái trả về thì

            //// Đọc nội dung phản hồi JSON
            string responseContent = await response.Content.ReadAsStringAsync();

            //// Chuyển đổi nội dung JSON thành đối tượng (nếu cần thiết)
            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            //// Truy cập thông điệp từ đối tượng JSON
            string message = responseData.message; // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn

            await Console.Out.WriteLineAsync(message);

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for Id:{testData.Id}");
        }


        //viết testcase, gọi hàm ở trên để truyền file csv vô
        [Test, TestCaseSource(nameof(PaginatedProductData))]
        public async Task TestPaginatedProductOutOfStockData(PaginatedProductCsvEntity testData)
        {
            var baseUrl = "/api/product/product-out-of-stock"; // Base URL for the GET endpoint

            // Constructing the URL with query string parameters
            var url = $"{baseUrl}?PageIndex={testData.PageIndex}&PageSize={testData.PageSize}";

            var response = await _client.GetAsync(url);

            //nếu muốn đọc json từ cái trả về thì

            //// Đọc nội dung phản hồi JSON
            string responseContent = await response.Content.ReadAsStringAsync();

            //// Chuyển đổi nội dung JSON thành đối tượng (nếu cần thiết)
            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            //// Truy cập thông điệp từ đối tượng JSON
            if (responseData is JArray)
            {
                foreach (var item in responseData)
                {
                    // Truy cập thông điệp từ mỗi phần tử trong mảng JSON (nếu có)
                    string message = item.message; // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn
                    Console.WriteLine($"Message: {message}");
                }
            }
            else
            {
                // Truy cập thông điệp từ đối tượng JSON đơn lẻ
                string message = responseData.message; // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn
                Console.WriteLine($"Message: {message}");
            } // Thay "message" bằng tên trường chứa thông điệp trong JSON của bạn

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for Paginating:{testData.PageSize}, {testData.PageIndex}");
        }
    }
}
