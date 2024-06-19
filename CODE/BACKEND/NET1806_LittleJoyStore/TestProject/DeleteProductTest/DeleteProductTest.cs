using Microsoft.AspNetCore.Mvc.Testing;
using NET1806_LittleJoy.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestProject.CreateProductTest;
using TestProject.Ultils;

namespace TestProject.DeleteProductTest
{
    internal class DeleteProductTest
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
        public static IEnumerable<DeleteProductEntity> DeleteProductData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\DeleteProductTest\DeleteProductTest.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<DeleteProductEntity>(filePath);
        }


        [Test, TestCaseSource(nameof(DeleteProductData))]
        public async Task TestDeleteProduct(DeleteProductEntity testData)
        {
            var url = $"/api/product?Id={testData.Id}";
            var response = await _client.DeleteAsync(url);


            // Read JSON response
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            // Verify HTTP status code
            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode,
                            $"Failed for Product: {testData.Id}");

            // Verify error message
            Assert.AreEqual(testData.ErrorMessage, (string)responseData.message,
                            $"Expected error message: {testData.ErrorMessage}, but got: {(string)responseData.message}");

            Console.WriteLine($"Test Case: {testData.Case}, Product ID: {testData.Id}");
            Console.WriteLine($"Expected status code: {(HttpStatusCode)testData.ResponseCode}, Actual status code: {response.StatusCode}");

            Console.WriteLine($"Expected error message: {testData.ErrorMessage}, Actual error message: {(string)responseData.message}");
        }
    }
}
