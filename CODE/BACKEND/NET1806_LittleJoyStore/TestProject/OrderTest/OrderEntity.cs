using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.OrderTest
{
    public class OrderEntity
    {
        public int STT {  get; set; }
        public int UserId { get; set; }

        public int TotalPrice { get; set; }

        public string Address { get; set; }

        public string? Note { get; set; }

        public int? AmountDiscount { get; set; }

        public int PaymentMethod { get; set; }

        public int Id { get; set; }

        public int Quantity { get; set; }

        public int ResponseCode { get; set; }
    }

    public class OrderCode
    {
        public int Code { get; set; }
    }
}
