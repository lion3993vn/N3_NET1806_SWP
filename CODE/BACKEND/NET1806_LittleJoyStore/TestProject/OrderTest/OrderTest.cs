using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using NET1806_LittleJoy.API;
using NET1806_LittleJoy.API.ViewModels.RequestModels;
using NET1806_LittleJoy.Repository.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestProject.DemoTest;
using TestProject.Ultils;

namespace TestProject.OrderTest
{
    public class OrderTest
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

        public static IEnumerable<OrderEntity> OrderTestData()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\OrderTest\OrderData.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<OrderEntity>(filePath);
        }

        public static IEnumerable<OrderCode> ListOrderCode()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\OrderTest\TestResults.csv"); //nhớ chỉnh file path
            return DataProvider.GetTestData<OrderCode>(filePath);
        }

        [Test, TestCaseSource(nameof(OrderTestData))]
        public async Task TestCreateOrder(OrderEntity testData)
        {
            var url = "/api/order/create-order";
            List<ProductOrder> productOrders = new List<ProductOrder>();
            productOrders.Add(new ProductOrder()
            {
                Id = testData.Id,
                Quantity = testData.Quantity,
            });
            var json = JsonConvert.SerializeObject(new
            {
                UserId = testData.UserId,
                TotalPrice = testData.TotalPrice,
                Address = testData.Address,
                Note = testData.Note,
                AmountDiscount = testData.AmountDiscount,
                PaymentMethod = testData.PaymentMethod,
                productOrders = productOrders
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);
            
            if(response.StatusCode == HttpStatusCode.OK)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                dynamic responseData = JsonConvert.DeserializeObject(responseContent);

                int ordercode = responseData.orderCode;


                var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\TestProject\OrderTest\TestResults.csv");
                using (var writer = new StreamWriter(filePath, append: true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecord(ordercode);
                    writer.WriteLine();
                }
            }

            Assert.AreEqual((HttpStatusCode)testData.ResponseCode, response.StatusCode, "Error for testcase: " + testData.STT);
        }

        [Test, TestCaseSource(nameof(ListOrderCode))]
        public async Task TestGetOrder(OrderCode testData)
        {
            var url = "/api/order/get-order-by-orderCode/"+ testData.Code;
            var response = await _client.GetAsync(url);
            var responseCode = (int)response.StatusCode;
            Assert.AreEqual(200, responseCode, "Error for OrderCode: " + testData);
        }
    }
}
